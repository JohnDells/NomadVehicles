using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NomadCodeTestBusiness;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

            var files = Vehicle.FormFiles;
            var vehicleImages = await GetImagesFromStream(files);

            await _manager.SaveVehicle(Vehicle.Id, Vehicle.Name, Vehicle.VehicleTypeId, vehicleImages);

            return RedirectToPage("./Index");
        }

        public static async Task<List<byte[]>> GetImagesFromStream(List<IFormFile> files)
        {
            List<byte[]> result = null;
            if (files != null)
            {
                result = new List<byte[]>();
                foreach (var file in files)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        var content = stream.ToArray();
                        result.Add(content);
                    }
                }
            }
            return result;
        }
    }
}