using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PlanesRabbitMQ.BL.Services.Impl;
using PlanesRabbitMQ.BL.Services.Requests;using PlanesRabbitMQ.Contracts.Planes;

namespace PlanesRabbitMQ.BL.Services.Contracts
{
    public class BatchExecutionPlanesServices:IBatchExecutionPlanesServices
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IXmlPlanesParser _xmlPlanesParser;
        private readonly ILogger<BatchExecutionPlanesServices> _logger;

        public BatchExecutionPlanesServices(IPublishEndpoint publishEndpoint,
            IXmlPlanesParser xmlPlanesParser,
            ILogger<BatchExecutionPlanesServices> logger)
        {
            _publishEndpoint = publishEndpoint;
            _xmlPlanesParser = xmlPlanesParser;
            _logger = logger;
        }

        public async Task PopulateInPlanesInBatch(
            PostBatchPlanesRequest planesRequest, Guid batchId)
        {
            _logger.LogInformation("Starting execution of planes batch with id: {batchId}",batchId);
            var planes = _xmlPlanesParser.ParsePlanes();
            for (int i = 0; i <Math.Min(planesRequest.PlanesCount, planes.Count); i++)
            {
                await _publishEndpoint.Publish<PlaneBatchAddRequested>(new
                {
                    BatchId = batchId,
                    Plane = planes[i],
                });  
            }
        }
    }
}
