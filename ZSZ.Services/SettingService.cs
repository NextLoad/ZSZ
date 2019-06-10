using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IServices;

namespace ZSZ.Services
{
    public class SettingService: ISetting
    {
        public long AddNewSetting(string name, string value)
        {
            throw new NotImplementedException();
        }

        public void UpdateSetting(long id, string name, string value)
        {
            throw new NotImplementedException();
        }

        public void MarkDeleted(long id)
        {
            throw new NotImplementedException();
        }

        public SettingDTO[] GetAll()
        {
            throw new NotImplementedException();
        }

        public SettingDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public SettingDTO GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
