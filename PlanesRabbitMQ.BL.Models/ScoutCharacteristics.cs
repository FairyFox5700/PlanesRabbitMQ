using System.Xml.Serialization;

namespace PlanesRabbitMQ.BL.Models
{
    public class ScoutCharacteristics
    {
        [XmlElement("rocketsCount")]
        public System.Numerics.BigInteger RocketsCount { get; set; }

        public override string ToString()
        {
            return $"Scout Characteristics: RocketsCount= {RocketsCount}";
        }
    }
}