using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IPermissionManage : IRepository<Domain.SYS_PERMISSION>
    {
        /// <summary>
        /// 根据系统ID获取所有模块的权限ID集合
        /// </summary>
        List<int> GetPermissionIdBySysId(string sysId);
    }
}
