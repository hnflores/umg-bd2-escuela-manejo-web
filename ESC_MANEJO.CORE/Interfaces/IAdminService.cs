using ESC_MANEJO.CORE.Entities.Administrator;
using ESC_MANEJO.CORE.Entities.Response;
using System.Threading.Tasks;

namespace ESC_MANEJO.CORE.Interfaces
{
    public interface IAdminService
    {
        Task<Response<string>> LogIn(User request);
    }
}
