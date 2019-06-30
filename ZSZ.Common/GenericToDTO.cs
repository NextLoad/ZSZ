using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Common
{
    public class GenericToDTO
    {
        public static TDTO ToDTO<T, TDTO>(T t)
        {
            Type type = typeof(T);
            Type typeDto = typeof(TDTO);
            object dto = Activator.CreateInstance(typeDto);
            foreach (PropertyInfo prop in typeDto.GetProperties())
            {
                //获取实体类型t的对应的属性信息
                var tProp = type.GetProperty(prop.Name);
                if (tProp.CanRead)
                {
                    //获取t类型的属性值
                    object tValue = tProp.GetValue(t);

                    if (prop.CanWrite)
                    {
                        prop.SetValue(dto, tValue);
                    }
                }
            }
            return (TDTO)dto;
        }
    }
}
