using System;

namespace PlanesRabbitMQ.Components.Activities.Arguments
{
    public class CancelPlanesArguments
    {
        public Guid PlaneId { get; }
        public string Reason { get; }
    }
}