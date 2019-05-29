using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Services.Entities
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<PermissionEntity> PermissionEntities { get; set; } = new List<PermissionEntity>();
        public virtual ICollection<AdminUserEntity> AdminUserEntities { get; set; } = new List<AdminUserEntity>();
    }
}
