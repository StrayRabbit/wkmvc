using System;

namespace Service.IService
{
    public interface IUserRoleManage:IRepository<Domain.SYS_USER_ROLE>
    {
        /// <summary>
        /// 设置用户角色
        /// add yuangang by 2016-05-19
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID字符串</param>
        /// <returns></returns>
        bool SetUserRole(int userId, string roleId);
    }
}
