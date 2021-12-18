namespace PlanesRabbitMQ.WebApi.Publisher.Internal
{
    public class AppConfig
    {
        public RabbitMqSettings RabbitMq { get; set; }
    }

    public class RabbitMqSettings
    {
        public string HostAddress { get; set; }
        public int Port { get; private set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}