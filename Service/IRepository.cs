using Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections;

namespace Service
{
    /// <summary>
    /// 所有的数据操作基类接口
    /// add yuangang by 2015-05-10
    /// </summary>
    public interface IRepository<T> where T : class
    {
        #region 数据对象操作
        /// <summary>
        /// 数据上下文
        /// </summary>
        DbContext _Context { get; }
        /// <summary>
        /// 数据上下文
        /// </summary>
        Domain.MyConfig Config { get; }
        #endregion

        #region 单模型 CRUD 操作
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool Save(T entity, bool IsCommit = true);
        /// <summary>
        /// 增加一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> SaveAsync(T entity, bool IsCommit = true);

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool Update(T entity, bool IsCommit = true);
        /// <summary>
        /// 更新一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity, bool IsCommit = true);

        /// <summary>
        /// 增加或更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="isEdit">是否增加</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool SaveOrUpdate(T entity, bool isEdit, bool IsCommit = true);
        /// <summary>
        /// 增加或更新一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="isEdit">是否增加</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> SaveOrUpdateAsync(T entity, bool isEdit, bool IsCommit = true);

        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 通过Lamda表达式获取实体（异步方式）
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool Delete(T entity, bool IsCommit = true);
        /// <summary>
        /// 删除一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity, bool IsCommit = true);

        #endregion

        #region 多模型操作
        /// <summary>
        /// 增加多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool SaveList(List<T> T1, bool IsCommit = true);
        /// <summary>
        /// 增加多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> SaveListAsync(List<T> T1, bool IsCommit = true);

        /// <summary>
        /// 增加多条记录，独立模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool SaveList<T1>(List<T1> T, bool IsCommit = true) where T1 : class;
        /// <summary>
        /// 增加多条记录，独立模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> SaveListAsync<T1>(List<T1> T, bool IsCommit = true) where T1 : class;

        /// <summary>
        /// 更新多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool UpdateList(List<T> T1, bool IsCommit = true);
        /// <summary>
        /// 更新多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> UpdateListAsync(List<T> T1, bool IsCommit = true);

        /// <summary>
        /// 更新多条记录，独立模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool UpdateList<T1>(List<T1> T, bool IsCommit = true) where T1 : class;
        /// <summary>
        /// 更新多条记录，独立模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> UpdateListAsync<T1>(List<T1> T, bool IsCommit = true) where T1 : class;

        /// <summary>
        /// 删除多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool DeleteList(List<T> T1, bool IsCommit = true);
        /// <summary>
        /// 删除多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> DeleteListAsync(List<T> T1, bool IsCommit = true);

