using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface IHouse : IServiceAutofac
    {
        /// <summary>
        /// 获取所有房屋
        /// </summary>
        /// <returns></returns>
        HouseDTO[] GetAll();

        /// <summary>
        /// 获取指定的房屋
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        HouseDTO GetById(long id);

        /// <summary>
        /// 获取某个城市下房源类型房屋总量
        /// </summary>
        /// <returns></returns>
        long GetTotalCount(long cityId, long typeId);

        /// <summary>
        /// 分页获取房屋数据
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="typeId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        HouseDTO[] GetPageData(long cityId, long typeId, int pageIndex, int pageCount);

        /// <summary>
        /// 新增房屋
        /// </summary>
        /// <returns></returns>
        long AddNewHouse(HouseDTO house);

        /// <summary>
        /// 更新房屋
        /// </summary>
        /// <param name="house"></param>
        void UpdateHouse(HouseDTO house);

        /// <summary>
        /// 删除房屋
        /// </summary>
        /// <param name="houseId"></param>
        void MarkDeleted(long houseId);

        
    }
}
