using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NomadCodeTestBusiness
{
    public class VehicleManager : IVehicleManager
    {
        private readonly IVehicleRepository _repository;

        public VehicleManager(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<VehicleTypeDto>> GetVehicleTypes()
        {
            return await _repository.GetVehicleTypes();
        }

        public async Task<int> GetVehicleCount(string startsWith = null)
        {
            return await _repository.GetVehicleCount(startsWith);
        }

        public async Task<List<VehicleDto>> GetVehicles(int skip, int take, string startsWith = null)
        {
            return await _repository.GetVehicles(skip, take, startsWith);
        }

        public async Task<VehicleDto> GetVehicle(Guid id)
        {
            return await _repository.GetVehicle(id);
        }

        public async Task SaveVehicle(Guid id, string name, Guid vehicleTypeId, List<byte[]> images)
        {
            await _repository.SaveVehicle(id, name, vehicleTypeId);

            if (images != null)
            {
                foreach (var image in images)
                {
                    await _repository.SaveVehicleImage(Guid.NewGuid(), id, image);
                }
            }
        }

        public async Task DeleteVehicle(Guid id)
        {
            await _repository.DeleteVehicle(id);
        }

        public async Task<byte[]> GetVehicleImage(Guid id)
        {
            return await _repository.GetVehicleImage(id);
        }
    }
}