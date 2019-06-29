using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.AdminWeb.Models
{
    public class HouseListViewModel
    {
        public HouseDTO[] Housees { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public long TotalCount { get; set; }
    }
}