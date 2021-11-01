using System.Xml.Serialization;

namespace PlanesRabbitMQ.BL.Models
{
    public class Parameters
    {
        [XmlElement("length")]
        public System.Double Length { get; set; }

        [XmlElement("width")]
        public System.Double Width { get; set; }

        [XmlElement("height")]
        public System.Double Height { get; set; }

        public override string ToString()
        {
            return $"Parameters: Length = {Length}, Width = {Width}, Height = {Height}";
        }
    }
}