using System;
using System.Collections.Generic;
using Automatonymous;
using PlanesRabbitMQ.Contracts.Enums;

namespace PlanesRabbitMQ.Components
{
    public class BatchState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public string CurrentState { get; set; }

        public DateTime? ReceiveTimestamp { get; set; }

        public DateTime? CreateTimestamp { get; set; }

        public DateTime? UpdateTimestamp { get; set; }

        public BatchAction? Action { get; set; }
        public int? ActiveThreshold { get; set; } = 20;

        public int? Total { get; set; }

        public Guid? ScheduledId { get; set; }

        public Stack<Guid> UnprocessedPlaneIds { get; set; } = new Stack<Guid>();

        public Dictionary<Guid, Guid> ProcessingPlanesIds { get; set; } = new Dictionary<Guid, Guid>(); // CorrelationId, PlaneId
        public List<JobState> Jobs { get; set; } = new List<JobState>();
    }
}