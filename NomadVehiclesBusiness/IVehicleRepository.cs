using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NomadCodeTestBusiness
{
    public interface IVehicleRepository
    {
        Task<List<VehicleTypeDto>> GetVehicleTypes();

        Task<int> GetVehicleCount(string startsWith = null);

        Task<List<VehicleDto>> GetVehicles(int skip, int take, string startsWith = null);

        Task<VehicleDto> GetVehicle(Guid id);

        Task SaveVehicle(Guid id, string name, Guid vehicleTypeId);

        Task DeleteVehicle(Guid id);
    }
}