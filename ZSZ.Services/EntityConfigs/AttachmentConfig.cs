using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class AttachmentConfig : EntityTypeConfiguration<AttachmentEntity>
    {
        public AttachmentConfig()
        {
            this.ToTable("T_Attachments");
            this.Property(a => a.Name).IsRequired().HasMaxLength(50);
            this.Property(a => a.IconName).IsRequired().HasMaxLength(50);
            this.HasMany(a => a.HouseEntities)
                .WithMany(h => h.AttachmentEntities)
                .Map(r => r.ToTable("T_HouseAttachmentRel")
                .MapLeftKey("AttachmentId").MapRightKey("HouseId"));
        }
    }
}
