
using MassTransit;
using MassTransit.Definition;
using PlanesRabbitMQ.Contracts;

namespace PlanesRabbitMQ.Components.Consumers.Definitions
{
    public class SubmitBatchConsumerDefinition :
        ConsumerDefinition<SubmitBatchConsumer>
    {
        public SubmitBatchConsumerDefinition()
        {
            ConcurrentMessageLimit = 10;

          /*  Request<SubmitBatch>(x =>
            {
                x.Responds<BatchRejected>();
                x.Responds<BatchSubmitted>();

                x.Publishes<BatchReceived>();
            });*/
        }
    }
}