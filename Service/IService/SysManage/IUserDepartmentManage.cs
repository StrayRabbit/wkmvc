using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.IService
{
    /// <summary>
    /// 用户部门关系业务接口
    /// add yuangang by 2016-05-19
    /// </summary>
    public interface IUserDepartmentManage : IRepository<Domain.SYS_USER_DEPARTMENT>
    {
        /// <summary>
        /// 根据部门ID获取当前部门的所有用户集合
        /// </summary>
        List<Domain.SYS_USER> GetUserListByDptId(List<string> dptId);
        /// <summary>
        /// 根据用户ID获取所在的部门集合
        /// </summary>
        List<Domain.SYS_DEPARTMENT> GetDptListByUserId(int userId);
        /// <summary>
        /// 保存用户部门关系
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="dptId">部门ID集合</param>
        /// <returns></returns>
        bool SaveUserDpt(int userId, string dptId);
    }
}