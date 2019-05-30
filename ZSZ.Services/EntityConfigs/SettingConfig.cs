using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class SettingConfig : EntityTypeConfiguration<SettingEntity>
    {
        public SettingConfig()
        {
            this.ToTable("T_Settings");
            this.Property(s => s.Name).IsRequired().HasMaxLength(1024);
            
        }
    }
}
