using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Services.Entities
{
    /// <summary>
    /// 配置表
    /// </summary>
    public class SettingEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
