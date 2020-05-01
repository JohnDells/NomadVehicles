using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NomadCodeTestBusiness
{
    public class VehicleEfRepository : IVehicleRepository
    {
        private readonly IVehicleDbContext _context;

        public VehicleEfRepository(IVehicleDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleTypeDto>> GetVehicleTypes()
        {
            return await _context.VehicleTypes.OrderBy(x => x.Name).Select(x => new VehicleTypeDto { Id = x.Id, Name = x.Name }).ToListAsync();
        }

        public async Task<int> GetVehicleCount(string startsWith = null)
        {
            IQueryable<VehicleEntity> query = _context.Vehicles;
            if (!string.IsNullOrEmpty(startsWith))
            {
                query = query.Where(x => x.Name.StartsWith(startsWith));
            }

            return await query.CountAsync();
        }

        public async Task<List<VehicleDto>> GetVehicles(int skip, int take, string startsWith = null)
        {
            IQueryable<VehicleEntity> query = _context.Vehicles;
            if (!string.IsNullOrEmpty(startsWith))
            {
                query = query.Where(x => x.Name.StartsWith(startsWith));
            }

            return await query.OrderBy(x => x.Name).Skip(skip).Take(take).Select(x => new VehicleDto { Id = x.Id, Name = x.Name, VehicleTypeName = x.VehicleType.Name, VehicleImageIds = x.VehicleImages.Select(y => y.Id).ToList() }).ToListAsync();
        }

        public async Task<VehicleDto> GetVehicle(Guid id)
        {
            return await _context.Vehicles.Where(x => x.Id == id).Select(x => new VehicleDto { Id = x.Id, Name = x.Name, VehicleTypeId = x.VehicleTypeId, VehicleImageIds = x.VehicleImages.Select(y => y.Id).ToList() }).FirstOrDefaultAsync();
        }

        public async Task SaveVehicle(Guid id, string name, Guid vehicleTypeId)
        {
            var item = await _context.Vehicles.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item == null)
            {
                item = new VehicleEntity { Id = id };
                _context.Vehicles.Add(item);
            }

            item.Name = name;
            item.VehicleTypeId = vehicleTypeId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteVehicle(Guid id)
        {
            var vehicleImages = await _context.VehicleImages.Where(x => x.VehicleId == id).ToListAsync();
            if (vehicleImages != null)
            {
                foreach (var vehicleImage in vehicleImages)
                {
                    _context.VehicleImages.Remove(vehicleImage);
                }
                await _context.SaveChangesAsync();
            }

            var item = await _context.Vehicles.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item != null)
            {
                _context.Vehicles.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveVehicleImage(Guid id, Guid vehicleId, byte[] content)
        {
            var item = await _context.VehicleImages.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item == null)
            {
                item = new VehicleImageEntity { Id = id, VehicleId = vehicleId };
                _context.VehicleImages.Add(item);
            }

            item.Content = content;
            await _context.SaveChangesAsync();
        }

        public async Task<byte[]> GetVehicleImage(Guid id)
        {
            return await _context.VehicleImages.Where(x => x.Id == id).Select(x => x.Content).FirstOrDefaultAsync();
        }
    }
}