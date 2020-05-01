using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NomadCodeTestBusiness
{
    [Table("VehicleType")]
    public class VehicleTypeEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}