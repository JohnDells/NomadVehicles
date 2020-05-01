using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NomadCodeTestBusiness;

namespace NomadVehicles.Pages.Vehicles
{
    public class CreateModel : PageModel
    {
        private readonly IVehicleManager _manager;
        public CreateModel(IVehicleManager manager)
        {
            _manager = manager;
        }

        [BindProperty]
        public VehicleDto Vehicle { get; set; }

        public List<SelectListItem> VehicleTypes { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var vehicleTypes = await _manager.GetVehicleTypes();
            VehicleTypes = vehicleTypes.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            Vehicle = new VehicleDto { Id = Guid.NewGuid(), Name = "", VehicleTypeId = vehicleTypes.Select(x => x.Id).FirstOrDefault() };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _manager.SaveVehicle(Vehicle.Id, Vehicle.Name, Vehicle.VehicleTypeId);

            return RedirectToPage("./Index");
        }
    }
}