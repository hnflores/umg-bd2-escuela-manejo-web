using ESC_MANEJO.CORE.Entities.Administrator;
using ESC_MANEJO.CORE.Entities.Configuration;
using ESC_MANEJO.CORE.Entities.Response;
using ESC_MANEJO.CORE.Enumerations;
using ESC_MANEJO.CORE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESC_MANEJO.WEB.Controllers
{
    public class DriverController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ILogService _logService;
        private readonly ConfigurationMessages _messagesDefault;

        public DriverController(IAdminService adminService, ILogService logService,
            IOptions<ConfigurationMessages> messagesDefault)
        {
            _adminService = adminService;
            _logService = logService;
            _messagesDefault = messagesDefault.Value;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            Response<List<User>> drivers = await _adminService.GetDrivers();
            if (drivers.Code == ResponseCode.FatalError)
            {
                return Redirect(Url.Action("Error", "Home", new { Id = 500 }));
            }
            return View(drivers.Data);
        }
    }
}
