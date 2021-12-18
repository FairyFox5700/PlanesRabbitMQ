using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace PlanesRabbitMQ.WebApi.Consumer.Components
{
    public class PlanesConsumerDefinition: ConsumerDefinition<PlanesConsumer>
    {
        public PlanesConsumerDefinition()
        {
            Endpoint(x => x.PrefetchCount = 1000);
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<PlanesConsumer> consumerConfigurator)
        {
            consumerConfigurator.Options<BatchOptions>(options => options
                .SetMessageLimit(100)
                .SetTimeLimit(1000)
                .SetConcurrencyLimit(10));
        }
    }
}
