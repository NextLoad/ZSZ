﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.AdminWeb.Models
{
    public class AdminUserAddNewGetViewModel
    {
        public RoleDTO[] Roles { get; set; }
        public CityDTO[] Cities { get; set; }

    }
}