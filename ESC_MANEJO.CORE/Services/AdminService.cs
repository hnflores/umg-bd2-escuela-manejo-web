﻿using ESC_MANEJO.CORE.Entities.Administrator;
using ESC_MANEJO.CORE.Entities.Configuration;
using ESC_MANEJO.CORE.Entities.Response;
using ESC_MANEJO.CORE.Enumerations;
using ESC_MANEJO.CORE.Interfaces;
using Microsoft.Extensions.Options;
using System;
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
    }
}
