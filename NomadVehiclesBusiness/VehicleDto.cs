using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NomadCodeTestBusiness
{
    public class VehicleDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Must be 50 or less characters long.")]
        public string Name { get; set; }

        [Required]
        public Guid VehicleTypeId { get; set; }

        public string VehicleTypeName { get; set; }

        public Guid? VehicleImageId { get; set; }

        public List<IFormFile> FormFiles { get; set; }
    }
}