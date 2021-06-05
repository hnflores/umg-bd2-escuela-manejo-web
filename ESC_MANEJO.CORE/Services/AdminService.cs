using ESC_MANEJO.CORE.Entities;
using ESC_MANEJO.CORE.Entities.Administrator;
using ESC_MANEJO.CORE.Entities.Configuration;
using ESC_MANEJO.CORE.Entities.Response;
using ESC_MANEJO.CORE.Enumerations;
using ESC_MANEJO.CORE.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESC_MANEJO.CORE.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ILogService _logService;
        private readonly ICryptoService _cryptoService;
        private readonly ConfigurationMessages _messagesDefault;

        public AdminService(ILogService logService, IOptions<ConfigurationMessages> messagesDefault
            , ICryptoService cryptoService, IAdminRepository adminRepository)
        {
            _logService = logService;
            _messagesDefault = messagesDefault.Value;
            _cryptoService = cryptoService;
            _adminRepository = adminRepository;
        }

        public async Task<Response<string>> LogIn(User request)
        {
            Response<string> responseAPI = new();
            try
            {
                responseAPI = await _adminRepository.LogIn(request);
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(LogIn)},{nameof(AdminService)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return responseAPI;
        }
        public async Task<Response<List<User>>> GetDrivers() => await _adminRepository.GetDrivers();

        public async Task<Response<List<Vehicle>>> GetVehicles() => await _adminRepository.GetVehicles();
        public async Task<Response<Vehicle>> GetVehicle(string vehicleId)
        {
            Response<Vehicle> responseAPI = new();
            try
            {
                responseAPI = await _adminRepository.GetVehicle(new Vehicle { VehicleId = vehicleId });
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(GetVehicle)},{nameof(AdminService)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return responseAPI;
        }
        public async Task<Response<string>> AddVehicle(Vehicle request) => await _adminRepository.AddVehicle(request);
        public async Task<Response<string>> UpdateVehicle(Vehicle request) => await _adminRepository.UpdateVehicle(request);
        public async Task<Response<string>> DeleteVehicle(string vehicleId) => await _adminRepository.DeleteVehicle(new Vehicle { VehicleId = vehicleId });

        public async Task<Response<List<Customer>>> GetCustomers() => await _adminRepository.GetCustomers();
        public async Task<Response<Customer>> GetCustomerById(string customerId)
        {
            Response<Customer> responseAPI = new();
            try
            {
                responseAPI = await _adminRepository.GetCustomerById(new Customer { CustomerId = customerId });
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(GetCustomerById)},{nameof(AdminService)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return responseAPI;
        }

        public async Task<Response<string>> AddContract(Contract request) => await _adminRepository.AddContract(request);
        public async Task<Response<List<Contract>>> GetContracts() => await _adminRepository.GetContracts();
        public async Task<Response<Contract>> GetContract(int contractId) => await _adminRepository.GetContract(new Contract { ContractId = contractId});
        public async Task<Response<string>> UpdateContract(Contract request) => await _adminRepository.UpdateContract(request);
        public async Task<Response<string>> DeleteContract(int contractId) => await _adminRepository.DeleteContract(new Contract { ContractId = contractId });

    }
}
