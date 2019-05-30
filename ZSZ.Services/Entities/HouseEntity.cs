using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Services.Entities
{
    /// <summary>
    /// 房屋表
    /// </summary>
    public class HouseEntity : BaseEntity
    {
        /// <summary>
        /// 小区ID
        /// </summary>
        public long CommunityId { get; set; }
        public virtual CommunitityEntity CommunitityEntity { get; set; }
        /// <summary>
        /// 户型类型ID
        /// </summary>
        public long RoomTypeId { get; set; }
        public virtual IdNameEntity RoomType { get; set; }
        public string Address { get; set; }
        public int MonthRent { get; set; }
        /// <summary>
        /// 房屋状态ID
        /// </summary>
        public long StatusId { get; set; }

        public virtual IdNameEntity Status { get; set; }
        public double Area { get; set; }
        /// <summary>
        /// 装修状态
        /// </summary>
        public long DecorateStatusId { get; set; }
        public virtual IdNameEntity DecorateStatus { get; set; }
        public int TotalFloatCount { get; set; }
        public int FloorIndex { get; set; }
        /// <summary>
        /// 房屋类别
        /// </summary>
        public long TypeId { get; set; }
        public virtual IdNameEntity Type { get; set; }
        public string Direction { get; set; }
        public DateTime LookableDateTime { get; set; }
        public DateTime CheckInDateTime { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhoneNum { get; set; }
        public string Description { get; set; }
        public virtual ICollection<AttachmentEntity> AttachmentEntities { get; set; } = new List<AttachmentEntity>();

    }
}
