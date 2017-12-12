using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDALServer
{
   public interface IBaseDALServer<T> where T: class
    {
        /// <summary>
        /// 新增(实体)
        /// </summary>
        /// <param name="Entity">待新增实体</param>
        void Add(T Entity);

        /// <summary>
        /// 修改（实体）
        /// </summary>
        /// <param name="Entity">修改实体</param>
        void Edit(T Entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Entity"></param>
        void Del(T Entity);
        /// <summary>
        /// 提交改变  因为用了线程内唯一的
        /// </summary>
        /// <returns></returns>
        int SaveChange();

        /// <summary>
        /// 无分页查询
        /// </summary>
        /// <param name="wherelambda"></param>
        /// <returns></returns>
        IQueryable<T> LoadEntity(Expression<Func<T, bool>> wherelambda);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="total">查询记录总条数</param>
        /// <param name="whereLamda">组合查询条件</param>
        /// <param name="isAdc">是否升序</param>
        /// <param name="orderProperty">排序字段</param>
        /// <returns></returns>
        IQueryable<T> LoadEntityByPage1(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLamda, bool isAdc, string orderProperty);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit">每条记录</param>
        /// <param name="offset"></param>
        /// <param name="total"></param>
        /// <param name="whereLamdal"></param>
        /// <param name="isAsc"></param>
        /// <param name="orderByProperty"></param>
        /// <returns></returns>
        IQueryable<T> LoadEntityByPage2(int limit, int offset, out int total, Expression<Func<T, bool>> whereLamdal, bool isAsc, string orderByProperty);

        /// <summary>
        /// 执行sql语句的增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int CreateOrUpdateOrDeleteSql(string sql, params object[] parameters);
    }
}
