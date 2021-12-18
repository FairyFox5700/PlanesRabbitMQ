using System;
using System.Threading.Tasks;
using PlanesRabbitMQ.BL.Services.Requests;

namespace PlanesRabbitMQ.BL.Services.Impl
{
    public interface IBatchExecutionPlanesServices
    {
        Task PopulateInPlanesInBatch(
           PostBatchPlanesRequest planesRequest, Guid batchId);
    }
}