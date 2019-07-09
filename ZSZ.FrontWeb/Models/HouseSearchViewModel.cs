using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.FrontWeb.Models
{
    public class HouseSearchViewModel
    {
        public HouseDTO[] Houses { get; set; }
        public RegionDTO[] Regions { get; set; }
        public CityDTO City { get; set; }
    }
}