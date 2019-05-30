using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface IRegion : IServiceAutofac
    {
        /// <summary>
        /// 获取指定城市的区域
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        RegionDTO[] GetAll(long cityId);

        /// <summary>
        /// 获取指定的区域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RegionDTO GetById(long id);


    }
}
