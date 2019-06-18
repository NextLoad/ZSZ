using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZSZ.AdminWeb.Models
{
    public class LoginModel
    {
        [Phone]
        public string PhoneNum { get; set; }

        [StringLength(maximumLength: 16, MinimumLength = 2)]
        public string Password { get; set; }
        public string VerifyCode { get; set; }
        public bool Online { get; set; }

    }
}