        /// <summary>
        /// 删除多条记录，独立模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool DeleteList<T1>(List<T1> T, bool IsCommit = true) where T1 : class;
        /// <summary>
        /// 删除多条记录，独立模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> DeleteListAsync<T1>(List<T1> T, bool IsCommit = true) where T1 : class;

        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> predicate, bool IsCommit = true);
        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate, bool IsCommit = true);

        /// <summary>
        /// 执行SQL删除
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        int DeleteBySql(string sql, params DbParameter[] para);
        /// <summary>
        /// 执行SQL删除（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        Task<int> DeleteBySqlAsync(string sql, params DbParameter[] para);
        #endregion

        #region 存储过程操作
        /// <summary>
        /// 执行增删改存储过程
        /// </summary>
        object ExecuteProc(string procname, params DbParameter[] parameter);
        /// <summary>
        /// 执行查询的存储过程
        /// </summary>
        object ExecuteQueryProc(string procname, params DbParameter[] parameter);
        #endregion

        #region 获取多条数据操作

        /// <summary>
        /// 返回IQueryable集合，延时加载数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> LoadAll(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 返回IQueryable集合，延时加载数据（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IQueryable<T>> LoadAllAsync(Expression<Func<T, bool>> predicate);

        // <summary>
        /// 返回List<T>集合,不采用延时加载
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<T> LoadListAll(Expression<Func<T, bool>> predicate);
        // <summary>
        /// 返回List<T>集合,不采用延时加载（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<T>> LoadListAllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 获取DbQuery的列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        DbQuery<T> LoadQueryAll(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 获取DbQuery的列表（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<DbQuery<T>> LoadQueryAllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 获取IEnumerable列表
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        IEnumerable<T> LoadEnumerableAll(string sql, params DbParameter[] para);
        /// <summary>
        /// 获取IEnumerable列表（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        Task<IEnumerable<T>> LoadEnumerableAllAsync(string sql, params DbParameter[] para);

        /// <summary>
        /// 获取数据动态集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        IEnumerable LoadEnumerable(string sql, params DbParameter[] para);
        /// <summary>
        /// 获取数据动态集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        Task<IEnumerable> LoadEnumerableAsync(string sql, params DbParameter[] para);

        /// <summary>
        /// 采用SQL进行数据的查询，返回IList集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        List<T> SelectBySql(string sql, params DbParameter[] para);
        /// <summary>
        /// 采用SQL进行数据的查询，返回IList集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        Task<List<T>> SelectBySqlAsync(string sql, params DbParameter[] para);

        /// <summary>
        /// 采用SQL进行数据的查询，指定泛型，返回IList集合
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        List<T1> SelectBySql<T1>(string sql, params DbParameter[] para);
        /// <summary>
        /// 采用SQL进行数据的查询，指定泛型，返回IList集合
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        Task<List<T1>> SelectBySqlAsync<T1>(string sql, params DbParameter[] para);

        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回实体对象集合
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <typeparam name="TResult">数据结果，与TEntity一致</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>实体集合</returns>
        List<TResult> QueryEntity<TEntity, TOrderBy, TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Expression<Func<TEntity, TResult>> selector, bool IsAsc)
            where TEntity : class
            where TResult : class;
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回实体对象集合（异步方式）
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <typeparam name="TResult">数据结果，与TEntity一致</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>实体集合</returns>
        Task<List<TResult>> QueryEntityAsync<TEntity, TOrderBy, TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Expression<Func<TEntity, TResult>> selector, bool IsAsc)
            where TEntity : class
            where TResult : class;

        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回Object对象集合
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>自定义实体集合</returns>
        List<object> QueryObject<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Func<IQueryable<TEntity>, List<object>> selector, bool IsAsc)
            where TEntity : class;
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回Object对象集合（异步方式）
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>自定义实体集合</returns>
        Task<List<object>> QueryObjectAsync<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Func<IQueryable<TEntity>, List<object>> selector, bool IsAsc)
            where TEntity : class;

        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回动态类对象集合
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>动态类</returns>
        dynamic QueryDynamic<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Func<IQueryable<TEntity>, List<object>> selector, bool IsAsc)
            where TEntity : class;
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回动态类对象集合（异步方式）
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>动态类</returns>
        Task<dynamic> QueryDynamicAsync<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Func<IQueryable<TEntity>, List<object>> selector, bool IsAsc)
            where TEntity : class;

        #endregion

        #region 验证是否存在

        /// <summary>
        /// 验证当前条件是否存在相同项
        /// </summary>
        bool IsExist(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 验证当前条件是否存在相同项（异步方式）
        /// </summary>
        Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据SQL验证实体对象是否存在
        /// </summary>
        bool IsExist(string sql, params DbParameter[] para);
        /// <summary>
        /// 根据SQL验证实体对象是否存在（异步方式）
        /// </summary>
        Task<bool> IsExistAsync(string sql, params DbParameter[] para);

        #endregion

        #region 分页查询
        /// <summary>
        /// 通用EF分页，默认显示20条记录
        /// </summary>
        /// <typeparam name="TEntity">实体模型</typeparam>
        /// <typeparam name="TOrderBy">排序类型</typeparam>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">显示条数</param>
        /// <param name="where">过滤条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">结果集合</param>
        /// <param name="isAsc">排序方向true正序 false倒序</param>
        /// <returns>自定义实体集合</returns>
        PageInfo<object> Query<TEntity, TOrderBy>
            (int index, int pageSize,
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Func<IQueryable<TEntity>, List<object>> selector,
            bool IsAsc)
            where TEntity : class;
        /// <summary>
        /// 对IQueryable对象进行分页逻辑处理，过滤、查询项、排序对IQueryable操作
        /// </summary>
        /// <param name="t">Iqueryable</param>
        /// <param name="index">当前页</param>
        /// <param name="PageSize">每页显示多少条</param>
        /// <returns>当前IQueryable to List的对象</returns>
        Common.PageInfo<T> Query(IQueryable<T> query, int index, int PageSize);
        /// <summary>
        /// 普通SQL查询分页方法
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">显示行数</param>
        /// <param name="tableName">表名/视图</param>
        /// <param name="field">获取项</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="orderby">排序字段+排序方向</param>
        /// <param name="group">分组字段</param>
        /// <returns>结果集</returns>
        Common.PageInfo Query(int index, int pageSize, string tableName, string field, string filter, string orderby, string group, params DbParameter[] para);
        /// <summary>
        /// 简单的Sql查询分页
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        Common.PageInfo Query(int index, int pageSize, string sql, string orderby, params DbParameter[] para);
        /// <summary>
        /// 多表联合分页算法
        /// </summary>
        PageInfo Query(IQueryable query, int index, int pagesize);
        #endregion

        #region ADO.NET增删改查方法
        /// <summary>
        /// 执行增删改方法,含事务处理
        /// </summary>
        object ExecuteSqlCommand(string sql, params DbParameter[] para);
        /// <summary>
        /// 执行多条SQL，增删改方法,含事务处理
        /// </summary>
        object ExecuteSqlCommand(Dictionary<string, object> sqllist);
        /// <summary>
        /// 执行查询方法,返回动态类，接收使用var，遍历时使用dynamic类型
        /// </summary>
        object ExecuteSqlQuery(string sql, params DbParameter[] para);
        #endregion

        #region 更新操作
        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="dic">被解析的字段</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        bool Modify(string table, Dictionary<string, object> dic, string where);
        #endregion
    }
}
