using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class RegionConfig : EntityTypeConfiguration<RegionEntity>
    {
        public RegionConfig()
        {
            this.ToTable("T_Regions");
            this.Property(r => r.Name).IsRequired().HasMaxLength(50);
            this.HasRequired(r => r.CityEntity)
                .WithMany().HasForeignKey(r => r.CityId).WillCascadeOnDelete(false);
        }
    }
}
