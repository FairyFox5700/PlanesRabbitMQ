using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Microsoft.Extensions.Logging;
using PlanesRabbitMQ.DAL.Entities;

namespace PlanesRabbitMQ.DAL.Impl
{
    public class PlanesRepository: IPlanesRepository
    {
        private readonly ILogger<PlanesRepository> _logger;
        private readonly PlanesContext _planesContext;

        public PlanesRepository(ILogger<PlanesRepository> logger,
            PlanesContext planesContext)
        {
            _logger = logger;
            _planesContext = planesContext;
        }

        public async Task BulkInsertPlanes(List<Plane>planesToInsert)
        {
            await using var transaction = await _planesContext.Database.BeginTransactionAsync();
            try
            {
                await _planesContext.BulkInsertAsync(planesToInsert,config =>config.IncludeGraph=true );
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Exception occured while executing batch insert :{ex.Message}");
            }
        }
        
    }
}