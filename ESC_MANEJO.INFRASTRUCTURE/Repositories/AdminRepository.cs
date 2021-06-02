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
                if (httpResponse.Code == ResponseCode.Success)
                    responseAPI = httpResponse.Data;
                else
                {
                    responseAPI.Code = httpResponse.Code;
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
    }
}
