using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class PermissionConfig : EntityTypeConfiguration<PermissionEntity>
    {
        public PermissionConfig()
        {
            this.ToTable("T_Permissions");
            this.Property(p => p.Description).IsOptional().HasMaxLength(1024);
            this.Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
