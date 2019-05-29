using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Services.Entities
{
    /// <summary>
    /// 房屋图片表
    /// </summary>
    public class HousePicEntity : BaseEntity
    {
        public long HouseId { get; set; }
        public virtual HouseEntity HouseEntity { get; set; }
        public string Url { get; set; }
        public string ThumbUrl { get; set; }
    }
}
