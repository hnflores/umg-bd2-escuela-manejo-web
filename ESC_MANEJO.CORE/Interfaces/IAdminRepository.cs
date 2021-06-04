using ESC_MANEJO.CORE.Entities;
using ESC_MANEJO.CORE.Entities.Administrator;
using ESC_MANEJO.CORE.Entities.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESC_MANEJO.CORE.Interfaces
{
    public interface IAdminRepository
    {
        Task<Response<string>> LogIn(User request);
        Task<Response<List<User>>> GetDrivers();
        Task<Response<List<Vehicle>>> GetVehicles();
        Task<Response<Vehicle>> GetVehicle(Vehicle request);
        Task<Response<string>> AddVehicle(Vehicle request);
        Task<Response<string>> UpdateVehicle(Vehicle request);

        Task<Response<List<Customer>>> GetCustomers();
        Task<Response<Customer>> GetCustomerById(Customer request);
        Task<Response<string>> AddContract(Contract request);
        Task<Response<List<Contract>>> GetContracts();
    }
}
