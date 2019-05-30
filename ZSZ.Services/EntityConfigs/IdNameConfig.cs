using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class IdNameConfig : EntityTypeConfiguration<IdNameEntity>
    {
        public IdNameConfig()
        {
            this.ToTable("T_IdNames");
            this.Property(i => i.Name).IsRequired().HasMaxLength(1024);
            this.Property(i => i.TypeName).IsRequired().HasMaxLength(1024);
        }
    }
}
