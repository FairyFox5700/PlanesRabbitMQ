using System;
using PlanesRabbitMQ.BL.Models;

namespace PlanesRabbitMQ.Contracts.Planes
{
    public interface PlaneBatchAddRequested
    {
        public  Guid BatchId { get; set; }
        public  Plane Plane { get; set; }
    }
}