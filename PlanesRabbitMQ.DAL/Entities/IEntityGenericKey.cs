namespace PlanesRabbitMQ.DAL.Entities
{
    public interface IEntityGenericKey<TKey>
    {
        public TKey Id { get; set; }
    }
}