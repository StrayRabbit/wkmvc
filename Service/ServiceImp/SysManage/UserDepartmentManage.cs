using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.ServiceImp
{
    /// <summary>
    /// 用户部门关系业务实现类
    /// add yuangang by 2016-05-19
    /// </summary>
    public class UserDepartmentManage : RepositoryBase<Domain.SYS_USER_DEPARTMENT>, IService.IUserDepartmentManage
    {
        /// <summary>
        /// 根据部门ID获取当前部门的所有用户ID集合
        /// </summary>
        public List<Domain.SYS_USER> GetUserListByDptId(List<string> dptId)
        {
            return this.LoadAll(p => dptId.Contains(p.DEPARTMENT_ID)).Select(p => p.SYS_USER).ToList();
        }
        /// <summary>
        /// 根据用户ID获取所在的部门ID集合
        /// </summary>
        public List<Domain.SYS_DEPARTMENT> GetDptListByUserId(int userId)
        {
            return this.LoadAll(p => p.USER_ID == userId).Select(p => p.SYS_DEPARTMENT).ToList();
        }

        /// <summary>
        /// 保存用户部门关系
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="dptId">部门ID集合</param>
        public bool SaveUserDpt(int userId, string dptId)
        {
            try
            {
                //原始部门人员关系是否与当前设置一致，不一致重新构造
                if (this.IsExist(p => p.USER_ID == userId))
                {
                    //存在之后再对比是否一致 
                    var oldCount = this.LoadAll(p => p.USER_ID == userId && dptId.Contains(p.DEPARTMENT_ID)).Select(p => p.DEPARTMENT_ID).ToList();
                    var newdptid = dptId.Split(',').OrderBy(c => c).ToList();
                    if (oldCount.Count == newdptid.Count && oldCount.All(newdptid.Contains)) return true;
                    //删除原有关系
                    this.Delete(p => p.USER_ID == userId);
                }
                if (!string.IsNullOrEmpty(dptId))
                {
                    //添加现有关系
                    var list = dptId.Split(',').Select(item => new Domain.SYS_USER_DEPARTMENT()
                    {
                        DEPARTMENT_ID = item,
                        USER_ID = userId
                    }).ToList();
                    return SaveList(list);
                }
                return true;
            }
            catch (Exception e) { throw e.InnerException; }
        }
    }
}