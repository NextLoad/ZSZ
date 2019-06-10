using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IServices;
using ZSZ.Services.Entities;

namespace ZSZ.Services
{
    public class HouseService : IHouse
    {
        private HouseDTO ToDTO(HouseEntity house)
        {
            HouseDTO houseDto = new HouseDTO();
            houseDto.Address = house.Address;
            houseDto.Area = house.Area;
            houseDto.CheckInDateTime = house.CheckInDateTime;
            houseDto.CommunityId = house.CommunityId;
            houseDto.CommunityName = house.CommunitityEntity.Name;
            houseDto.DecorateStatusId = house.DecorateStatusId;
            houseDto.DecorateStatusName = house.DecorateStatus.Name;
            houseDto.Description = house.Description;
            houseDto.Direction = house.Direction;
            houseDto.FloorIndex = house.FloorIndex;
            houseDto.LookableDateTime = house.LookableDateTime;
            houseDto.MonthRent = house.MonthRent;
            houseDto.OwnerName = house.OwnerName;
            houseDto.OwnerPhoneNum = house.OwnerPhoneNum;
            houseDto.RoomTypeId = house.RoomTypeId;
            houseDto.RoomTypeName = house.RoomType.Name;
            houseDto.StatusId = house.StatusId;
            houseDto.StatusName = house.Status.Name;
            houseDto.TotalFloatCount = house.TotalFloatCount;
            houseDto.TypeId = house.TypeId;
            houseDto.TypeName = house.Type.Name;
            houseDto.Id = house.Id;
            houseDto.CreateDateTime = house.CreateDateTime;
            return houseDto;
        }
        public HouseDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<HouseEntity> service = new CommonService<HouseEntity>(ctx);
                return service.GetAll().AsNoTracking().ToList().Select(ToDTO).ToArray();
            }
        }

        public HouseDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<HouseEntity> service = new CommonService<HouseEntity>(ctx);
                return ToDTO(service.GetById(id));
            }
        }

        public long GetTotalCount(long cityId, long typeId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                return ctx.Houses.Where(h => h.TypeId == typeId && h.CommunitityEntity.RegionEntity.CityId == cityId)
                    .LongCount();
            }
        }

        public HouseDTO[] GetPageData(long cityId, long typeId, int pageIndex, int pageCount)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                return ctx.Houses.Where(h => h.TypeId == typeId && h.CommunitityEntity.RegionEntity.CityId == cityId)
                    .OrderBy(h => h.TypeId).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList().Select(ToDTO).ToArray();
            }
        }

        private HouseEntity ToEntity(HouseDTO houseDto)
        {
            HouseEntity house = new HouseEntity();
            house.Address = houseDto.Address;
            house.Area = houseDto.Area;
            house.CheckInDateTime = houseDto.CheckInDateTime;
            house.CommunityId = houseDto.CommunityId;
            //house.CommunityName = houseDto.CommunitityEntity.Name;
            house.DecorateStatusId = houseDto.DecorateStatusId;
            //house.DecorateStatusName = houseDto.DecorateStatus.Name;
            house.Description = houseDto.Description;
            house.Direction = houseDto.Direction;
            house.FloorIndex = houseDto.FloorIndex;
            house.LookableDateTime = houseDto.LookableDateTime;
            house.MonthRent = houseDto.MonthRent;
            house.OwnerName = houseDto.OwnerName;
            house.OwnerPhoneNum = houseDto.OwnerPhoneNum;
            house.RoomTypeId = houseDto.RoomTypeId;
            //house.RoomTypeName = houseDto.RoomType.Name;
            house.StatusId = houseDto.StatusId;
            //house.StatusName = houseDto.Status.Name;
            house.TotalFloatCount = houseDto.TotalFloatCount;
            house.TypeId = houseDto.TypeId;
            //house.TypeName = houseDto.Type.Name;
            house.Id = houseDto.Id;
            house.CreateDateTime = houseDto.CreateDateTime;
            return house;
        }

        public long AddNewHouse(HouseDTO houseDto)
        {
            HouseEntity houseEntity = ToEntity(houseDto);
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                ctx.Houses.Add(houseEntity);
                ctx.SaveChanges();
                return houseEntity.Id;
            }
        }

        public void UpdateHouse(HouseDTO houseDto)
        {
            HouseEntity houseEntity = ToEntity(houseDto);
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                var house = ctx.Houses.SingleOrDefault(h => h.Id == houseDto.Id);
                if (house == null)
                {
                    throw new ArgumentException("房子信息不存在" + houseDto.Id);
                }

                house = houseEntity;
                ctx.SaveChanges();
            }
        }

        public void MarkDeleted(long houseId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<HouseEntity> service = new CommonService<HouseEntity>(ctx);
                service.MarkDeleted(houseId);
            }
        }
    }
}
