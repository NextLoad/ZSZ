using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Services.Entities
{
    /// <summary>
    /// 房屋预约表
    /// </summary>
    public class HouseAppointmentEntity : BaseEntity
    {
        public long? UserId { get; set; }
        public virtual UserEntity UserEntity { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public DateTime VisitDate { get; set; }
        public long HouseId { get; set; }
        public virtual HouseEntity HouseEntity { get; set; }
        public string Status { get; set; }
        public long? FollowAdminUserId { get; set; }
        public virtual AdminUserEntity AdminUserEntity { get; set; }
        public DateTime FollowDateTime { get; set; }

    }
}
