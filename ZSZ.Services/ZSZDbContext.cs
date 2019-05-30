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

        public DbSet<AdminLogEntity> AdminLogEntities { get; set; }
        public DbSet<AdminUserEntity> AdminUserEntities { get; set; }
        public DbSet<AttachmentEntity> AttachmentEntities { get; set; }
        public DbSet<CityEntity> CityEntities { get; set; }
        public DbSet<CommunitityEntity> CommunitityEntities { get; set; }
        public DbSet<HouseAppointmentEntity> HouseAppointmentEntities { get; set; }
        public DbSet<HouseEntity> HouseEntities { get; set; }
        public DbSet<HousePicEntity> HousePicEntities { get; set; }
        public DbSet<IdNameEntity> IdNameEntities { get; set; }
        public DbSet<PermissionEntity> PermissionEntities { get; set; }
        public DbSet<RegionEntity> RegionEntities { get; set; }
        public DbSet<RoleEntity> RoleEntities { get; set; }
        public DbSet<SettingEntity> SettingEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
    }
}
