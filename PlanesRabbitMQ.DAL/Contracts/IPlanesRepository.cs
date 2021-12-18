using System.Collections.Generic;
using System.Threading.Tasks;
using PlanesRabbitMQ.DAL.Entities;

namespace PlanesRabbitMQ.DAL.Impl
{
    public interface IPlanesRepository
    {
        Task BulkInsertPlanes(List<Plane> planesToInsert);
    }
}