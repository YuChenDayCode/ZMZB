using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Data.ORM
{
    public interface IDbProvider<T>
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> All();
        /// <summary>
        /// 查询单条符合条件的数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns>数据</returns>
        T Get(Expression<Func<T, object>> exp);

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <param name="top">获取多少条(默认全部)</param>
        /// <returns>数据</returns>
        IEnumerable<T> GetList(Expression<Func<T, object>> exp, int top = -1);

        /// <summary>
        /// 根据条件获取条数
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <param name="CountField">统计字段,默认主键</param>
        /// <returns>总条数</returns>
        int Count(Expression<Func<T, object>> exp, Expression<Func<T, object>> CountField = null);

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">展示条数</param>
        /// <param name="TotalCount">返回总条数</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortWay">排序方式(默认升序)</param>
        /// <param name="SortField1">排序字段1</param>
        /// <param name="SortWay1">排序方式1(默认升序)</param>
        /// <returns>数据</returns>
        IEnumerable<T> GetListPage(Expression<Func<T, object>> exp, int PageIndex, int PageSize, out int TotalCount, Expression<Func<T, object>> SortField = null, string SortWay = "ASC", Expression<Func<T, object>> SortField1 = null, string SortWay1 = "ASC");



        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>是否成功</returns>
        bool Insert(T entity);

        /// <summary>
        /// 插入数据并返回主键
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="Id">主键</param>
        void Insert_Return_Id(T entity, out int Id);


        /// <summary>
        /// 更新指定实体
        /// </summary>
        /// <param name="entity">实体</param>
        bool Update(T entity, Expression<Func<T, object>> expressions);

        /// <summary>
        /// 更新指定字段
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fields">需要更新的字段</param>
        bool Update(T entity, Expression<Func<T, object>> expressions, string[] fields);


        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="expressions">条件表达式</param>
        /// <returns></returns>
        int Delete(Expression<Func<T, object>> expressions);

        /// <summary>
        /// 根据主键集合删除
        /// </summary>
        /// <param name="pkvlues"></param>
        int Delete<K>(IEnumerable<K> pkvlues);

    }
}
