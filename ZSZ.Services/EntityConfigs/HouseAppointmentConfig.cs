using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class HouseAppointmentConfig : EntityTypeConfiguration<HouseAppointmentEntity>
    {
        public HouseAppointmentConfig()
        {
            this.ToTable("T_HouseAppointments");
            this.Property(h => h.Name).HasMaxLength(50);
            this.Property(h => h.PhoneNum).HasMaxLength(20).IsUnicode(false);
            this.Property(h => h.Status).HasMaxLength(20);
            this.HasRequired(h => h.HouseEntity)
                .WithMany().HasForeignKey(h => h.HouseId).WillCascadeOnDelete(false);
            this.HasOptional(h => h.AdminUserEntity)
                .WithMany().HasForeignKey(h => h.FollowAdminUserId).WillCascadeOnDelete(false);
            this.HasOptional(h => h.UserEntity)
                .WithMany().HasForeignKey(h => h.UserId).WillCascadeOnDelete(false);
        }
    }
}
