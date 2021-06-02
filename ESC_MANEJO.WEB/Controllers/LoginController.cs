using ESC_MANEJO.CORE.Entities.Administrator;
using ESC_MANEJO.CORE.Entities.Configuration;
using ESC_MANEJO.CORE.Entities.Response;
using ESC_MANEJO.CORE.Enumerations;
using ESC_MANEJO.CORE.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ESC_MANEJO.WEB.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ILogService _logService;
        private readonly ConfigurationMessages _messagesDefault;
        public LoginController(IAdminService adminService, ILogService logService,
            IOptions<ConfigurationMessages> messagesDefault)
        {
            _adminService = adminService;
            _logService = logService;
            _messagesDefault = messagesDefault.Value;           
        }
        public IActionResult Index()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        public virtual async Task<JsonResult> LogIn([FromBody] User user)
        {
            Response<string> response = new();
            try
            {                
                response = await _adminService.LogIn(user);                
            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.FatalError;
                response.Description = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(LogIn)},{nameof(LoginController)}-{nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return Json(response);
        }

    }
}
