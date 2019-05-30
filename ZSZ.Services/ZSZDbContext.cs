using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using ZSZ.Services.Entities;
using ZSZ.Services.EntityConfigs;

namespace ZSZ.Services
{
    class ZSZDbContext : DbContext
    {
        private ILog logger = LogManager.GetLogger(typeof(ZSZDbContext));
        public ZSZDbContext() : base("name=connStr")
        {
            Database.SetInitializer<ZSZDbContext>(null);
            this.Database.Log = sql => logger.DebugFormat("sql执行语句日志：{0}", sql);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<AdminLogEntity> AdminLogs { get; set; }
        public DbSet<AdminUserEntity> AdminUsers { get; set; }
        public DbSet<AttachmentEntity> Attachments { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<CommunitityEntity> Communitities { get; set; }
        public DbSet<HouseAppointmentEntity> HouseAppointments { get; set; }
        public DbSet<HouseEntity> Houses { get; set; }
        public DbSet<HousePicEntity> HousePics { get; set; }
        public DbSet<IdNameEntity> IdNames { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<RegionEntity> Regions { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<SettingEntity> Settings { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}
