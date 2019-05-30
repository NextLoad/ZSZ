using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface IdName : IServiceAutofac
    {
        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <returns></returns>
        IdNameDTO[] GetAll();

        /// <summary>
        /// 根据类别名获取数据字典
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        IdNameDTO[] GetByTypeName(string typeName);

        /// <summary>
        /// 新增数据字典
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        long AddNew(String typeName, String name);

        /// <summary>
        /// 获取指定数据字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IdNameDTO GetById(long id);
    }
}
