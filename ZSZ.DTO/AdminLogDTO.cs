using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.DTO
{
    public class AdminLogDTO : BaseDTO
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhoneNum { get; set; }
        public string Msg { get; set; }
    }
}
