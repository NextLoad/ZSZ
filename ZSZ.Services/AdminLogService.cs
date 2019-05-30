using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IServices;
using ZSZ.Services.Entities;

namespace ZSZ.Services
{
    public class AdminLogService : IAdminLog
    {
        public long AddNew(long adminUserId, string msg)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminLogEntity> cs = new CommonService<AdminLogEntity>(ctx);
                AdminLogEntity admin = new AdminLogEntity { UserId = adminUserId, Msg = msg };
                ctx.AdminLogs.Add(admin);
                ctx.SaveChanges();
                return admin.Id;
            }
        }

        public AdminLogDTO ToDTO(AdminLogEntity admin)
        {
            AdminLogDTO adminDto = new AdminLogDTO();
            adminDto.UserId = admin.UserId;
            adminDto.Msg = admin.Msg;
            adminDto.UserName = admin.AdminUserEntity.Name;
            adminDto.UserPhoneNum = admin.AdminUserEntity.PhoneNum;
            adminDto.CreateDateTime = admin.CreateDateTime;
            adminDto.Id = admin.Id;
            return adminDto;
        }

        public AdminLogDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminLogEntity> cs = new CommonService<AdminLogEntity>(ctx);
                AdminLogEntity admin = cs.GetById(id);
                return ToDTO(admin);
            }
        }
    }
}
