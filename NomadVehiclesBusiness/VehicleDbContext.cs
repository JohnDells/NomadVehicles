using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NomadCodeTestBusiness
{
    public class VehicleDbContext : DbContext, IVehicleDbContext
    {
        private readonly string _connectionString;

        public VehicleDbContext(IConfiguration config)
        {
            _connectionString = config["ConnectionStrings:VehicleConnection"];
        }

        public DbSet<VehicleTypeEntity> VehicleTypes { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<VehicleImageEntity> VehicleImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}