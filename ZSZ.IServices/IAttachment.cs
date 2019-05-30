using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface IAttachment:IServiceAutofac
    {
        /// <summary>
        /// 新增房屋设备
        /// </summary>
        /// <param name="name"></param>
        /// <param name="iconName"></param>
        /// <returns></returns>
        long AddAttachment(string name, string iconName);

        /// <summary>
        /// 获取所有房屋设备
        /// </summary>
        /// <returns></returns>
        AttachmentDTO[] GetAll();

        /// <summary>
        /// 获取指定房屋的设备
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        AttachmentDTO[] GetAttachment(long houseId);
    }
}
