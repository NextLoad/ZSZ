using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IServices;
using ZSZ.Services.Entities;

namespace ZSZ.Services
{
    public class IdNameService: IdName
    {
        public IdNameDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<IdNameEntity> cs = new CommonService<IdNameEntity>(ctx);
                return cs.GetAll().ToList().Select(i => ToDTO(i)).ToArray();
            }
        }

        private IdNameDTO ToDTO(IdNameEntity i)
        {
            throw new NotImplementedException();
        }

        public IdNameDTO[] GetByTypeName(string typeName)
        {
            throw new NotImplementedException();
        }

        public long AddNew(string typeName, string name)
        {
            throw new NotImplementedException();
        }

        public IdNameDTO GetById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
