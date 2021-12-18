using System.Xml.Serialization;

namespace PlanesRabbitMQ.DAL.Entities
{
    public enum PlaneType
    {
        [XmlEnum("support")]
        Support,
        [XmlEnum("convoy")]
        Convoy,
        [XmlEnum("destoyer")]
        Destroyer,
        [XmlEnum("interceptor")]
        Interceptor,
        [XmlEnum("scout")]
        Scout,
    }
}