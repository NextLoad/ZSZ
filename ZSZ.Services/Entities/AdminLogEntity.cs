using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Services.Entities
{
    /// <summary>
    /// 管理员操作日志表
    /// </summary>
    public class AdminLogEntity : BaseEntity
    {
        public long UserId { get; set; }
        public virtual AdminUserEntity AdminUserEntity { get; set; }
        public string Msg { get; set; }
    }
}
