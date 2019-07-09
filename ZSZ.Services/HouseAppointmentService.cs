using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IServices;
using ZSZ.Services.Entities;

namespace ZSZ.Services
{
    public class HouseAppointmentService : IHouseAppointment
    {
        public long AddNewHouseAppointment(long? userId, string name, string phoneNum, DateTime visiDate, long houseId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                HouseAppointmentEntity houseAppointment = new HouseAppointmentEntity();
                houseAppointment.UserId = userId;
                houseAppointment.Name = name;
                houseAppointment.PhoneNum = phoneNum;
                houseAppointment.VisitDate = visiDate;
                houseAppointment.HouseId = houseId;
                houseAppointment.Status = "未处理";
                ctx.HouseAppointments.Add(houseAppointment);
                ctx.SaveChanges();
                return houseAppointment.Id;
            }
        }

        public void UpdateHouseAppointment(long id, long? userId, string name, string phoneNum, DateTime visiDate, long houseId,
            string status, long? followAdminUserId, DateTime followDateTime)
        {
            throw new NotImplementedException();
        }

        public bool Follow(long adminUserId, long houseAppointmentId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<HouseAppointmentEntity> appointService = new CommonService<HouseAppointmentEntity>(ctx);
                var houseAppointment = appointService.GetById(houseAppointmentId);
                if (houseAppointment == null)
                {
                    throw new ArgumentException("不存在的id" + houseAppointmentId);
                }

                //如果已经被下单了
                if (houseAppointment.FollowAdminUserId != null)
                {
                    return houseAppointment.FollowAdminUserId.Value == adminUserId;
                }
                houseAppointment.FollowAdminUserId = adminUserId;
                houseAppointment.FollowDateTime = DateTime.Now;
                try
                {
                    ctx.SaveChanges();
                    return true;
                }
                //如果抛出异常 则表示抢单失败（乐观锁）
                catch (DbUpdateConcurrencyException)
                {
                    return false;
                }

            }
        }

        private HouseAppointmentDTO ToDTO(HouseAppointmentEntity houseApp)
        {
            HouseAppointmentDTO dto = new HouseAppointmentDTO();
            dto.CommunityName = houseApp.HouseEntity.CommunitityEntity.Name;
            dto.CreateDateTime = houseApp.CreateDateTime;
            dto.FollowAdminUserId = houseApp.FollowAdminUserId;
            if (houseApp.AdminUserEntity != null)
            {
                dto.FollowAdminUserName = houseApp.AdminUserEntity.Name;
            }
            dto.FollowDateTime = houseApp.FollowDateTime;
            dto.HouseId = houseApp.HouseId;
            dto.Id = houseApp.Id;
            dto.Name = houseApp.Name;
            dto.PhoneNum = houseApp.PhoneNum;
            dto.RegionName = houseApp.HouseEntity.CommunitityEntity.RegionEntity.Name;
            dto.Status = houseApp.Status;
            dto.UserId = houseApp.UserId;
            dto.VisitDate = houseApp.VisitDate;
            dto.HouseAddress = houseApp.HouseEntity.Address;
            return dto;
        }


        public long GetTotalCount(long cityId, string status)
        {
            throw new NotImplementedException();
        }

        public HouseAppointmentDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<HouseAppointmentEntity> appointService = new CommonService<HouseAppointmentEntity>(ctx);
                return appointService.GetAll().ToList().Select(a => ToDTO(a)).ToArray();
            }

        }

        public HouseAppointmentDTO[] GetPagedData(long cityId, string status, int pageSize, int currentIndex)
        {
            throw new NotImplementedException();
        }
    }
}
