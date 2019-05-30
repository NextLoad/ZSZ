using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class AdminUserConfig : EntityTypeConfiguration<AdminUserEntity>
    {
        public AdminUserConfig()
        {
            this.ToTable("T_AdminUsers");
            this.Property(u => u.EMail).IsRequired().HasMaxLength(30);
            this.Property(u => u.Name).IsRequired().HasMaxLength(50);
            this.Property(u => u.PasswordHash).IsRequired().HasMaxLength(100);
            this.Property(u => u.PasswordSalt).IsRequired().HasMaxLength(20);
            this.Property(u => u.PhoneNum).IsRequired().HasMaxLength(20).IsUnicode(false);
            this.HasOptional(u => u.CityEntity)
                .WithMany().HasForeignKey(u => u.CityId).WillCascadeOnDelete(false);
        }
    }
}
