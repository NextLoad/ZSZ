using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Services.Entities
{
    /// <summary>
    /// 数据字典表
    /// </summary>
    public class IdNameEntity:BaseEntity
    {
        public string TypeName { get; set; }
        public string Name { get; set; }
    }
}
