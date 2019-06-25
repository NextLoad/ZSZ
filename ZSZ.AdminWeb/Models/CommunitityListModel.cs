using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZSZ.AdminWeb.Models
{
    public class CommunitityListModel
    {
        public int PageIndex { get; set; } = 1;
        public int PageCount { get; set; } = 10;
        public long TotalCount { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Name { get; set; }
        public long RegionId { get; set; } = 1;
    }
}