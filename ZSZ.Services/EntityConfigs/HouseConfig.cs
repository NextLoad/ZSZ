﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services.EntityConfigs
{
    public class HouseConfig : EntityTypeConfiguration<HouseEntity>
    {
        public HouseConfig()
        {
            this.ToTable("T_Houses");
            this.Property(h => h.Address).IsRequired().HasMaxLength(128);
            this.Property(h => h.Direction).HasMaxLength(20);
            this.Property(h => h.OwnerName).HasMaxLength(20);
            this.Property(h => h.OwnerPhoneNum).IsRequired().HasMaxLength(20).IsUnicode(false);
            this.HasRequired(h => h.CommunitityEntity)
                .WithMany().HasForeignKey(h => h.CommunityId).WillCascadeOnDelete(false);
            this.HasRequired(h => h.RoomType)
                .WithMany().HasForeignKey(h => h.RoomTypeId).WillCascadeOnDelete(false);
            this.HasRequired(h => h.DecorateStatus)
                .WithMany().HasForeignKey(h => h.DecorateStatusId).WillCascadeOnDelete(false);
            this.HasRequired(h => h.Status)
                .WithMany().HasForeignKey(h => h.StatusId).WillCascadeOnDelete(false);
            this.HasRequired(h => h.Type)
                .WithMany().HasForeignKey(h => h.TypeId).WillCascadeOnDelete(false);
        }
    }
}
