using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.AdminWeb.Models
{
    public class CommunitityGetViewModel
    {
        public CommunitityDTO Communitity { get; set; }
        public RegionDTO[] Regions { get; set; }
    }
}