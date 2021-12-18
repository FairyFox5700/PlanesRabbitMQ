using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Courier;
using MassTransit.Courier.Contracts;
using Microsoft.Extensions.Logging;
using PlanesRabbitMQ.Contracts;
using PlanesRabbitMQ.Contracts.Enums;

namespace PlanesRabbitMQ.Components.Consumers
{
    public class ProcessBatchJobConsumer :  IConsumer<ProcessBatchJob>
    {
        readonly ILogger<ProcessBatchJobConsumer> _logger;

        public ProcessBatchJobConsumer(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ProcessBatchJobConsumer>();
        }

        public async Task Consume(ConsumeContext<ProcessBatchJob> context)
        {
            using (_logger.BeginScope("ProcessBatchJob {BatchJobId}, {PlaneId}", 
                context.Message.BatchJobId, context.Message.PlaneId))
            {
                var builder = new RoutingSlipBuilder(NewId.NextGuid());

                switch (context.Message.Action)
                {
                    case BatchAction.CancelPlanesProcessing:
                        builder.AddActivity(
                            "CancelPlane",
                            new Uri("queue:cancel-plane_execute"),
                            new
                            {
                                context.Message.PlaneId,
                                Reason = "Product discontinued",
                            });

                        await builder.AddSubscription(
                            context.SourceAddress,
                            RoutingSlipEvents.ActivityFaulted,
                            RoutingSlipEventContents.None,
                            "CancelPlane",
                            x => x.Send<BatchJobFailed>(new
                            {
                                context.Message.BatchJobId,
                                context.Message.BatchId,
                                context.Message.PlaneId,
                            }));
                        break;

                    case BatchAction.SuspendPlanesProcessing:
                        builder.AddActivity(
                            "SuspendPlane",
                            new Uri("queue:suspend-planes_execute"),
                            new {context.Message.PlaneId});

                        await builder.AddSubscription(
                            context.SourceAddress,
                            RoutingSlipEvents.ActivityFaulted,
                            RoutingSlipEventContents.None,
                            "SuspendPlane",
                            x => x.Send<BatchJobFailed>(new
                            {
                                context.Message.BatchJobId,
                                context.Message.BatchId,
                                context.Message.PlaneId,
                            }));
                        break;
                }

                await builder.AddSubscription(
                    context.SourceAddress,
                    RoutingSlipEvents.Completed,
                    x => x.Send<BatchJobCompleted>(new
                    {
                        context.Message.BatchJobId,
                        context.Message.BatchId,
                    }));

                await context.Execute(builder.Build());
            }
        }
    }
}