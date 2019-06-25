using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface ICommunitity : IServiceAutofac
    {
        /// <summary>
        /// 新增小区
        /// </summary>
        /// <param name="name"></param>
        /// <param name="regionId"></param>
        /// <param name="location"></param>
        /// <param name="traffic"></param>
        /// <param name="buildYear"></param>
        /// <returns></returns>
        long AddNewCommunitity(string name, long regionId, string location, string traffic, int? buildYear);

        /// <summary>
        /// 获取区域内的小区
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        CommunitityDTO[] GetByRegionId(long regionId);

        /// <summary>
        /// 更新小区信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="regionId"></param>
        /// <param name="location"></param>
        /// <param name="traffic"></param>
        /// <param name="buildYear"></param>
        void UpdateCommunitity(long id, string name, long regionId, string location, string traffic, int? buildYear);

        /// <summary>
        /// 删除小区
        /// </summary>
        /// <param name="id"></param>
        void MarkDeleted(long id);

        /// <summary>
        /// 获取小区信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CommunitityDTO GetById(long id);



    }
}
