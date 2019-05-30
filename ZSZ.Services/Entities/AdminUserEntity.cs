using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Services.Entities
{
    /// <summary>
    /// 管理员用户表
    /// </summary>
    public class AdminUserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string EMail { get; set; }
        public long? CityId { get; set; }
        public virtual CityEntity CityEntity { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }
        public virtual ICollection<RoleEntity> RoleEntities { get; set; } = new List<RoleEntity>();

    }
}
