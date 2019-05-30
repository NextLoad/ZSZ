using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface IRole:IServiceAutofac
    {
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        long AddNewRole(string name);

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        void UpdateRole(long id, string name);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        void MarkDeleted(long id);

        /// <summary>
        /// 根据用户id获取所有角色
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <returns></returns>
        RoleDTO[] GetByAdminUserId(long adminUserId);

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        RoleDTO[] GetAll();

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        RoleDTO GetByName(string name);

        /// <summary>
        /// 获取指定角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleDTO GetById(long id);

        /// <summary>
        /// 给指定用户添加角色
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <param name="roleIds"></param>
        void AddRoleIds(long adminUserId, long[] roleIds);

        /// <summary>
        /// 给指定用户更新角色
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <param name="roleIds"></param>
        void UpdateRoleIds(long adminUserId, long[] roleIds);
    }
}
