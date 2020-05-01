using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NomadCodeTestBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NomadVehicles.Pages.Vehicles
{
    public class EditModel : PageModel
    {
        private readonly IVehicleManager _manager;

        public EditModel(IVehicleManager manager)
        {
            _manager = manager;
        }

        [BindProperty]
        public VehicleDto Vehicle { get; set; }

        public List<SelectListItem> VehicleTypes { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle = await _manager.GetVehicle((Guid)id);

            if (Vehicle == null)
            {
                return NotFound();
            }

            var vehicleTypes = await _manager.GetVehicleTypes();
            VehicleTypes = vehicleTypes.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = (x.Id == Vehicle.VehicleTypeId) }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _manager.SaveVehicle(Vehicle.Id, Vehicle.Name, Vehicle.VehicleTypeId, null);

            return RedirectToPage("./Index");
        }
    }
}