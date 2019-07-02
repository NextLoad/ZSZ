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
    public class HousePicService : IHousePic
    {
        public HousePicDTO[] GetPics(long houseId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<HousePicEntity> housePicService = new CommonService<HousePicEntity>(ctx);
                var housePics = housePicService.GetAll().Where(h => h.HouseId == houseId);
                return housePics.ToList().Select(h => ToDTO(h)).ToArray();
            }
        }

        private HousePicDTO ToDTO(HousePicEntity housePic)
        {
            HousePicDTO housePicDTO = new HousePicDTO();
            housePicDTO.HouseId = housePic.HouseId;
            housePicDTO.Id = housePic.Id;
            housePicDTO.ThumbUrl = housePic.ThumbUrl;
            housePicDTO.Url = housePic.Url;
            housePicDTO.CreateDateTime = housePic.CreateDateTime;
            return housePicDTO;
        }

        public long AddNewHousePic(HousePicDTO housePic)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                HousePicEntity housePicAdd = new HousePicEntity();
                housePicAdd.HouseId = housePic.HouseId;
                housePicAdd.ThumbUrl = housePic.ThumbUrl;
                housePicAdd.Url = housePic.Url;
                ctx.HousePics.Add(housePicAdd);
                ctx.SaveChanges();
                return housePicAdd.Id;
            }
        }

        public void UpdateHousePic(HousePicDTO housePic)
        {
            throw new NotImplementedException();
        }

        public void MarkDeleted(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<HousePicEntity> housePicService = new CommonService<HousePicEntity>(ctx);
                housePicService.MarkDeleted(id);
            }
        }
    }
}
