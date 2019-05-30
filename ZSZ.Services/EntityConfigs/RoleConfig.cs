using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class RoleConfig : EntityTypeConfiguration<RoleEntity>
    {
        public RoleConfig()
        {
            this.ToTable("T_Roles");
            this.Property(p => p.Name).IsRequired().HasMaxLength(50);
            this.HasMany(r => r.AdminUserEntities)
                .WithMany(u => u.RoleEntities)
                .Map(r => r.ToTable("T_RoleAdminUserRel").MapLeftKey("RoleId").MapRightKey("AdminUserId"));
            this.HasMany(r => r.PermissionEntities)
                .WithMany(p => p.RoleEntities)
                .Map(r => r.ToTable("T_RolePermissionRel").MapLeftKey("RoleId").MapRightKey("PermissionId"));
        }
    }
}
