﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZSZ.AdminWeb.Models
{
    public class RoleAddModel
    {
        [Required]
        public string  Name { get; set; }
        public long[] PermIds { get; set; }

    }
}