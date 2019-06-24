using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.AdminWeb.Models
{
    public class RegionListViewModel
    {
        public long CityId { get; set; }
        public RegionDTO[] Regions { get; set; }
        public long TotalCount { get; set; }
        public int PageIndex { get; set; }

        public int PageCount { get; set; }
      
    }
}