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
    }
}
