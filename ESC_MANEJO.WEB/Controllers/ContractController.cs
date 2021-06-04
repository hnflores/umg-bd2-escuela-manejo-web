using ESC_MANEJO.CORE.Entities;
using ESC_MANEJO.CORE.Entities.Administrator;
using ESC_MANEJO.CORE.Entities.Configuration;
using ESC_MANEJO.CORE.Entities.Response;
using ESC_MANEJO.CORE.Enumerations;
using ESC_MANEJO.CORE.Interfaces;
using ESC_MANEJO.WEB.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESC_MANEJO.WEB.Controllers
{
    public class ContractController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ILogService _logService;
        private readonly ConfigurationMessages _messagesDefault;

        public ContractController(IAdminService adminService, ILogService logService,
            IOptions<ConfigurationMessages> messagesDefault)
        {
            _adminService = adminService;
            _logService = logService;
            _messagesDefault = messagesDefault.Value;
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            ViewNewContract view = new();

            Response<List<Customer>> customers = await _adminService.GetCustomers();
            Response<List<User>> drivers = await _adminService.GetDrivers();
            Response<List<Vehicle>> vehicles = await _adminService.GetVehicles();
            if (customers.Code == ResponseCode.FatalError || drivers.Code == ResponseCode.FatalError || vehicles.Code == ResponseCode.Error)
            {
                return RedirectToAction();
            }
            drivers.Data = drivers.Data.Where(x => x.Estado == "A").ToList();
            customers.Data = customers.Data.Where(x => x.Estado == "A").ToList();
            vehicles.Data = vehicles.Data.Where(x => x.Estado == "A").ToList();
            view.Customers = customers.Data; view.Drivers = drivers.Data; view.Vehicles = vehicles.Data;
            return View(view);
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            Response<List<Contract>> contracts = await _adminService.GetContracts();
            if (contracts.Code == ResponseCode.FatalError)
            {
                return RedirectToAction();
            }
            return View(contracts.Data);
        }


        [ValidateAntiForgeryToken]
        [Authorize]
        public virtual async Task<JsonResult> AddContract([FromBody] Contract request) => Json(await _adminService.AddContract(request));
    }
}
