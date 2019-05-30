using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class HousePicConfig : EntityTypeConfiguration<HousePicEntity>
    {
        public HousePicConfig()
        {
            this.ToTable("T_HousePics");
            this.Property(h => h.ThumbUrl).IsRequired().HasMaxLength(1024);
            this.Property(h => h.Url).IsRequired().HasMaxLength(1024);
            this.HasRequired(h => h.HouseEntity)
                .WithMany().HasForeignKey(h => h.HouseId).WillCascadeOnDelete(false);

        }
    }
}
