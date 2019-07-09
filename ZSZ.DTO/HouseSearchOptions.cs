using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.DTO
{
    public class HouseSearchOptions
    {

        public long TypeId { get; set; }
        public long? RegionId { get; set; }
        public long CityId { get; set; }
        public decimal? MonthRentStart { get; set; }
        public decimal? MonthRentEnd { get; set; }
        public HouseSearchOrderByType OrderByType { get; set; } = HouseSearchOrderByType.MonthRentAsc;
        public string KeyWords { get; set; }
    }
}
