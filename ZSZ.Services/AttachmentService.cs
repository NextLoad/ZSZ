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
    public class AttachmentService : IAttachment
    {
        public long AddAttachment(string name, string iconName)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                AttachmentEntity attachment = new AttachmentEntity();
                attachment.Name = name;
                attachment.IconName = iconName;
                ctx.Attachments.Add(attachment);
                ctx.SaveChanges();
                return attachment.Id;
            }
        }

        public AttachmentDTO ToDTO(AttachmentEntity attachment)
        {
            AttachmentDTO attachmentDto = new AttachmentDTO();
            attachmentDto.IconName = attachment.IconName;
            attachmentDto.Name = attachment.Name;
            attachmentDto.Id = attachment.Id;
            attachmentDto.CreateDateTime = attachmentDto.CreateDateTime;
            return attachmentDto;
        }

        public AttachmentDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AttachmentEntity> cs = new CommonService<AttachmentEntity>(ctx);
                return cs.GetAll().AsNoTracking().ToList().Select(a => ToDTO(a)).ToArray();
            }
        }

        public AttachmentDTO[] GetAttachment(long houseId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<HouseEntity> cs = new CommonService<HouseEntity>(ctx);
                HouseEntity house = cs.GetById(houseId);
                if (house == null)
                {
                    throw new ArgumentException("房子不存在" + houseId);
                }

                return house.AttachmentEntities.ToList().Select(a => ToDTO(a)).ToArray();
            }
        }
    }
}
