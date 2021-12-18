namespace PlanesRabbitMQ.BL.Services.Requests
{
        public record PostBatchPlanesRequest
        {
            public  int PlanesCount { get; set; }
        }
}