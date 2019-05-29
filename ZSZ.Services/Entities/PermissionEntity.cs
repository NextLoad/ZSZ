using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Services.Entities
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class PermissionEntity:BaseEntity
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RoleEntity> RoleEntities { get; set; }=new List<RoleEntity>();
    }
}
