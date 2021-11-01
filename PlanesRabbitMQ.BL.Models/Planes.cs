using System.Xml.Serialization;

namespace PlanesRabbitMQ.BL.Models
{
    [XmlRoot("planes", Namespace = "http://www.mydomain/planes",IsNullable = false)]
    public partial class Planes
    {
        [XmlElement("plane")]
        public Plane[] Plane { get; set; }
    }
}