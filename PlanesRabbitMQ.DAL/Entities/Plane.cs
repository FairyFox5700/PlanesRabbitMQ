using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanesRabbitMQ.DAL.Entities
{
    public class Plane: IComparable<Plane>, IEntityGenericKey<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public System.String Model { get; set; }
        public System.String Origin { get; set; }
        public Chars Chars { get; set; } 
        public Parameters Parameters { get; set; } 
        public double Price { get; set; }

        public int CompareTo(Plane other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Price.CompareTo(other.Price);
        }
    }
}