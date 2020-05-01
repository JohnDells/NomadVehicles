using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace NomadCodeTestBusiness
{
    public interface IVehicleDbContext
    {
        DbSet<VehicleTypeEntity> VehicleTypes { get; set; }
        DbSet<VehicleEntity> Vehicles { get; set; }
        DbSet<VehicleImageEntity> VehicleImages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}