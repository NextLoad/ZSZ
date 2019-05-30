using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface IPermission : IServiceAutofac
    {
        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        long AddNewPermission(string name, string description);

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        void UpdatePermission(long id, string name, string description);

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id"></param>
        void MarkDeleted(long id);

        /// <summary>
        /// 获取某个角色的权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        PermissionDTO[] GetByRoleId(long roleId);

        /// <summary>
        /// 获取指定的权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PermissionDTO GetById(long id);

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        PermissionDTO[] GetAll();

        /// <summary>
        /// 根据名称获取权限
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        PermissionDTO GetByName(string name);

        /// <summary>
        /// 增加角色的权限项
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permIds"></param>
        void AddPermIds(long roleId, long[] permIds);

        /// <summary>
        /// 更新角色的权限项
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permIds"></param>
        void UpdatePermIds(long roleId, long[] permIds);
    }
}
