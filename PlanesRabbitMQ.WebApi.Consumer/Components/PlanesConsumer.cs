using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PlanesRabbitMQ.Contracts.Planes;
using PlanesRabbitMQ.DAL.Entities;
using PlanesRabbitMQ.DAL.Impl;

namespace PlanesRabbitMQ.WebApi.Consumer.Components
{
    public class PlanesConsumer: IConsumer<Batch<PlaneBatchAddRequested>>
    {
        private readonly IPlanesRepository _planesRepository;
        private readonly ILogger<PlanesConsumer> _logger;

        public PlanesConsumer(ILogger<PlanesConsumer> logger, IPlanesRepository planesRepository)
        {
            _planesRepository = planesRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Batch<PlaneBatchAddRequested>> context)
        {
            for (int i = 0; i < context.Message.Length; i++)
            {
                ConsumeContext<PlaneBatchAddRequested> message = context.Message[i];

                _logger.LogInformation("Plane: {0}", message.Message.Plane.ToString());
            }

            var planesToInsert = context.Message
                .Select(e=>e.Message.Plane)
                .Distinct()
                .Select(p => new Plane()
                {
                    Chars = new Chars()
                    {
                        Characteristics =  new ScoutCharacteristics()
                        {
                            RocketsCount= (int)p.Chars.Characteristics.RocketsCount,
                        },
                        HasRadar = p.Chars.HasRadar,
                        HasAmmunition = p.Chars.HasAmmunition,
                        Type = p.Chars.Type,
                        NumberOfSeats = p.Chars.NumberOfSeats,
                    },
                    Price = p.Price,
                    Id = p.Id,
                    Origin = p.Origin,
                    Model = p.Model,
                    Parameters = new Parameters()
                    {
                        Height = p.Parameters.Height,
                        Length = p.Parameters.Length,
                        Width = p.Parameters.Width
                    },
                })
                .ToList();
            
          await  _planesRepository.BulkInsertPlanes(planesToInsert);
        }
    }
}