using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class UserConfig : EntityTypeConfiguration<UserEntity>
    {
        public UserConfig()
        {
            this.ToTable("T_Users");
            this.Property(u => u.PasswordHash).IsRequired().HasMaxLength(100);
            this.Property(u => u.PasswordSalt).IsRequired().HasMaxLength(20);
            this.Property(u => u.PhoneNum).IsRequired().HasMaxLength(20).IsUnicode(false);
            this.HasRequired(u => u.CityEntity)
                .WithMany().HasForeignKey(u => u.CityId).WillCascadeOnDelete(false);
        }
    }
}
