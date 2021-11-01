using System;
using System.Xml.Serialization;

namespace PlanesRabbitMQ.BL.Models
{
    public class Plane: IComparable<Plane>
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("model")]
        public System.String Model { get; set; } = "";

        [XmlElement("origin")]
        public System.String Origin { get; set; } = "";

        [XmlElement("chars")]
        public Chars Chars { get; set; } = new Chars();

        [XmlElement("parameters")]
        public Parameters Parameters { get; set; } = new Parameters();
        
        [XmlElement("price")]
        public double Price { get; set; }

        public int CompareTo(Plane other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Price.CompareTo(other.Price);
        }

        public override string ToString()
        {
            return
                $"Plane: Id = {Id}, Model = {Model}, Origin = {Origin}, Price ={Price},{Environment.NewLine}s Chars= {Chars}{Environment.NewLine},Parameters = {Parameters}{Environment.NewLine}";
        }
    }
}