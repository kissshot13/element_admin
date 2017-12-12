using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Model;
using System.Data;
using System.Data.Entity;

namespace DALServer
{
    public class BaseDALServer<T> where T: class
    {
        public DbContext db = EFContextFactory.GetCurrentObjectContext();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">待增加实体</param>
        public void Add(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Added;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">待修改实体</param>
        public void Edit(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public void Del(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Deleted;
        }

        public int SavaChanges()
        {
            return db.SaveChanges();
        }

        public IQueryable<T> LoadEntity(Expression<Func<T, bool>> whereL)
        {
            var tempData = db.Set<T>().AsQueryable();
            if (whereL != null)
            {
                tempData = tempData.Where(whereL);
            }
            else
            {
                return null;
            }
            return tempData.AsQueryable();
        }

        //public IQueryable<T> LoadEntityByPage1(int pageSize, int PageIndex, out int total, Expression<Func<T, bool>> whereLambda, bool isDesc, string orderByProperty)
        //{
        //    var tempData = db.Set<T>().AsQueryable();
        //    if (pageSize <= 0)
        //    {
        //        pageSize = 10;
        //    }
        //    if (whereLambda != null)
        //    {
        //        tempData = tempData.Where(whereLambda);
        //    }
        //    total = tempData.Count();
        //    if (PageIndex > 0)
        //    {
        //        tempData = tempData.OrderBy(orderByProperty, isDesc).Skip(pageSize * (PageIndex - 1)).Take(pageSize);
        //    }



        //}
    }
}
