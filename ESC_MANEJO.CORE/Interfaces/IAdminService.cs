using ESC_MANEJO.CORE.Entities;
using ESC_MANEJO.CORE.Entities.Administrator;
using ESC_MANEJO.CORE.Entities.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESC_MANEJO.CORE.Interfaces
{
    public interface IAdminService
    {
        Task<Response<string>> LogIn(User request);
        Task<Response<List<User>>> GetDrivers();

        Task<Response<List<Vehicle>>> GetVehicles();
        Task<Response<Vehicle>> GetVehicle(string vehicleId);
        Task<Response<string>> AddVehicle(Vehicle request);
        Task<Response<string>> UpdateVehicle(Vehicle request);
        Task<Response<string>> DeleteVehicle(string vehicleId);

        Task<Response<List<Customer>>> GetCustomers();
        Task<Response<Customer>> GetCustomerById(string customerId);
        Task<Response<string>> AddContract(Contract request);
        Task<Response<List<Contract>>> GetContracts();
        Task<Response<Contract>> GetContract(int contractId);
        Task<Response<string>> UpdateContract(Contract request);
        Task<Response<string>> DeleteContract(int contractId);
    }
}
