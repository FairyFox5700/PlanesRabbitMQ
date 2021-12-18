using GreenPipes;
using MassTransit;
using MassTransit.Definition;

namespace PlanesRabbitMQ.Components.StateMachines.Definitions
{
    public class JobStateMachineDefinition :
        SagaDefinition<JobState>
    {
        public JobStateMachineDefinition()
        {
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, 
            ISagaConfigurator<JobState> sagaConfigurator)
        {
            sagaConfigurator.UseMessageRetry(r => r.Immediate(5));
            sagaConfigurator.UseInMemoryOutbox();
        }
    }
}