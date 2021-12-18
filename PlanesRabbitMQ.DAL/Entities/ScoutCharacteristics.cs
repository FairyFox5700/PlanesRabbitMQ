using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanesRabbitMQ.DAL.Entities
{
    public class ScoutCharacteristics: IEntityGenericKey<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RocketsCount { get; set; }
    }
}