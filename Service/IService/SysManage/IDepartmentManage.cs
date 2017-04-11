using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IDepartmentManage : IRepository<Domain.SYS_DEPARTMENT>
    {
        /// <summary>
        /// 递归部门列表，返回按级别排序
        /// add yuangang by 2016-05-19
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<Domain.SYS_DEPARTMENT> RecursiveDepartment(List<Domain.SYS_DEPARTMENT> list);

        /// <summary>
        /// 根据部门ID递归部门列表，返回子部门+本部门的对象集合
        /// add yuangang by 2016-05-19
        /// </summary>
        List<Domain.SYS_DEPARTMENT> RecursiveDepartment(string parentId);
        /// <summary>
        /// 自动创建部门编号
        /// add yuangang by 2016-05-19
        /// </summary>
        string CreateCode(string parentCode);

        /// <summary>
        /// 部门是否存在下级部门
        /// add huafg by 2015-06-03
        /// </summary>
        bool DepartmentIsExists(string idlist);

        /// <summary>
        /// 根据部门ID获取部门名称，不存在返回空
        /// </summary>
        string GetDepartmentName(string id);
        /// <summary>
        /// 显示错层方法
        /// </summary>
        object GetDepartmentName(string name, decimal? level);
        /// <summary>
        /// 获取部门父级列表
        /// </summary>
        System.Collections.IList GetDepartmentByDetail();
    }
}
