using ESC_MANEJO.CORE.Entities.Response;

namespace ESC_MANEJO.CORE.Entities.Services
{
    public class HttpServiceResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
