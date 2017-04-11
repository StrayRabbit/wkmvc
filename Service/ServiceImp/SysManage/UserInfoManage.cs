using System;

namespace Service.ServiceImp
{
    public class UserInfoManage : RepositoryBase<Domain.SYS_USERINFO>, Service.IService.IUserInfoManage
    {

        public System.Data.Entity.DbContext Context
        {
            get { throw new NotImplementedException(); }
        }

        public Domain.MyConfig Config
        {
            get { throw new NotImplementedException(); }
        }

        public System.Data.Entity.DbSet<Domain.SYS_USERINFO> dbSet
        {
            get { throw new NotImplementedException(); }
        }

        public System.Data.Entity.DbContextTransaction Transaction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Committed
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public Domain.SYS_USERINFO Get(System.Linq.Expressions.Expression<Func<Domain.SYS_USERINFO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Save(Domain.SYS_USERINFO entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Domain.SYS_USERINFO entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(Domain.SYS_USERINFO entity, bool isEdit)
        {
            throw new NotImplementedException();
        }

        public int Delete(System.Linq.Expressions.Expression<Func<Domain.SYS_USERINFO, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public int DeleteBySql(string sql, params System.Data.Common.DbParameter[] para)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(System.Linq.Expressions.Expression<Func<Domain.SYS_USERINFO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(string sql, params System.Data.Common.DbParameter[] para)
        {
            throw new NotImplementedException();
        }

        public int SaveList<T1>(System.Collections.Generic.List<T1> t) where T1 : class
        {
            throw new NotImplementedException();
        }

        public int SaveList(System.Collections.Generic.List<Domain.SYS_USERINFO> t)
        {
            throw new NotImplementedException();
        }

        public int UpdateList<T1>(System.Collections.Generic.List<T1> t) where T1 : class
        {
            throw new NotImplementedException();
        }

        public int UpdateList(System.Collections.Generic.List<Domain.SYS_USERINFO> t)
        {
            throw new NotImplementedException();
        }

        public int DeleteList(System.Collections.Generic.List<Domain.SYS_USERINFO> t)
        {
            throw new NotImplementedException();
        }

        public int DeleteList<T1>(System.Collections.Generic.List<T1> t) where T1 : class
        {
            throw new NotImplementedException();
        }

        public object ExecuteProc(string procname, params System.Data.Common.DbParameter[] parameter)
        {
            throw new NotImplementedException();
        }

        public object ExecuteQueryProc(string procname, params System.Data.Common.DbParameter[] parameter)
        {
            throw new NotImplementedException();
        }

        public System.Linq.IQueryable<Domain.SYS_USERINFO> LoadAll(System.Linq.Expressions.Expression<Func<Domain.SYS_USERINFO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<Domain.SYS_USERINFO> LoadListAll(System.Linq.Expressions.Expression<Func<Domain.SYS_USERINFO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public System.Data.Entity.Infrastructure.DbQuery<Domain.SYS_USERINFO> LoadQueryAll(System.Linq.Expressions.Expression<Func<Domain.SYS_USERINFO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<Domain.SYS_USERINFO> LoadEnumerableAll(string sql, params System.Data.Common.DbParameter[] para)
        {
            throw new NotImplementedException();
        }

        public System.Collections.IEnumerable LoadEnumerable(string sql, params System.Data.Common.DbParameter[] para)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<Domain.SYS_USERINFO> SelectBySql(string sql, params System.Data.Common.DbParameter[] para)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<T1> SelectBySql<T1>(string sql, params System.Data.Common.DbParameter[] para)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<TResult> QueryEntity<TEntity, TOrderBy, TResult>(System.Linq.Expressions.Expression<Func<TEntity, bool>> where, System.Linq.Expressions.Expression<Func<TEntity, TOrderBy>> orderby, System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, bool IsAsc)
            where TEntity : class
            where TResult : class
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<object> QueryObject<TEntity, TOrderBy>(System.Linq.Expressions.Expression<Func<TEntity, bool>> where, System.Linq.Expressions.Expression<Func<TEntity, TOrderBy>> orderby, Func<System.Linq.IQueryable<TEntity>, System.Collections.Generic.List<object>> selector, bool IsAsc) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public dynamic QueryDynamic<TEntity, TOrderBy>(System.Linq.Expressions.Expression<Func<TEntity, bool>> where, System.Linq.Expressions.Expression<Func<TEntity, TOrderBy>> orderby, Func<System.Linq.IQueryable<TEntity>, System.Collections.Generic.List<object>> selector, bool IsAsc) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IList<T1> PageByListSql<T1>(string sql, System.Collections.Generic.IList<System.Data.Common.DbParameter> parameters, Common.PageCollection page)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IList<Domain.SYS_USERINFO> PageByListSql(string sql, System.Collections.Generic.IList<System.Data.Common.DbParameter> parameters, Common.PageCollection page)
        {
            throw new NotImplementedException();
        }

        public Common.PageInfo<object> Query<TEntity, TOrderBy>(int index, int pageSize, System.Linq.Expressions.Expression<Func<TEntity, bool>> where, System.Linq.Expressions.Expression<Func<TEntity, TOrderBy>> orderby, Func<System.Linq.IQueryable<TEntity>, System.Collections.Generic.List<object>> selector, bool IsAsc) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Common.PageInfo<Domain.SYS_USERINFO> Query(System.Linq.IQueryable<Domain.SYS_USERINFO> query, int index, int PageSize)
        {
            throw new NotImplementedException();
        }

        public Common.PageInfo Query(int index, int pageSize, string tableName, string field, string filter, string orderby, string group, params System.Data.Common.DbParameter[] para)
        {
            throw new NotImplementedException();
        }

        public Common.PageInfo Query(int index, int pageSize, string sql, string orderby, params System.Data.Common.DbParameter[] para)
        {
            throw new NotImplementedException();
        }

        public Common.PageInfo Query(System.Linq.IQueryable query, int index, int pagesize)
        {
            throw new NotImplementedException();
        }

        public object ExecuteSqlCommand(string sql, params System.Data.Common.DbParameter[] para)
        {
            throw new NotImplementedException();
        }

        public object ExecuteSqlCommand(System.Collections.Generic.Dictionary<string, object> sqllist)
        {
            throw new NotImplementedException();
        }

        public object ExecuteSqlQuery(string sql, params System.Data.Common.DbParameter[] para)
        {
            throw new NotImplementedException();
        }
    }
}
