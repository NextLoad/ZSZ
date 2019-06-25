using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.AdminWeb.Models
{
    public class CommunitityListViewModel
    {
        public long RegionId { get; set; }
        public CommunitityDTO[] Communitities { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public long TotalCount { get; set; }
    }
}