using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanesRabbitMQ.DAL.Entities
{
    public class ParametersCharacteristics:IEntityGenericKey<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public System.Double Length { get; set; }
        public System.Double Width { get; set; }
        public System.Double Height { get; set; }
    }
}