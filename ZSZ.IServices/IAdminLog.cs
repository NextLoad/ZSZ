using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface IAdminLog:IServiceAutofac
    {
        /// <summary>
        /// 插入一条日志
        /// </summary>
        /// <param name="adminUserId">操作用户Id</param>
        /// <param name="msg">操作信息</param>
        /// <returns></returns>
        long AddNew(long adminUserId,string msg);
        /// <summary>
        /// 获取一条记录
        /// </summary>
        /// <param name="id">记录id</param>
        /// <returns></returns>
        AdminLogDTO GetById(long id);
    }
}
