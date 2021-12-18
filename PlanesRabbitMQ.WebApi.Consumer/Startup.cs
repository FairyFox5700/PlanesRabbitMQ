using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PlanesRabbitMQ.Contracts.Planes;
using PlanesRabbitMQ.DAL;
using PlanesRabbitMQ.DAL.Impl;
using PlanesRabbitMQ.WebApi.Consumer.Components;
using PlanesRabbitMQ.WebApi.Consumer.Internal;

namespace PlanesRabbitMQ.WebApi.Consumer
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
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "PlanesRabbitMQ.WebApi.Consumer", Version = "v1"});
            });
            services.AddDbContext<PlanesContext>(x
                => x.UseSqlServer(Configuration.GetConnectionString("PlanesConnection")));
            services.AddTransient<IPlanesRepository, PlanesRepository>();
            services.AddScoped<PlanesConsumer>();
            services.AddHealthChecks();
            var appConfig = Configuration.GetSection(nameof(AppConfig)).Get<AppConfig>();
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.Configure<AppConfig>(options => Configuration.GetSection("AppConfig").Bind(options));
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((cfg, y) =>
                {
                    y.Host((string) appConfig.RabbitMq.HostAddress, appConfig.RabbitMq.VirtualHost,
                        h =>
                        {
                            h.Username(appConfig.RabbitMq.Username);
                            h.Password(appConfig.RabbitMq.Password);
                        });

                    ConfigureBus(cfg, y);
                });
            });
            services.AddMassTransitHostedService();
            /* services.AddControllers();
             services.AddMassTransit((cfg) =>
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
                         ConfigureBus(x,y);
                     });
                 }
                 else
                 {
                     throw new ApplicationException("Invalid Bus configuration. Couldn't find RabbitMq config");
                 }
                 cfg.AddConsumersFromNamespaceContaining<PlanesConsumer>();
             });*/
            //services.AddMassTransitHostedService();
        }

        private void ConfigureBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.ReceiveEndpoint(KebabCaseEndpointNameFormatter.Instance.Consumer<PlanesConsumer>(), 
                e =>
            {
                e.PrefetchCount = 20;
                e.Batch<PlaneBatchAddRequested>(b =>
                {
                    b.MessageLimit = 100;
                    b.ConcurrencyLimit = 10;
                    b.TimeLimit = TimeSpan.FromSeconds(1);
                    b.Consumer<PlanesConsumer, PlaneBatchAddRequested>(context);
                });
               configurator.ConfigureEndpoints(context);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint("/swagger/v1/swagger.json", 
                        "PlanesRabbitMQ.WebApi.Consumer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}