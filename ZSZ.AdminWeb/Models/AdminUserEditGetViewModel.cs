using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.AdminWeb.Models
{
    public class AdminUserEditGetViewModel
    {
        public AdminUserDTO AdminUserDto { get; set; }
        public RoleDTO[] RoleDtos { get; set; }
        public CityDTO[] CityDtos { get; set; }
    }
}