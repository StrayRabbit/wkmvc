using System;
using System.Linq;

namespace Service.ServiceImp
{
    public class UserRoleManage : RepositoryBase<Domain.SYS_USER_ROLE>, IService.IUserRoleManage
    {
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID字符串</param>
        /// <returns></returns>
        public bool SetUserRole(int userId, string roleId)
        {
            try
            {
                //删除用户角色
                this.Delete(p => p.FK_USERID == userId);
                //设置当前用户的角色
                if (string.IsNullOrEmpty(roleId)) return true;

                foreach (var entity in roleId.Split(',').Select(t => new Domain.SYS_USER_ROLE()
                {
                    FK_USERID = userId,
                    FK_ROLEID = int.Parse(t)
                }))
                {
                    this.dbSet.Add(entity);
                }

                return Context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
