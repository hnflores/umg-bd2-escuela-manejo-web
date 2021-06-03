using ESC_MANEJO.CORE.Entities;
using ESC_MANEJO.CORE.Entities.Configuration;
using ESC_MANEJO.CORE.Entities.Response;
using ESC_MANEJO.CORE.Enumerations;
using ESC_MANEJO.CORE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESC_MANEJO.WEB.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ILogService _logService;
        private readonly ConfigurationMessages _messagesDefault;

        public VehicleController(IAdminService adminService, ILogService logService,
            IOptions<ConfigurationMessages> messagesDefault)
        {
            _adminService = adminService;
            _logService = logService;
            _messagesDefault = messagesDefault.Value;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            Response<List<Vehicle>> vehicles = await _adminService.GetVehicles();
            if (vehicles.Code == ResponseCode.FatalError)
            {
                return RedirectToAction();
            }
            return View(vehicles.Data);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            string vehicleId = TempData["VehicleId"] as string;
            TempData["VehicleId"] = vehicleId;

            Response<Vehicle> vehicle = await _adminService.GetVehicle(vehicleId);
            if (vehicle.Code == ResponseCode.FatalError)
            {
                return RedirectToAction();
            }
            return View(vehicle.Data);
        }

        public virtual JsonResult SetIdEdit([FromBody] string id)
        {
            TempData["VehicleId"] = id;
            return Json(Url.Action("Edit", "Vehicle"));
        }



        [ValidateAntiForgeryToken]
        public virtual async Task<JsonResult> AddVehicle([FromBody] Vehicle request) => Json(await _adminService.AddVehicle(request));

        [ValidateAntiForgeryToken]
        public virtual async Task<JsonResult> UpdateVehicle([FromBody] Vehicle request) => Json(await _adminService.UpdateVehicle(request));

    }
}
