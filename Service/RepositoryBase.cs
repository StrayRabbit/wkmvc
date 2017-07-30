using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Domain;
using System.Linq.Expressions;
using System.Collections;
using System.Threading.Tasks;
using Common;

namespace Service
{
    /// <summary>
    /// 数据操作基本实现类，公用实现方法
    /// add yuangang by 2015-05-10
    /// </summary>
    /// <typeparam name="T">具体操作的实体模型</typeparam>
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region 固定公用帮助，含事务

        private DbContext context = new MyConfig().db;
        /// <summary>
        /// 数据上下文
        /// </summary>
        public DbContext _Context
        {
            get
            {
                context.Configuration.ValidateOnSaveEnabled = false;
                return context;
            }
        }
        /// <summary>
        /// 数据上下文--->拓展属性
        /// </summary>
        public MyConfig Config
        {
            get
            {
                return new MyConfig();
            }
        }
        #endregion

        #region 单模型 CRUD 操作
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool Save(T entity, bool IsCommit = true)
        {
            _Context.Set<T>().Add(entity);
            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 增加一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> SaveAsync(T entity, bool IsCommit = true)
        {
            _Context.Set<T>().Add(entity);
            if (IsCommit)
                return await Task.Run(() => _Context.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool Update(T entity, bool IsCommit = true)
        {
            _Context.Set<T>().Attach(entity);
            _Context.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 更新一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(T entity, bool IsCommit = true)
        {
            _Context.Set<T>().Attach(entity);
            _Context.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            if (IsCommit)
                return await Task.Run(() => _Context.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 增加或更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsSave">是否增加</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool SaveOrUpdate(T entity, bool isEdit, bool IsCommit = true)
        {
            return isEdit ? Update(entity, IsCommit) : Save(entity, IsCommit);
        }
        /// <summary>
        /// 增加或更新一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsSave">是否增加</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> SaveOrUpdateAsync(T entity, bool isEdit, bool IsCommit = true)
        {
            return isEdit ? await UpdateAsync(entity, IsCommit) : await SaveAsync(entity, IsCommit);
        }

        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return _Context.Set<T>().AsNoTracking().SingleOrDefault(predicate);
        }
        /// <summary>
        /// 通过Lamda表达式获取实体（异步方式）
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => _Context.Set<T>().AsNoTracking().SingleOrDefault(predicate));
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool Delete(T entity, bool IsCommit = true)
        {
            if (entity == null) return false;
            _Context.Set<T>().Attach(entity);
            _Context.Set<T>().Remove(entity);

            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 删除一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(T entity, bool IsCommit = true)
        {
            if (entity == null) return await Task.Run(() => false);
            _Context.Set<T>().Attach(entity);
            _Context.Set<T>().Remove(entity);
            if (IsCommit)
                return await Task.Run(() => _Context.SaveChanges() > 0);
            else
                return await Task.Run(() => false); ;
        }

        #endregion

        #region 多模型操作
        /// <summary>
        /// 增加多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool SaveList(List<T> T1, bool IsCommit = true)
        {
            if (T1 == null || T1.Count == 0) return false;

            T1.ToList().ForEach(item =>
            {
                _Context.Set<T>().Add(item);
            });

            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 增加多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> SaveListAsync(List<T> T1, bool IsCommit = true)
        {
            if (T1 == null || T1.Count == 0) return await Task.Run(() => false);

            T1.ToList().ForEach(item =>
            {
                _Context.Set<T>().Add(item);
            });

            if (IsCommit)
                return await Task.Run(() => _Context.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 增加多条记录，独立模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool SaveList<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return false;
            _Context.Set<T1>().Local.Clear();
            T.ToList().ForEach(item =>
            {
                _Context.Set<T1>().Add(item);
            });
            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 增加多条记录，独立模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> SaveListAsync<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return await Task.Run(() => false);
            _Context.Set<T1>().Local.Clear();
            T.ToList().ForEach(item =>
            {
                _Context.Set<T1>().Add(item);
            });
            if (IsCommit)
                return await Task.Run(() => _Context.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 更新多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool UpdateList(List<T> T1, bool IsCommit = true)
        {
            if (T1 == null || T1.Count == 0) return false;

            T1.ToList().ForEach(item =>
            {
                _Context.Set<T>().Attach(item);
                _Context.Entry<T>(item).State = System.Data.Entity.EntityState.Modified;
            });

            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 更新多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateListAsync(List<T> T1, bool IsCommit = true)
        {
            if (T1 == null || T1.Count == 0) return await Task.Run(() => false);

            T1.ToList().ForEach(item =>
            {
                _Context.Set<T>().Attach(item);
                _Context.Entry<T>(item).State = System.Data.Entity.EntityState.Modified;
            });

            if (IsCommit)
                return await Task.Run(() => _Context.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 更新多条记录，独立模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool UpdateList<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return false;

            T.ToList().ForEach(item =>
            {
                _Context.Set<T1>().Attach(item);
                _Context.Entry<T1>(item).State = System.Data.Entity.EntityState.Modified;
            });

            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 更新多条记录，独立模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateListAsync<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return await Task.Run(() => false);

            T.ToList().ForEach(item =>
            {
                _Context.Set<T1>().Attach(item);
                _Context.Entry<T1>(item).State = System.Data.Entity.EntityState.Modified;
            });

            if (IsCommit)
                return await Task.Run(() => _Context.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 删除多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool DeleteList(List<T> T1, bool IsCommit = true)
        {
            if (T1 == null || T1.Count == 0) return false;

            T1.ToList().ForEach(item =>
            {
                _Context.Set<T>().Attach(item);
                _Context.Set<T>().Remove(item);
            });

            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 删除多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteListAsync(List<T> T1, bool IsCommit = true)
        {
            if (T1 == null || T1.Count == 0) return await Task.Run(() => false);

            T1.ToList().ForEach(item =>
            {
                _Context.Set<T>().Attach(item);
                _Context.Set<T>().Remove(item);
            });

            if (IsCommit)
                return await Task.Run(() => _Context.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 删除多条记录，独立模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool DeleteList<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return false;

            T.ToList().ForEach(item =>
            {
                _Context.Set<T1>().Attach(item);
                _Context.Set<T1>().Remove(item);
            });

            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 删除多条记录，独立模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteListAsync<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return await Task.Run(() => false);

            T.ToList().ForEach(item =>
            {
                _Context.Set<T1>().Attach(item);
                _Context.Set<T1>().Remove(item);
            });

            if (IsCommit)
                return await Task.Run(() => _Context.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public virtual bool Delete(Expression<Func<T, bool>> predicate, bool IsCommit = true)
        {
            IQueryable<T> entry = (predicate == null) ? _Context.Set<T>().AsQueryable() : _Context.Set<T>().Where(predicate);
            List<T> list = entry.ToList();

            if (list != null && list.Count == 0) return false;
            list.ForEach(item =>
            {
                _Context.Set<T>().Attach(item);
                _Context.Set<T>().Remove(item);
            });

            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate, bool IsCommit = true)
        {
            IQueryable<T> entry = (predicate == null) ? _Context.Set<T>().AsQueryable() : _Context.Set<T>().Where(predicate);
            List<T> list = entry.ToList();

            if (list != null && list.Count == 0) return await Task.Run(() => false);
            list.ForEach(item =>
            {
                _Context.Set<T>().Attach(item);
                _Context.Set<T>().Remove(item);
            });

            if (IsCommit)
                return await Task.Run(() => _Context.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 执行SQL删除
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        public virtual int DeleteBySql(string sql, params DbParameter[] para)
        {
            return _Context.Database.ExecuteSqlCommand(sql, para);
        }
        /// <summary>
        /// 执行SQL删除（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        public virtual async Task<int> DeleteBySqlAsync(string sql, params DbParameter[] para)
        {
            return await Task.Run(() => _Context.Database.ExecuteSqlCommand(sql, para));
        }
        #endregion

        #region 获取多条数据操作

        /// <summary>
        /// 返回IQueryable集合，延时加载数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> LoadAll(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? _Context.Set<T>().Where(predicate).AsNoTracking<T>() : _Context.Set<T>().AsQueryable<T>().AsNoTracking<T>();
        }
        /// <summary>
        /// 返回IQueryable集合，延时加载数据（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<IQueryable<T>> LoadAllAsync(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? await Task.Run(() => _Context.Set<T>().Where(predicate).AsNoTracking<T>()) : await Task.Run(() => _Context.Set<T>().AsQueryable<T>().AsNoTracking<T>());
        }

        // <summary>
        /// 返回List<T>集合,不采用延时加载
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual List<T> LoadListAll(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? _Context.Set<T>().Where(predicate).AsNoTracking().ToList() : _Context.Set<T>().AsQueryable<T>().AsNoTracking().ToList();
        }
        // <summary>
        /// 返回List<T>集合,不采用延时加载（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> LoadListAllAsync(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? await Task.Run(() => _Context.Set<T>().Where(predicate).AsNoTracking().ToList()) : await Task.Run(() => _Context.Set<T>().AsQueryable<T>().AsNoTracking().ToList());
        }

        /// <summary>
        /// 获取DbQuery的列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual DbQuery<T> LoadQueryAll(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? _Context.Set<T>().Where(predicate) as DbQuery<T> : _Context.Set<T>();
        }
        /// <summary>
        /// 获取DbQuery的列表（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<DbQuery<T>> LoadQueryAllAsync(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? await Task.Run(() => _Context.Set<T>().Where(predicate) as DbQuery<T>) : await Task.Run(() => _Context.Set<T>());
        }

        /// <summary>
        /// 获取IEnumerable列表
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual IEnumerable<T> LoadEnumerableAll(string sql, params DbParameter[] para)
        {
            return _Context.Database.SqlQuery<T>(sql, para);
        }
        /// <summary>
        /// 获取IEnumerable列表（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> LoadEnumerableAllAsync(string sql, params DbParameter[] para)
        {
            return await Task.Run(() => _Context.Database.SqlQuery<T>(sql, para));
        }

        /// <summary>
        /// 获取数据动态集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual IEnumerable LoadEnumerable(string sql, params DbParameter[] para)
        {
            return _Context.Database.SqlQueryForDynamic(sql, para);
        }
        /// <summary>
        /// 获取数据动态集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable> LoadEnumerableAsync(string sql, params DbParameter[] para)
        {
            return await Task.Run(() => _Context.Database.SqlQueryForDynamic(sql, para));
        }

        /// <summary>
        /// 采用SQL进行数据的查询，返回IList集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual List<T> SelectBySql(string sql, params DbParameter[] para)
        {
            return _Context.Database.SqlQuery(typeof(T), sql, para).Cast<T>().ToList();
        }
        /// <summary>
        /// 采用SQL进行数据的查询，返回IList集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual async Task<List<T>> SelectBySqlAsync(string sql, params DbParameter[] para)
        {
            return await Task.Run(() => _Context.Database.SqlQuery(typeof(T), sql, para).Cast<T>().ToList());
        }

        /// <summary>
        /// 采用SQL进行数据的查询，指定泛型，返回IList集合
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public virtual List<T1> SelectBySql<T1>(string sql, params DbParameter[] para)
        {
            return _Context.Database.SqlQuery<T1>(sql, para).ToList();
        }
        /// <summary>
        /// 采用SQL进行数据的查询，指定泛型，返回IList集合
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public virtual async Task<List<T1>> SelectBySqlAsync<T1>(string sql, params DbParameter[] para)
        {
            return await Task.Run(() => _Context.Database.SqlQuery<T1>(sql, para).ToList());
        }

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
        public virtual List<TResult> QueryEntity<TEntity, TOrderBy, TResult>
            (Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Expression<Func<TEntity, TResult>> selector,
            bool IsAsc)
            where TEntity : class
            where TResult : class
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (selector == null)
            {
                return query.Cast<TResult>().AsNoTracking().ToList();
            }
            return query.Select(selector).AsNoTracking().ToList();
        }
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
        public virtual async Task<List<TResult>> QueryEntityAsync<TEntity, TOrderBy, TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Expression<Func<TEntity, TResult>> selector, bool IsAsc)
            where TEntity : class
            where TResult : class
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (selector == null)
            {
                return await Task.Run(() => query.Cast<TResult>().AsNoTracking().ToList());
            }
            return await Task.Run(() => query.Select(selector).AsNoTracking().ToList());
        }

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
        public virtual List<object> QueryObject<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Func<IQueryable<TEntity>, List<object>> selector, bool IsAsc)
            where TEntity : class
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (selector == null)
            {
                return query.AsNoTracking().ToList<object>();
            }
            return selector(query);
        }
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
        public virtual async Task<List<object>> QueryObjectAsync<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Func<IQueryable<TEntity>, List<object>> selector, bool IsAsc)
            where TEntity : class
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (selector == null)
            {
                return await Task.Run(() => query.AsNoTracking().ToList<object>());
            }
            return await Task.Run(() => selector(query));
        }

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
        public dynamic QueryDynamic<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Func<IQueryable<TEntity>, List<object>> selector, bool IsAsc)
            where TEntity : class
        {
            List<object> list = QueryObject<TEntity, TOrderBy>
                 (where, orderby, selector, IsAsc);
            return JsonConverter.JsonClass(list);
        }
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
        public virtual async Task<dynamic> QueryDynamicAsync<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Func<IQueryable<TEntity>, List<object>> selector, bool IsAsc)
            where TEntity : class
        {
            List<object> list = QueryObject<TEntity, TOrderBy>
                 (where, orderby, selector, IsAsc);
            return await Task.Run(() => JsonConverter.JsonClass(list));
        }

        #endregion

        #region 验证是否存在

        /// <summary>
        /// 验证当前条件是否存在相同项
        /// </summary>
        public virtual bool IsExist(Expression<Func<T, bool>> predicate)
        {
            var entry = _Context.Set<T>().Where(predicate);
            return (entry.Any());
        }
        /// <summary>
        /// 验证当前条件是否存在相同项（异步方式）
        /// </summary>
        public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
        {
            var entry = _Context.Set<T>().Where(predicate);
            return await Task.Run(() => entry.Any());
        }

        /// <summary>
        /// 根据SQL验证实体对象是否存在
        /// </summary>
        public virtual bool IsExist(string sql, params DbParameter[] para)
        {
            IEnumerable result = _Context.Database.SqlQuery(typeof(int), sql, para);

            if (result.GetEnumerator().Current == null || result.GetEnumerator().Current.ToString() == "0")
                return false;
            return true;
        }
        /// <summary>
        /// 根据SQL验证实体对象是否存在（异步方式）
        /// </summary>
        public virtual async Task<bool> IsExistAsync(string sql, params DbParameter[] para)
        {
            IEnumerable result = _Context.Database.SqlQuery(typeof(int), sql, para);

            if (result.GetEnumerator().Current == null || result.GetEnumerator().Current.ToString() == "0")
                return await Task.Run(() => false);
            return await Task.Run(() => true);
        }

        #endregion

        #region 存储过程操作
        /// <summary>
        /// 执行返回影响行数的存储过程
        /// </summary>
        /// <param name="procname">过程名称</param>
        /// <param name="parameter">参数对象</param>
        /// <returns></returns>
        public virtual object ExecuteProc(string procname, params DbParameter[] parameter)
        {
            return ExecuteSqlCommand(procname, parameter);
        }
        /// <summary>
        /// 执行返回结果集的存储过程
        /// </summary>
        /// <param name="procname">过程名称</param>
        /// <param name="parameter">参数对象</param>
        /// <returns></returns>
        public virtual object ExecuteQueryProc(string procname, params DbParameter[] parameter)
        {
            return _Context.Database.SqlFunctionForDynamic(procname, parameter);
        }
        #endregion

        #region 分页操作
        /// <summary>
        /// 对IQueryable对象进行分页逻辑处理，过滤、查询项、排序对IQueryable操作
        /// </summary>
        /// <param name="t">Iqueryable</param>
        /// <param name="index">当前页</param>
        /// <param name="PageSize">每页显示多少条</param>
        /// <returns>当前IQueryable to List的对象</returns>
        public virtual Common.PageInfo<T> Query(IQueryable<T> query, int index, int PageSize)
        {
            if (index < 1)
            {
                index = 1;
            }
            if (PageSize <= 0)
            {
                PageSize = 20;
            }
            int count = query.Count();

            int maxpage = count / PageSize;

            if (count % PageSize > 0)
            {
                maxpage++;
            }
            if (index > maxpage)
            {
                index = maxpage;
            }
            if (count > 0)
                query = query.Skip((index - 1) * PageSize).Take(PageSize);
            return new Common.PageInfo<T>(index, PageSize, count, query.ToList());
        }
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
        public virtual Common.PageInfo<object> Query<TEntity, TOrderBy>
            (int index, int pageSize,
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Func<IQueryable<TEntity>,
            List<object>> selector,
            bool isAsc)
            where TEntity : class
        {
            if (index < 1)
            {
                index = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 20;
            }

            IQueryable<TEntity> query = _Context.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }
            int count = query.Count();

            int maxpage = count / pageSize;

            if (count % pageSize > 0)
            {
                maxpage++;
            }
            if (index > maxpage)
            {
                index = maxpage;
            }

            if (orderby != null)
            {
                query = isAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (count > 0)
                query = query.Skip((index - 1) * pageSize).Take(pageSize);
            //返回结果为null，返回所有字段
            if (selector == null)
                return new Common.PageInfo<object>(index, pageSize, count, query.ToList<object>());
            return new Common.PageInfo<object>(index, pageSize, count, selector(query).ToList());
        }
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
        public virtual Common.PageInfo Query(int index, int pageSize, string tableName, string field, string filter, string orderby, string group, params DbParameter[] para)
        {
            //执行分页算法
            if (index <= 0)
                index = 1;
            int start = (index - 1) * pageSize;
            if (start > 0)
                start -= 1;
            else
                start = 0;
            int end = index * pageSize;

            #region 查询逻辑
            string logicSql = "SELECT";
            //查询项
            if (!string.IsNullOrEmpty(field))
            {
                logicSql += " " + field;
            }
            else
            {
                logicSql += " *";
            }
            logicSql += " FROM (" + tableName + " ) where";
            //过滤条件
            if (!string.IsNullOrEmpty(filter))
            {
                logicSql += " " + filter;
            }
            else
            {
                filter = " 1=1";
                logicSql += "  1=1";
            }
            //分组
            if (!string.IsNullOrEmpty(group))
            {
                logicSql += " group by " + group;
            }

            #endregion

            //获取当前条件下数据总条数
            int count = _Context.Database.SqlQuery(typeof(int), "select count(*) from (" + tableName + ") where " + filter, para).Cast<int>().FirstOrDefault();
            string sql = "SELECT T.* FROM ( SELECT B.* FROM ( SELECT A.*,ROW_NUMBER() OVER(ORDER BY getdate()) as RN" +
                         logicSql + ") A ) B WHERE B.RN<=" + end + ") T WHERE T.RN>" + start;
            //排序
            if (!string.IsNullOrEmpty(orderby))
            {
                sql += " order by " + orderby;
            }
            var list = ExecuteSqlQuery(sql, para) as IEnumerable;
            if (list != null)
                return new Common.PageInfo(index, pageSize, count, list);
            return new Common.PageInfo(index, pageSize, count, new { });
        }

        /// <summary>
        /// 最简单的SQL分页
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="pageSize">显示行数</param>
        /// <param name="sql">纯SQL语句</param>
        /// <param name="orderby">排序字段与方向</param>
        /// <returns></returns>
        public virtual Common.PageInfo Query(int index, int pageSize, string sql, string orderby, params DbParameter[] para)
        {
            return this.Query(index, pageSize, sql, null, null, orderby, null, para);
        }
        /// <summary>
        /// 多表联合分页算法
        /// </summary>
        public virtual Common.PageInfo Query(IQueryable query, int index, int PageSize)
        {
            var enumerable = (query as System.Collections.IEnumerable).Cast<object>();
            if (index < 1)
            {
                index = 1;
            }
            if (PageSize <= 0)
            {
                PageSize = 20;
            }

            int count = enumerable.Count();

            int maxpage = count / PageSize;

            if (count % PageSize > 0)
            {
                maxpage++;
            }
            if (index > maxpage)
            {
                index = maxpage;
            }
            if (count > 0)
                enumerable = enumerable.Skip((index - 1) * PageSize).Take(PageSize);
            return new Common.PageInfo(index, PageSize, count, JsonConverter.JsonClass(enumerable.ToList()));
        }
        #endregion

        #region ADO.NET增删改查方法
        /// <summary>
        /// 执行增删改方法,含事务处理
        /// </summary>
        public virtual object ExecuteSqlCommand(string sql, params DbParameter[] para)
        {
            return _Context.Database.ExecuteSqlCommand(sql, para);

        }
        /// <summary>
        /// 执行多条SQL，增删改方法,含事务处理
        /// </summary>
        public virtual object ExecuteSqlCommand(Dictionary<string, object> sqllist)
        {
            int rows = 0;
            IEnumerator<KeyValuePair<string, object>> enumerator = sqllist.GetEnumerator();

            while (enumerator.MoveNext())
            {
                rows += _Context.Database.ExecuteSqlCommand(enumerator.Current.Key, enumerator.Current.Value);
            }
            return rows;

        }
        /// <summary>
        /// 执行查询方法,返回动态类，接收使用var，遍历时使用dynamic类型
        /// </summary>
        public virtual object ExecuteSqlQuery(string sql, params DbParameter[] para)
        {
            return _Context.Database.SqlQueryForDynamic(sql, para);
        }

        #endregion

        #region 更新操作
        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="dic">被解析的字段</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public bool Modify(string table, Dictionary<string, object> dic, string where)
        {
            try
            {
                if (dic == null || dic.Count <= 0)
                    return false;
                List<DbParameter> list = new List<DbParameter>();
                //解析字典
                string col = string.Empty;
                //字典反射
                var kv = dic.GetEnumerator();
                while (kv.MoveNext())
                {
                    var current = kv.Current;
                    if (!string.IsNullOrEmpty(current.Key) && current.Value != null)
                    {
                        col += current.Key.ToLower() + "=@" + current.Key.ToLower() + ",";
                        list.Add(new System.Data.SqlClient.SqlParameter("@" + current.Key.ToLower(), current.Value));
                    }
                }
                col = col.Trim(',');
                //拼接SQL
                string sql = "update " + table + " set " + col + " where 1=1 " + where;
                //执行
                object obj = this.ExecuteSqlCommand(sql, list.ToArray());
                return obj.ToString() != "0";
            }
            catch (Exception e) { throw e; }
        }
        #endregion

    }
}
