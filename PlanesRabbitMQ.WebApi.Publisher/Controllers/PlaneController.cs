using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PlanesRabbitMQ.BL.Services.Impl;
using PlanesRabbitMQ.BL.Services.Requests;

namespace PlanesRabbitMQ.WebApi.Publisher.Controllers
{
    public class PlaneController :  ControllerBase
    {
        private readonly IBatchExecutionPlanesServices _executionPlanesService;

        public PlaneController(IBatchExecutionPlanesServices executionPlanesService)
        {
           _executionPlanesService = executionPlanesService;
        }

        [HttpPost("create", Name = "Create")]
        public async Task<ActionResult<Guid>> Post(PostBatchPlanesRequest planesRequest)
        {
            var batchId = NewId.NextGuid();
            await _executionPlanesService
                .PopulateInPlanesInBatch(planesRequest,batchId);
            return Accepted(batchId);
        }
    }
}