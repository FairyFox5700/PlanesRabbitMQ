using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace PlanesRabbitMQ.Components.Helpers
{
    public class JsonValueConverter<T> : ValueConverter<T, string> where T : class
    {
        public JsonValueConverter(ConverterMappingHints hints = default) :
            base(v => JsonConvert.SerializeObject(v),
                v =>JsonConvert.DeserializeObject<T>(v), hints)
        { }
    }
}