using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface IHouseAppointment : IServiceAutofac
    {
        /// <summary>
        /// 新增预约看房信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        /// <param name="visiDate"></param>
        /// <param name="houseId"></param>
        /// <param name="status"></param>
        /// <param name="followAdminUserId"></param>
        /// <param name="followDateTime"></param>
        /// <returns></returns>
        long AddNewHouseAppointment(long? userId,
                                    string name,
                                    string phoneNum,
                                    DateTime visiDate,
                                    long houseId
                                    );

        /// <summary>
        /// 更新预约看房信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        /// <param name="visiDate"></param>
        /// <param name="houseId"></param>
        /// <param name="status"></param>
        /// <param name="followAdminUserId"></param>
        /// <param name="followDateTime"></param>
        void UpdateHouseAppointment(long id,
                                    long? userId,
                                    string name,
                                    string phoneNum,
                                    DateTime visiDate,
                                    long houseId,
                                    string status,
                                    long? followAdminUserId,
                                    DateTime followDateTime);

        bool Follow(long adminUserId, long houseAppointmentId); //使用乐观锁解决并发的问题（这里先不实现，先抛个异常，后面再做）

        /// <summary>
        /// 获取某个城市下状态为status的预约信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        long GetTotalCount(long cityId, string status);

        /// <summary>
        /// 分页获取预约信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="status"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        HouseAppointmentDTO[] GetPagedData(long cityId, String status, int pageSize, int currentIndex);

    }
}
