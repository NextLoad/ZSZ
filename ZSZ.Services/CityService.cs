﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Common;
using ZSZ.DTO;
using ZSZ.IServices;
using ZSZ.Services.Entities;

namespace ZSZ.Services
{
    public class CityService : ICity
    {
        public long AddNewCity(string cityName)
        {
            CityEntity city = new CityEntity { Name = cityName };
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                ctx.Cities.Add(city);
                ctx.SaveChanges();
                return city.Id;
            }
        }

        public CityDTO ToDTO(CityEntity city)
        {
            CityDTO cityDto = new CityDTO();
            cityDto.Name = city.Name;
            cityDto.Id = city.Id;
            cityDto.CreateDateTime = city.CreateDateTime;
            return cityDto;
        }

        public CityDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {

                CommonService<CityEntity> cs = new CommonService<CityEntity>(ctx);
                return cs.GetAll().AsNoTracking().ToList().Select(c => GenericToDTO.ToDTO<CityEntity, CityDTO>(c)).ToArray();
            }
        }

        public CityDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<CityEntity> cs = new CommonService<CityEntity>(ctx);
                return ToDTO(cs.GetById(id));
            }
        }

        public CityDTO[] GetPageData(int currentIndex, int pageSize)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<CityEntity> cs = new CommonService<CityEntity>(ctx);
                return cs.GetPageData(currentIndex, pageSize).AsNoTracking().ToList().Select(c => ToDTO(c)).ToArray();
            }
        }

        public void UpdateCity(long id, string name)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<CityEntity> cs = new CommonService<CityEntity>(ctx);
                var city = cs.GetById(id);
                city.Name = name;
                ctx.SaveChanges();
            }
        }

        public void MarkDeleted(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<CityEntity> cs = new CommonService<CityEntity>(ctx);
                cs.MarkDeleted(id);
            }
        }
    }
}
