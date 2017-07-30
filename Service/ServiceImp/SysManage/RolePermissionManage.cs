using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Service.IService;

namespace Service.ServiceImp
{
    public class RolePermissionManage : RepositoryBase<Domain.SYS_ROLE_PERMISSION>, IService.IRolePermissionManage
    {
        IPermissionManage PermissionManage { get; set; }
        /// <summary>
        /// 保存角色权限
        /// </summary>
        public bool SetRolePermission(int roleId, string newper, string sysId)
        {
            try
            {
                //1、获取当前系统的模块ID集合
                var permissionId = this.PermissionManage.GetPermissionIdBySysId(sysId).Cast<int>().ToList();
                //2、获取角色权限，是否存在，存在即删除，只删除当前选择的系统
                if (this.IsExist(p => p.ROLEID == roleId && permissionId.Any(e => e == p.PERMISSIONID)))
                {
                    //3、删除角色权限
                    this.Delete(p => p.ROLEID == roleId && permissionId.Any(e => e == p.PERMISSIONID));
                }
                //4、添加角色权限
                if (string.IsNullOrEmpty(newper)) return true;
                //Trim 保证数据安全
                var str = newper.Trim(',').Split(',');
                foreach (var per in str.Select(t => new Domain.SYS_ROLE_PERMISSION()
                {
                    PERMISSIONID = int.Parse(t),
                    ROLEID = roleId
                }))
                {
                    this._Context.Set<SYS_ROLE_PERMISSION>().Add(per);
                }
                //5、Save
                return this._Context.SaveChanges() > 0;
            }
            catch (Exception e) { throw e.InnerException; }
        }
    }
}
