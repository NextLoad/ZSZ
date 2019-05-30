using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface ISetting:IServiceAutofac
    {
        /// <summary>
        /// 新增配置信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long AddNewSetting(string name, string value);

        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void UpdateSetting(long id, string name, string value);

        /// <summary>
        /// 删除配置信息
        /// </summary>
        /// <param name="id"></param>
        void MarkDeleted(long id);

        /// <summary>
        /// 获取所有配置信息
        /// </summary>
        /// <returns></returns>
        SettingDTO[] GetAll();

        /// <summary>
        /// 获取指定配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SettingDTO GetById(long id);

        /// <summary>
        /// 获取指定配置信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        SettingDTO GetByName(string name);
    }
}
