using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IServices;

namespace ZSZ.Services
{
    public class HouseAppointmentService: IHouseAppointment
    {
        public long AddNewHouseAppointment(long? userId, string name, string phoneNum, DateTime visiDate, long houseId)
        {
            throw new NotImplementedException();
        }

        public void UpdateHouseAppointment(long id, long? userId, string name, string phoneNum, DateTime visiDate, long houseId,
            string status, long? followAdminUserId, DateTime followDateTime)
        {
            throw new NotImplementedException();
        }

        public bool Follow(long adminUserId, long houseAppointmentId)
        {
            throw new NotImplementedException();
        }

        public long GetTotalCount(long cityId, string status)
        {
            throw new NotImplementedException();
        }

        public HouseAppointmentDTO[] GetPagedData(long cityId, string status, int pageSize, int currentIndex)
        {
            throw new NotImplementedException();
        }
    }
}
