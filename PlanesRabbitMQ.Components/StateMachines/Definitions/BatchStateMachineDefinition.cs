using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using PlanesRabbitMQ.Contracts;

namespace PlanesRabbitMQ.Components.StateMachines.Definitions
{
    public class BatchStateMachineDefinition :
        SagaDefinition<BatchState>
    {
        public BatchStateMachineDefinition()
        {
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator,
            ISagaConfigurator<BatchState> sagaConfigurator)
        {
            sagaConfigurator.UseMessageRetry(r => r.Immediate(5));
            sagaConfigurator.UseInMemoryOutbox();

            var partition = endpointConfigurator.CreatePartitioner(8);

            sagaConfigurator.Message<BatchJobDone>(x => x
                .UsePartitioner(partition, m => m.Message.BatchId));
            sagaConfigurator.Message<BatchReceived>(x => x
                .UsePartitioner(partition, m => m.Message.BatchId));
            sagaConfigurator.Message<CancelBatch>(x => x
                .UsePartitioner(partition, m => m.Message.BatchId));
        }
    }
}