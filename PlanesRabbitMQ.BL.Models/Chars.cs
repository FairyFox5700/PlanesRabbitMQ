using System.Xml.Serialization;
using PlanesRabbitMQ.DAL.Entities;

namespace PlanesRabbitMQ.BL.Models
{
    public class Chars
    {
        [XmlAttribute("type")]
        public PlaneType Type { get; set; }

        [XmlElement("numberOfSeats", IsNullable = false)]
        public int NumberOfSeats { get; set; }
        
        [XmlElement("hasAmmunition", IsNullable = false)]
        public bool HasAmmunition { get; set; }

        [XmlElement("hasRadar", IsNullable = false)]
        public bool HasRadar { get; set; }
        
        [XmlElement("characteristics", IsNullable = false)]
        public ScoutCharacteristics Characteristics { get; set; } = new ScoutCharacteristics();

        public override string ToString()
        {
            return
                $"Chars:Number of seats{NumberOfSeats},Has radar= {HasRadar}, Has ammunition = {HasAmmunition}, Type = {Type}, " +
                $"Characteristics= {Characteristics}";
        }
    }
}