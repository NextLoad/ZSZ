using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IServices;
using ZSZ.Services.Entities;

namespace ZSZ.Services
{
    public class IdNameService : IdName
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
            IdNameDTO idDto = new IdNameDTO();
            idDto.Name = i.Name;
            idDto.TypeName = i.TypeName;
            idDto.Id = i.Id;
            idDto.CreateDateTime = i.CreateDateTime;
            return idDto;
        }

        public IdNameDTO[] GetByTypeName(string typeName)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                return ctx.IdNames.Where(i => i.TypeName == typeName).AsNoTracking().ToList().Select(ToDTO).ToArray();
            }
        }

        public long AddNew(string typeName, string name)
        {
            IdNameEntity idName = new IdNameEntity();
            idName.Name = name;
            idName.TypeName = typeName;
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                ctx.IdNames.Add(idName);
                ctx.SaveChanges();
                return idName.Id;
            }
        }

        public IdNameDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<IdNameEntity> csService = new CommonService<IdNameEntity>(ctx);
                return ToDTO(csService.GetById(id));
            }
        }
    }
}
