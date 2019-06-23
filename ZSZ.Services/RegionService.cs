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
    public class RegionService: IRegion
    {
        public RegionDTO[] GetAll(long cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RegionEntity> regionService = new CommonService<RegionEntity>(ctx);
                var regions = regionService.GetAll().Where(r => r.CityId == cityId).Include(r=>r.CityEntity);
                return regions.ToList().Select(r => ToDTO(r)).ToArray();
            }
        }

        private RegionDTO ToDTO(RegionEntity regionEntity)
        {
            RegionDTO regionDto = new RegionDTO();
            regionDto.CityName = regionEntity.CityEntity.Name;
            regionDto.CityId = regionEntity.CityId;
            regionDto.Name = regionEntity.Name;
            regionDto.Id = regionEntity.Id;
            regionDto.CreateDateTime = regionEntity.CreateDateTime;
            return regionDto;
        }

        public RegionDTO GetById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
