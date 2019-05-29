using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Services.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
