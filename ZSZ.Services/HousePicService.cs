using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IServices;

namespace ZSZ.Services
{
    public class HousePicService: IHousePic
    {
        public HousePicDTO[] GetPics(long houseId)
        {
            throw new NotImplementedException();
        }

        public long AddNewHousePic(HousePicDTO housePic)
        {
            throw new NotImplementedException();
        }

        public void UpdateHousePic(HousePicDTO housePic)
        {
            throw new NotImplementedException();
        }

        public void MarkDeleted(long id)
        {
            throw new NotImplementedException();
        }
    }
}
