using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                ctx.SaveChanges();
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
            RoleDTO roleDto = new RoleDTO();
            roleDto.Name = r.Name;
            roleDto.CreateDateTime = r.CreateDateTime;
            roleDto.Id = r.Id;
            return roleDto;
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
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> roleService = new CommonService<RoleEntity>(ctx);
                return roleService.GetAll().ToList().Select(r => ToDTO(r)).ToArray();
            }
        }

        public RoleDTO GetByName(string name)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> roleService = new CommonService<RoleEntity>(ctx);
                RoleEntity role = roleService.GetAll().SingleOrDefault(r => r.Name == name);
                return role == null ? null : ToDTO(role);
            }
        }

        public RoleDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> roleService = new CommonService<RoleEntity>(ctx);
                RoleEntity role = roleService.GetById(id);
                return role == null ? null : ToDTO(role);
            }
        }

        public void AddRoleIds(long adminUserId, long[] roleIds)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> adminService = new CommonService<AdminUserEntity>(ctx);
                AdminUserEntity adminUser = adminService.GetById(adminUserId);
                if (adminUser == null)
                {
                    throw new ArgumentException("管理员用户不存在！" + adminUserId);
                }
                CommonService<RoleEntity> roleService = new CommonService<RoleEntity>(ctx);
                var roles = roleService.GetAll().Where(r => roleIds.Contains(r.Id)).Include(r=>r.AdminUserEntities).Include(r=>r.PermissionEntities);
                foreach (RoleEntity role in roles.ToList())
                {
                    adminUser.RoleEntities.Add(role);
                }

                ctx.SaveChanges();
            }
        }

        public void UpdateRoleIds(long adminUserId, long[] roleIds)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> adminService = new CommonService<AdminUserEntity>(ctx);
                AdminUserEntity adminUser = adminService.GetById(adminUserId);
                if (adminUser == null)
                {
                    throw new ArgumentException("管理员用户不存在！" + adminUserId);
                }
                adminUser.RoleEntities.Clear();
                CommonService<RoleEntity> roleService = new CommonService<RoleEntity>(ctx);
                var roles = roleService.GetAll().Where(r => roleIds.Contains(r.Id));
                foreach (RoleEntity role in roles)
                {
                    adminUser.RoleEntities.Add(role);
                }

                ctx.SaveChanges();
            }
        }
    }
}
