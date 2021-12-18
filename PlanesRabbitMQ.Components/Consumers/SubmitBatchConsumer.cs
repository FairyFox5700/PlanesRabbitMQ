using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PlanesRabbitMQ.Contracts;

namespace PlanesRabbitMQ.Components.Consumers
{
    public class SubmitBatchConsumer : IConsumer<SubmitBatch>
    {
        readonly ILogger<SubmitBatchConsumer> _logger;

        public SubmitBatchConsumer(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SubmitBatchConsumer>();
        }

        public async Task Consume(ConsumeContext<SubmitBatch> context)
        {
            using (_logger.BeginScope("SubmitBatch {BatchId}", context.Message.BatchId))
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                    _logger.LogDebug("Validating batch {BatchId}", context.Message.BatchId);

                // do some validation
                if (context.Message.PlanesIds.Length == 0)
                {
                    await context.RespondAsync<BatchRejected>(new
                    {
                        context.Message.BatchId,
                        InVar.Timestamp,
                        Reason = "Must have at least one PlaneId to Process"
                    });

                    return;
                }

                await context.Publish<BatchReceived>(new
                {
                    context.Message.BatchId,
                    InVar.Timestamp,
                    context.Message.Action,
                    context.Message.PlanesIds,
                    context.Message.ActiveThreshold,
                    context.Message.DelayInSeconds,
                });

                await context.RespondAsync<BatchSubmitted>(new
                {
                    context.Message.BatchId,
                    InVar.Timestamp
                });

                if (_logger.IsEnabled(LogLevel.Debug))
                    _logger.LogDebug("Accepted plane {BatchId}", context.Message.BatchId);
            }
        }
    }
}