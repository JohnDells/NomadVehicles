using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NomadCodeTestBusiness
{
    [Table("VehicleImage")]
    public class VehicleImageEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid VehicleId { get; set; }

        public byte[] Content { get; set; }

        [ForeignKey("VehicleId")]
        public VehicleEntity Vehicle { get; set; }
    }
}