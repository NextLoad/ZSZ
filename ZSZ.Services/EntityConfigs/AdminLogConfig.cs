﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class AdminLogConfig : EntityTypeConfiguration<AdminLogEntity>
    {
        public AdminLogConfig()
        {
            this.ToTable("T_AdminLogs");
            this.Property(a => a.Msg).IsRequired();
            this.HasRequired(a => a.AdminUserEntity)
                .WithMany().HasForeignKey(a => a.UserId).WillCascadeOnDelete(false);
        }
    }
}
