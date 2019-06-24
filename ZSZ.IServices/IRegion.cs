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

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        RegionDTO[] GetPageData(long cityId, int pageIndex, int pageCount);

        /// <summary>
        /// 添加区域
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cityId"></param>
        void AddNew(string name, long cityId);

        /// <summary>
        /// 更新区域
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="cityId"></param>
        void Update(long id, string name, long cityId);

        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name="id"></param>
        void MarkDeleted(long id);



    }
}
