using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NomadCodeTestBusiness
{
    [Table("Vehicle")]
    public class VehicleEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public Guid VehicleTypeId { get; set; }

        [ForeignKey("VehicleTypeId")]
        public VehicleTypeEntity VehicleType { get; set; }

        public ICollection<VehicleImageEntity> VehicleImages { get; set; }
    }
}