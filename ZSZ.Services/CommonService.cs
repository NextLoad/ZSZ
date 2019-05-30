using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Services.Entities;

namespace ZSZ.Services
{
    class CommonService<T> where T : BaseEntity
    {
        private ZSZDbContext ctx;
        public CommonService(ZSZDbContext ctx)
        {
            this.ctx = ctx;
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return ctx.Set<T>().Where(t => t.IsDelete == false);
        }
        /// <summary>
        /// 根据Id获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public T GetById(long Id)
        {
            return GetAll().SingleOrDefault(t => t.Id == Id);
        }
        /// <summary>
        /// 获取数据总量
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()
        {
            return GetAll().LongCount();
        }
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public IQueryable<T> GetPageData(int pageIndex, int pageCount)
        {
            return GetAll().OrderByDescending(t => t.CreateDateTime).Skip((pageIndex - 1) * pageCount).Take(pageCount);
        }
        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool MarkDeleted(long id)
        {
            var item = GetById(id);
            item.IsDelete = true;
            ctx.SaveChanges();
            return true;

        }
    }
}
