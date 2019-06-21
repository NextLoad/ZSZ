using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface ICity:IServiceAutofac
    {
        /// <summary>
        /// 新增城市
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        long AddNewCity(string cityName);

        /// <summary>
        /// 获取所有城市
        /// </summary>
        /// <returns></returns>
        CityDTO[] GetAll();

        /// <summary>
        /// 获取指定城市
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CityDTO GetById(long id);

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="currentIndex">页码</param>
        /// <param name="pageSize">获取条数</param>
        /// <returns></returns>
        CityDTO[] GetPageData(int currentIndex,int pageSize);

        /// <summary>
        /// 更新城市
        /// </summary>
        /// <param name="id"></param>
        void UpdateCity(long id,string name);

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="id"></param>
        void MarkDeleted(long id);
    }
}
