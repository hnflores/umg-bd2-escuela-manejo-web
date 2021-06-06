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
using System;
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
                return Redirect(Url.Action("Error", "Home", new { Id = 500 }));
            }
            drivers.Data = drivers.Data.Where(x => x.Estado == "A").ToList();
            customers.Data = customers.Data.Where(x => x.Estado == "A").ToList();
            vehicles.Data = vehicles.Data.Where(x => x.Estado == "A").ToList();
            view.Customers = customers.Data; view.Drivers = drivers.Data; view.Vehicles = vehicles.Data;
            return View(view);
        }

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            ViewEditContract view = new();
            int contractId = Convert.ToInt32(TempData["ContractId"] as string);
            TempData["ContractId"] = contractId.ToString();
            Response<List<Customer>> customers = await _adminService.GetCustomers();
            Response<List<User>> drivers = await _adminService.GetDrivers();
            Response<List<Vehicle>> vehicles = await _adminService.GetVehicles();
            Response<Contract> contract = await _adminService.GetContract(contractId);
            if (customers.Code == ResponseCode.FatalError || drivers.Code == ResponseCode.FatalError || vehicles.Code == ResponseCode.Error || contract.Code == ResponseCode.FatalError)
            {
                return Redirect(Url.Action("Error", "Home", new { Id = 500 }));
            }
            drivers.Data = drivers.Data.Where(x => x.Estado == "A" || x.ColaboradorId == contract.Data.UserId).ToList();
            customers.Data = customers.Data.Where(x => x.Estado == "A").ToList();
            vehicles.Data = vehicles.Data.Where(x => x.Estado == "A" || x.VehicleId == contract.Data.VehicleId).ToList();
            view.Customers = customers.Data; view.Drivers = drivers.Data; view.Vehicles = vehicles.Data; view.Contract = contract.Data;
            return View(view);
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            Response<List<Contract>> contracts = await _adminService.GetContracts();
            if (contracts.Code == ResponseCode.FatalError)
            {
                return Redirect(Url.Action("Error", "Home", new { Id = 500 }));
            }
            return View(contracts.Data);
        }

        [Authorize]
        public virtual JsonResult SetIdEdit([FromBody] int id)
        {
            TempData["ContractId"] = id.ToString();
            return Json(Url.Action("Edit", "Contract"));
        }


        [ValidateAntiForgeryToken]
        [Authorize]
        public virtual async Task<JsonResult> AddContract([FromBody] Contract request) => Json(await _adminService.AddContract(request));

        [ValidateAntiForgeryToken]
        [Authorize]
        public virtual async Task<JsonResult> UpdateContract([FromBody] Contract request)
        {
            int contractId = Convert.ToInt32(TempData["ContractId"] as string);
            TempData["ContractId"] = contractId.ToString();
            request.ContractId = contractId;
            return Json(await _adminService.UpdateContract(request));
        }
        [Authorize]
        public virtual async Task<JsonResult> DeleteContract([FromBody] int contractId) => Json(await _adminService.DeleteContract(contractId));
    }
}
