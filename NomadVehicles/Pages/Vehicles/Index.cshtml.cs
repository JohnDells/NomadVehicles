using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NomadCodeTestBusiness;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NomadVehicles.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly IVehicleManager _manager;
        private const int PageSize = 10;

        public IndexModel(IVehicleManager manager)
        {
            _manager = manager;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public List<VehicleDto> Vehicles { get; set; } = new List<VehicleDto>();
        public int PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var skip = (PageNumber - 1) * PageSize;
            Vehicles = await _manager.GetVehicles(skip, PageSize, SearchString);
            var vehicleCount = await _manager.GetVehicleCount(SearchString);
            PageCount = ((vehicleCount - 1) / PageSize) + 1;

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _manager.DeleteVehicle(id);

            return RedirectToPage("./Index");

        }
    }
}