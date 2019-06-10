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
    public class PermissionService : IPermission
    {
        public long AddNewPermission(string name, string description)
        {
            PermissionEntity permission = new PermissionEntity();
            permission.Name = name;
            permission.Description = description;
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                ctx.Permissions.Add(permission);
                ctx.SaveChanges();
                return permission.Id;
            }
        }

        public void UpdatePermission(long id, string name, string description)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<PermissionEntity> service = new CommonService<PermissionEntity>(ctx);
                var permission = service.GetById(id);
                if (permission == null)
                {
                    throw new ArgumentException("权限不存在" + id);
                }

                permission.Name = name;
                permission.Description = description;
                ctx.SaveChanges();
            }
        }

        public void MarkDeleted(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<PermissionEntity> service = new CommonService<PermissionEntity>(ctx);
                service.MarkDeleted(id);
            }
        }

        private PermissionDTO ToDTO(PermissionEntity permission)
        {
            PermissionDTO permissionDto = new PermissionDTO();
            permissionDto.Name = permission.Name;
            permissionDto.Description = permission.Description;
            permissionDto.CreateDateTime = permission.CreateDateTime;
            permissionDto.Id = permission.Id;
            return permissionDto;
        }

        public PermissionDTO[] GetByRoleId(long roleId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> service = new CommonService<RoleEntity>(ctx);
                return service.GetById(roleId).PermissionEntities.ToList().Select(p => ToDTO(p)).ToArray();
            }
        }

        public PermissionDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<PermissionEntity> service = new CommonService<PermissionEntity>(ctx);
                return ToDTO(service.GetById(id));
            }
        }

        public PermissionDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<PermissionEntity> service = new CommonService<PermissionEntity>(ctx);
                return service.GetAll().ToList().Select(p => ToDTO(p)).ToArray();
            }
        }

        public PermissionDTO GetByName(string name)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<PermissionEntity> service = new CommonService<PermissionEntity>(ctx);
                var permission = service.GetAll().SingleOrDefault(p => p.Name == name);
                return permission == null ? null : ToDTO(permission);
            }
        }

        public void AddPermIds(long roleId, long[] permIds)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> service = new CommonService<RoleEntity>(ctx);
                var role = service.GetById(roleId);
                if (role == null)
                {
                    throw new ArgumentException("角色不存在" + roleId);
                }
                CommonService<PermissionEntity> permCs = new CommonService<PermissionEntity>(ctx);
                var permissions = permCs.GetAll().Where(p => permIds.Contains(p.Id)).ToList();
                foreach (var permission in permissions)
                {
                    //permission.RoleEntities.Add(role);
                    role.PermissionEntities.Add(permission);
                }

                ctx.SaveChanges();
            }
        }

        public void UpdatePermIds(long roleId, long[] permIds)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> service = new CommonService<RoleEntity>(ctx);
                var role = service.GetById(roleId);
                if (role == null)
                {
                    throw new ArgumentException("角色不存在" + roleId);
                }
                //先清除
                role.PermissionEntities.Clear();
                //再添加
                CommonService<PermissionEntity> permCs = new CommonService<PermissionEntity>(ctx);
                var permissions = permCs.GetAll().Where(p => permIds.Contains(p.Id)).ToList();
                foreach (var permission in permissions)
                {
                    //permission.RoleEntities.Add(role);
                    role.PermissionEntities.Add(permission);
                }

                ctx.SaveChanges();
            }
        }
    }
}
