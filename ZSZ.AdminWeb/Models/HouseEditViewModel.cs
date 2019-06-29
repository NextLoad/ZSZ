using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.AdminWeb.Models
{
    public class HouseEditViewModel
    {
        public HouseDTO House { get; set; }
        public RegionDTO[] Regions { get; set; }
        public CommunitityDTO[] Communities { get; set; }
        public IdNameDTO[] RoomTypes { get; set; }
        public IdNameDTO[] Status { get; set; }
        public IdNameDTO[] DecorateStatus { get; set; }
        public IdNameDTO[] Types { get; set; }
    }
}