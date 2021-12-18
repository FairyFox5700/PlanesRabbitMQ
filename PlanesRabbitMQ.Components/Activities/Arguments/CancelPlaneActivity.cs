using System;
using System.Threading.Tasks;
using MassTransit.Courier;
using MassTransit.Courier.Exceptions;
using Microsoft.Extensions.Logging;

namespace PlanesRabbitMQ.Components.Activities.Arguments
{
    public class CancelPlaneActivity: IExecuteActivity<CancelPlanesArguments>
    {
        private readonly BatchDbContext _dbContext;
        readonly ILogger _logger;

        public CancelPlaneActivity(BatchDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<CancelPlaneActivity>();
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<CancelPlanesArguments> context)
        {
            _logger.LogInformation("Cancelling {PlaneId}, with Reason: {Reason}",
                context.Arguments.PlaneId, context.Arguments.Reason);

            var random = new Random(DateTime.Now.Millisecond);

            if (random.Next(1, 10) == 1)
                throw new RoutingSlipException("Plane shipped, cannot cancel");

            await Task.Delay(random.Next(1, 7) * 1000);

            return context.Completed();
        }
    }
}