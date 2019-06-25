using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZSZ.AdminWeb.Models
{
    public class CommunitityAddNewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public long RegionId { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Traffic { get; set; }

        public int? BuiltYear { get; set; }

    }
}