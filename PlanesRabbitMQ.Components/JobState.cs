using System;
using Automatonymous;
using PlanesRabbitMQ.Contracts.Enums;

namespace PlanesRabbitMQ.Components
{
    public class JobState :
        SagaStateMachineInstance
    {
        public Guid BatchId { get; set; }

        public Guid PlaneId { get; set; }

        public string CurrentState { get; set; }

        public DateTime? ReceiveTimestamp { get; set; }

        public DateTime? CreateTimestamp { get; set; }

        public DateTime? UpdateTimestamp { get; set; }

        public BatchAction Action { get; set; }

        public string ExceptionMessage { get; set; }
        
        public BatchState Batch { get; set; }
        public Guid CorrelationId { get; set; }
    }
}