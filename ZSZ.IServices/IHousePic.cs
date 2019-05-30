using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface IHousePic:IServiceAutofac
    {
        /// <summary>
        /// 获取房屋图片
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        HousePicDTO[] GetPics(long houseId);

        /// <summary>
        /// 添加房屋图片
        /// </summary>
        /// <param name="housePic"></param>
        /// <returns></returns>
        long AddNewHousePic(HousePicDTO housePic);

        /// <summary>
        /// 更新房屋图片
        /// </summary>
        /// <param name="housePic"></param>
        void UpdateHousePic(HousePicDTO housePic);

        /// <summary>
        /// 删除房源图片
        /// </summary>
        /// <param name="id"></param>
        void MarkDeleted(long id);

    }
}
