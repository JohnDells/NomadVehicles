using Microsoft.AspNetCore.Mvc;
using NomadCodeTestBusiness;
using System;
using System.Threading.Tasks;

namespace NomadVehicles.Controllers
{
    public class VehicleImageController : Controller
    {
        private readonly IVehicleManager _manager;

        public VehicleImageController(IVehicleManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            if (id != Guid.Empty)
            {
                var content = await _manager.GetVehicleImage(id);
                if (content != null)
                {
                    return File(content, "image/jpg");
                }

                return BadRequest();
            }

            return NotFound();
        }
    }
}