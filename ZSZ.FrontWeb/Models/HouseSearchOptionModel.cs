using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZSZ.FrontWeb.Models
{
    public class HouseSearchOptionModel
    {
        public long TypeId { get; set; }
        public long? RegionId { get; set; }
        public string MonthRent { get; set; }
        public string OrderByType { get; set; }
        public string KeyWords { get; set; }


    }
}