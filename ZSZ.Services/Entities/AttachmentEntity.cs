using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Services.Entities
{
    /// <summary>
    /// 房屋配置表
    /// </summary>
    public class AttachmentEntity : BaseEntity
    {
        public string Name { get; set; }
        public string IconName { get; set; }
        public virtual ICollection<HouseEntity> HouseEntities { get; set; } = new List<HouseEntity>();
    }
}
