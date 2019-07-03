using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZSZ.FrontWeb.Models
{
    public class RegisterModel
    {
        [Phone]
        public string PhoneNum { get; set; }
        public int SmsCode { get; set; }
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string Password2 { get; set; }

        public long? CityId { get; set; }
    }
}