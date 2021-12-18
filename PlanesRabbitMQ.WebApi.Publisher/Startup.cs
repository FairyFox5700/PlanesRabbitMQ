using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PlanesRabbitMQ.BL.Services.Contracts;
using PlanesRabbitMQ.BL.Services.Impl;
using PlanesRabbitMQ.Contracts;
using PlanesRabbitMQ.WebApi.Publisher.Internal;

namespace PlanesRabbitMQ.WebApi.Publisher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "PlanesRabbitMQ.WebApi.Publisher", Version = "v1"});
            });
            services.AddHealthChecks();
            var appConfig = Configuration.GetSection(nameof(AppConfig)).Get<AppConfig>();
            services.Configure<AppConfig>(options => Configuration.GetSection("AppConfig").Bind(options));
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);

                    cfg.Host((string) appConfig.RabbitMq.HostAddress, appConfig.RabbitMq.VirtualHost, 
                        h =>
                        {
                            h.Username(appConfig.RabbitMq.Username);
                            h.Password(appConfig.RabbitMq.Password);
                        });
                
                }));
            });
            
            services.AddMassTransitHostedService();
                /*services.AddMassTransit(cfg =>
            {
                cfg.SetKebabCaseEndpointNameFormatter();
                if (appConfig.RabbitMq != null)
                {
                    cfg.UsingRabbitMq((x, y) =>
                    {
                        y.Host((string) appConfig.RabbitMq.HostAddress, appConfig.RabbitMq.VirtualHost, 
                            h =>
                        {
                            h.Username(appConfig.RabbitMq.Username);
                            h.Password(appConfig.RabbitMq.Password);
                        });

                        y.ConfigureEndpoints(x);
                    });
                }
                else
                {
                    throw new ApplicationException("Invalid Bus configuration. Couldn't find RabbitMq config");
                }
            });
            
            services.AddMassTransitHostedService();*/
            services.AddTransient<IBatchExecutionPlanesServices, BatchExecutionPlanesServices>();
            services.AddTransient<IXmlPlanesParser, XmlPlanesParser>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlanesRabbitMQ.WebApi.Publisher v1"));
            }

            app.UseHttpsRedirection();
            app.UseWebSockets();
            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}