using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.DTO
{
    public class HouseDTO : BaseDTO
    {
        /// <summary>
        /// 区域Id
        /// </summary>
        public long RegionId { get; set; }
        public string RegionName { get; set; }
        public int? BuiltYear { get; set; }
        public string Traffic { get; set; }
        /// <summary>
        /// 小区ID
        /// </summary>
        public long CommunityId { get; set; }
        public string CommunityName { get; set; }
        /// <summary>
        /// 户型类型ID
        /// </summary>
        public long RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public string Address { get; set; }
        public int MonthRent { get; set; }
        /// <summary>
        /// 房屋状态ID
        /// </summary>
        public long StatusId { get; set; }

        public string StatusName { get; set; }
        public double Area { get; set; }
        /// <summary>
        /// 装修状态
        /// </summary>
        public long DecorateStatusId { get; set; }
        public string DecorateStatusName { get; set; }
        public int TotalFloatCount { get; set; }
        public int FloorIndex { get; set; }
        /// <summary>
        /// 房屋类别
        /// </summary>
        public long TypeId { get; set; }
        public string TypeName { get; set; }
        public string Direction { get; set; }
        public DateTime LookableDateTime { get; set; }
        public DateTime CheckInDateTime { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhoneNum { get; set; }
        public string Description { get; set; }
    }
}
