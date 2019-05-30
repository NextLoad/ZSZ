using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class CommunitityConfig : EntityTypeConfiguration<CommunitityEntity>
    {
        public CommunitityConfig()
        {
            this.ToTable("T_Communitities");
            this.Property(c => c.Name).IsRequired().HasMaxLength(30);
            this.Property(c => c.Location).IsRequired().HasMaxLength(1024);
            this.HasRequired(c => c.RegionEntity)
                .WithMany().HasForeignKey(c => c.RegionId).WillCascadeOnDelete(false);
        }
    }
}
