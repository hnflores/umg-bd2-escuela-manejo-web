using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ESC_MANEJO.CORE.Entities.Configuration;
using ESC_MANEJO.CORE.Entities.Services;
using ESC_MANEJO.CORE.Enumerations;
using ESC_MANEJO.CORE.Interfaces;

namespace ESC_MANEJO.CORE.Services
{
    public class HttpService : IHttpService
    {

        private readonly IParseService _parseService;
        private readonly ILogService _logService;
        private readonly ConfigurationMessages _messagesDefault;
        public HttpService(IParseService parseService, IOptions<ConfigurationMessages> messagesDefault, ILogService logService)
        {
            _parseService = parseService;
            _messagesDefault = messagesDefault.Value;
            _logService = logService;
        }
        public async Task<HttpServiceResponse<T>> POST<T>(HttpServiceRequest request)
        {
            HttpServiceResponse<T> response = new HttpServiceResponse<T>();
            try
            {
                using var httpClient = new HttpClient
                {
                    BaseAddress = request.Uri
                };

                foreach (KeyValuePair<string, string> result in request.Headers)
                {
                    httpClient.DefaultRequestHeaders.Add(result.Key, result.Value);
                }

                StringContent content = new StringContent(request.Body, Encoding.UTF8, "application/json");

                using var responseAPI = await httpClient.PostAsync($"{request.Method}", content);
                string apiResponseString = await responseAPI.Content.ReadAsStringAsync();

                if (responseAPI.IsSuccessStatusCode)
                {
                    response.Code = ResponseCode.Success;
                    response.Data = _parseService.Deserealize<T>(apiResponseString);
                }
                else
                {
                    _logService.SaveLogApp($"[POST - HTTP CODE EXCEPTION] {apiResponseString}", LogType.Error);
                    response.Code = ResponseCode.FatalError;
                    response.Description = _messagesDefault.FatalErrorMessage;
                }
                
            }
            catch (TimeoutException ex)
            {
                _logService.SaveLogApp($"[POST - TimeoutException] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Code = ResponseCode.Timeout;
                response.Description = _messagesDefault.TimeoutMessage;
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[POST - Exception] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Code = ResponseCode.FatalError;
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }



    }
}
