using System.Collections.Generic;
using PlanesRabbitMQ.DAL.Entities;

namespace PlanesRabbitMQ.BL.Services.Impl
{
    public interface IXmlPlanesParser
    {
        List<Models.Plane> ParsePlanes();
    }
}