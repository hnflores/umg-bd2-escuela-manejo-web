using System.Threading.Tasks;
using ESC_MANEJO.CORE.Entities.Services;

namespace ESC_MANEJO.CORE.Interfaces
{
    public interface IHttpService
    {
        Task<HttpServiceResponse<T>> POST<T>(HttpServiceRequest request);
    }
}
