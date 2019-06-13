using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.AdminWeb.Models
{
    public class RoleEditGetModel
    {
        public RoleDTO RoleDto { get; set; }
        public PermissionDTO[] PermissionDtoHas { get; set; }
        public PermissionDTO[] PermissionDtoAll { get; set; }
    }
}