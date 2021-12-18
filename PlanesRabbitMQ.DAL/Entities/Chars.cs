using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanesRabbitMQ.DAL.Entities
{
    public class Chars:IEntityGenericKey<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public PlaneType Type { get; set; }
        public int NumberOfSeats { get; set; }
        public bool HasAmmunition { get; set; }
        public bool HasRadar { get; set; }
        public ScoutCharacteristics Characteristics { get; set; }
    }
}