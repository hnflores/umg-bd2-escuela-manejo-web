using Newtonsoft.Json;
using ESC_MANEJO.CORE.Interfaces;

namespace ESC_MANEJO.WEB.Services
{
    public class ParseService : IParseService
    {
        public T Deserealize<T>(dynamic model)
        {
            return JsonConvert.DeserializeObject<T>(model);
        }
        public string Serialize(dynamic model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
