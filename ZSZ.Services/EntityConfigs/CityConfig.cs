using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class CityConfig : EntityTypeConfiguration<CityEntity>
    {
        public CityConfig()
        {
            this.ToTable("T_Cities");
            this.Property(c => c.Name).IsRequired().HasMaxLength(20);
        }
    }
}
