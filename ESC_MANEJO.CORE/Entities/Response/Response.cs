namespace ESC_MANEJO.CORE.Entities.Response
{
    public class Response<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
