using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ServiceImp
{
    public class SystemManage : RepositoryBase<Domain.SYS_SYSTEM>, ISystemManage
    {
        /// <summary>
        /// 获取系统ID、NAME
        /// </summary>
        /// <param name="systems">用户拥有操作权限的系统</param>
        /// <returns></returns>
        public dynamic LoadSystemInfo(List<string> systems)
        {
            //return Common.JsonHelper.JsonConverter.JsonClass(this.LoadAll(null).Select(p => new { p.ID }).ToList());//??p.Name
            return Common.JsonHelper.JsonConverter.JsonClass(this.LoadAll(p => systems.Any(e => e == p.ID)).Select(p => new { p.ID, p.NAME }).ToList());
        }
    }
}
