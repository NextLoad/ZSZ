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
    public class CommunitityService : ICommunitity
    {
        public long AddNewCommunitity(string name, long regionId, string location, string traffic, int? buildYear)
        {
            CommunitityEntity communitity = new CommunitityEntity();
            communitity.BuiltYear = buildYear;
            communitity.Location = location;
            communitity.Name = name;
            communitity.RegionId = regionId;
            communitity.Traffic = traffic;
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                ctx.Communitities.Add(communitity);
                ctx.SaveChanges();
                return communitity.Id;
            }
        }

        public CommunitityDTO[] GetByRegionId(long regionId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<CommunitityEntity> communitityService = new CommonService<CommunitityEntity>(ctx);
                var communitities = communitityService.GetAll().Where(c => c.RegionId == regionId).Include(c => c.RegionEntity);
                return communitities.ToList().Select(c => ToDTO(c)).ToArray();
            }
        }

        public CommunitityDTO[] GetByCityId(long cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RegionEntity> regionService = new CommonService<RegionEntity>(ctx);
                var regions = regionService.GetAll().Where(r => r.CityId == cityId);
                CommonService<CommunitityEntity> communitityService = new CommonService<CommunitityEntity>(ctx);
                var communitities = communitityService.GetAll().Where(c => regions.Select(r => r.Id).Contains(c.RegionId));
                return communitities.ToList().Select(c => ToDTO(c)).ToArray();
            }
        }

        public void UpdateCommunitity(long id, string name, long regionId, string location, string traffic, int? buildYear)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<CommunitityEntity> communtityService = new CommonService<CommunitityEntity>(ctx);
                var communitity = communtityService.GetById(id);
                communitity.Name = name;
                communitity.RegionId = regionId;
                communitity.Location = location;
                communitity.Traffic = traffic;
                communitity.BuiltYear = buildYear;
                ctx.SaveChanges();
            }
        }

        public void MarkDeleted(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<CommunitityEntity> communtityService = new CommonService<CommunitityEntity>(ctx);
                communtityService.MarkDeleted(id);
            }
        }

        public CommunitityDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<CommunitityEntity> communtityService = new CommonService<CommunitityEntity>(ctx);
                return ToDTO(communtityService.GetById(id));
            }
        }

        private CommunitityDTO ToDTO(CommunitityEntity Communitity)
        {
            CommunitityDTO cDTO = new CommunitityDTO();
            cDTO.BuiltYear = Communitity.BuiltYear;
            cDTO.Location = Communitity.Location;
            cDTO.Name = Communitity.Name;
            cDTO.RegionId = Communitity.RegionId;
            cDTO.RegionName = Communitity.RegionEntity.Name;
            cDTO.Traffic = Communitity.Traffic;
            cDTO.Id = Communitity.Id;
            cDTO.CreateDateTime = Communitity.CreateDateTime;
            return cDTO;
        }
    }
}
