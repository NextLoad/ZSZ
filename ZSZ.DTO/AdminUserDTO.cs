using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.DTO
{
    [Serializable]
    public class AdminUserDTO:BaseDTO
    {
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string EMail { get; set; }
        public long? CityId { get; set; }
        public string CityName { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }
        public string[] RoleNames { get; set; }
        public long[] RoleIds { get; set; }
        
    }
}
