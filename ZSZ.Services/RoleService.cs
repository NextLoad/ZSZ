using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IServices;
using ZSZ.Services.Entities;

namespace ZSZ.Services
{
    public class RoleService : IRole
    {
        public long AddNewRole(string name)
        {
            RoleEntity role = new RoleEntity();
            role.Name = name;
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                ctx.Roles.Add(role);
                return role.Id;
            }
        }

        public void UpdateRole(long id, string name)
        {

            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> roleService = new CommonService<RoleEntity>(ctx);
                var role = roleService.GetById(id);
                if (role == null)
                {
                    throw new ArgumentException("角色不存在" + id);
                }

                role.Name = name;
                ctx.SaveChanges();
            }
        }

        public void MarkDeleted(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> roleService = new CommonService<RoleEntity>(ctx);
                roleService.MarkDeleted(id);
            }
        }

        private RoleDTO ToDTO(RoleEntity r)
        {
            throw new NotImplementedException();
        }

        public RoleDTO[] GetByAdminUserId(long adminUserId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> adminService = new CommonService<AdminUserEntity>(ctx);
                var adminUser = adminService.GetById(adminUserId);
                if (adminUser == null)
                {
                    throw new ArgumentException("不存在的管理员" + adminUserId);
                }

                return adminUser.RoleEntities.ToList().Select(r => ToDTO(r)).ToArray();
            }
        }

        public RoleDTO[] GetAll()
        {
            throw new NotImplementedException();
        }

        public RoleDTO GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public RoleDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void AddRoleIds(long adminUserId, long[] roleIds)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoleIds(long adminUserId, long[] roleIds)
        {
            throw new NotImplementedException();
        }
    }
}
