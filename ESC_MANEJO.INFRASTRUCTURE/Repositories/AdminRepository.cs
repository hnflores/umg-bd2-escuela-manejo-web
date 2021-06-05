using ESC_MANEJO.CORE.Entities;
using ESC_MANEJO.CORE.Entities.Administrator;
using ESC_MANEJO.CORE.Entities.Configuration;
using ESC_MANEJO.CORE.Entities.Response;
using ESC_MANEJO.CORE.Entities.Services;
using ESC_MANEJO.CORE.Enumerations;
using ESC_MANEJO.CORE.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESC_MANEJO.INFRASTRUCTURE.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IHttpService _httpService;
        private readonly ILogService _logService;
        private readonly IParseService _parseService;
        private readonly ConfigurationMessages _messagesDefault;
        private readonly ConfigurationRepository _configurationRepository;
        public AdminRepository(IHttpService httpService, ILogService logService, IOptions<ConfigurationMessages> messagesDefault
            , IOptions<ConfigurationRepository> configurationRepository, IParseService parseService)
        {
            _httpService = httpService;
            _logService = logService;
            _messagesDefault = messagesDefault.Value;
            _configurationRepository = configurationRepository.Value;
            _parseService = parseService;
        }
        public async Task<Response<string>> LogIn(User request)
        {
            Response<string> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodLogin,
                    Body = _parseService.Serialize(request),
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<string>> httpResponse = await _httpService.POST<Response<string>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(LogIn)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;

        }
        public async Task<Response<List<User>>> GetDrivers()
        {
            Response<List<User>> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodGetDrivers,
                    Body = string.Empty,
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<List<User>>> httpResponse = await _httpService.POST<Response<List<User>>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(GetDrivers)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;

        }

        public async Task<Response<List<Vehicle>>> GetVehicles()
        {
            Response<List<Vehicle>> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodGetVehicles,
                    Body = string.Empty,
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<List<Vehicle>>> httpResponse = await _httpService.POST<Response<List<Vehicle>>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(GetVehicles)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;

        }
        public async Task<Response<Vehicle>> GetVehicle(Vehicle request)
        {
            Response<Vehicle> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodGetVehicle,
                    Body = _parseService.Serialize(request),
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<Vehicle>> httpResponse = await _httpService.POST<Response<Vehicle>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(GetVehicle)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;

        }
        public async Task<Response<string>> AddVehicle(Vehicle request)
        {
            Response<string> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodAddVehicle,
                    Body = _parseService.Serialize(request),
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<string>> httpResponse = await _httpService.POST<Response<string>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(AddVehicle)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;

        }
        public async Task<Response<string>> UpdateVehicle(Vehicle request)
        {
            Response<string> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodUpdateVehicle,
                    Body = _parseService.Serialize(request),
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<string>> httpResponse = await _httpService.POST<Response<string>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(UpdateVehicle)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;

        }

        public async Task<Response<string>> DeleteVehicle(Vehicle request)
        {
            Response<string> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodDeleteVehicle,
                    Body = _parseService.Serialize(request),
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<string>> httpResponse = await _httpService.POST<Response<string>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(DeleteVehicle)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;

        }

        public async Task<Response<List<Customer>>> GetCustomers()
        {
            Response<List<Customer>> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodGetCustomers,
                    Body = string.Empty,
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<List<Customer>>> httpResponse = await _httpService.POST<Response<List<Customer>>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(GetCustomers)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;

        }
        public async Task<Response<Customer>> GetCustomerById(Customer request)
        {
            Response<Customer> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodGetGetCustomerById,
                    Body = _parseService.Serialize(request),
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<Customer>> httpResponse = await _httpService.POST<Response<Customer>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(GetCustomerById)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;

        }

        public async Task<Response<string>> AddContract(Contract request)
        {
            Response<string> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodAddContract,
                    Body = _parseService.Serialize(request),
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<string>> httpResponse = await _httpService.POST<Response<string>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(AddContract)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;
        }
        public async Task<Response<List<Contract>>> GetContracts()
        {
            Response<List<Contract>> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodGetContracts,
                    Body = string.Empty,
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<List<Contract>>> httpResponse = await _httpService.POST<Response<List<Contract>>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(GetContracts)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;

        }

        public async Task<Response<Contract>> GetContract(Contract request)
        {
            Response<Contract> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodGetContract,
                    Body = _parseService.Serialize(request),
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<Contract>> httpResponse = await _httpService.POST<Response<Contract>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(GetContract)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;

        }
        public async Task<Response<string>> DeleteContract(Contract request)
        {
            Response<string> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodDeleteContract,
                    Body = _parseService.Serialize(request),
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<string>> httpResponse = await _httpService.POST<Response<string>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(DeleteContract)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;
        }
        public async Task<Response<string>> UpdateContract(Contract request)
        {
            Response<string> responseAPI = new();
            try
            {
                HttpServiceRequest httpRequest = new()
                {
                    Uri = new Uri(_configurationRepository.Admin.Url),
                    Method = _configurationRepository.Admin.MethodUpdateContract,
                    Body = _parseService.Serialize(request),
                    Headers = new Dictionary<string, string>
                {
                    { "Key-Auth", _configurationRepository.Admin.KeyAuth }
                }
                };
                HttpServiceResponse<Response<string>> httpResponse = await _httpService.POST<Response<string>>(httpRequest);
                responseAPI.Code = httpResponse.Code;
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    if (httpResponse.Code == ResponseCode.Error)
                        responseAPI.Description = httpResponse.Description;
                    else if (httpResponse.Code == ResponseCode.FatalError)
                        responseAPI.Description = _messagesDefault.FatalErrorMessage;
                    else if (httpResponse.Code == ResponseCode.Timeout)
                        responseAPI.Description = _messagesDefault.TimeoutMessage;
                }
            }
            catch (Exception ex)
            {
                responseAPI.Code = ResponseCode.FatalError;
                responseAPI.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(UpdateContract)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);

            }
            return responseAPI;
        }
    }
}
