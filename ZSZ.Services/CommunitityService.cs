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
                var communitities = ctx.Communitities.Where(c => c.RegionId == regionId).Include(c => c.RegionEntity);
                return communitities.ToList().Select(c => ToDTO(c)).ToArray();
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
