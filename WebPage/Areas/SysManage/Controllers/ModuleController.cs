using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPage.Controllers;
using Common.Enums;

namespace WebPage.Areas.SysManage.Controllers
{
    public class ModuleController : BaseController
    {
        #region 声明容器
        /// <summary>
        /// 模块管理
        /// </summary>
        IModuleManage ModuleManage { get; set; }

        /// <summary>
        /// 权限管理
        /// </summary>
        IPermissionManage PermissionManage { get; set; }

        /// <summary>
        /// 系统管理
        /// </summary>
        ISystemManage SystemManage { get; set; }
        #endregion


        [UserAuthorizeAttribute(ModuleAlias = "Module", OperaAction = "View")] /* 验证用户对Module模块是否有View（查看） 权限 */
        public ActionResult Index()
        {
            try
            {
                #region 处理查询参数
                string systems = Request.QueryString["System"];
                ViewBag.Search = base.keywords;
                ViewData["System"] = systems;
                ViewData["Systemlist"] = this.SystemManage.LoadSystemInfo(CurrentUser.System_Id);
                #endregion

                return View(BindList(systems));
            }
            catch (Exception e)
            {
                WriteLog(Common.Enums.enumOperator.Select, "模块管理加载主页", e);
                throw e.InnerException;
            }

            return View();
        }

        private object BindList(string systems)
        {
            //预加载所有模块（二级缓存）
            var query = this.ModuleManage.LoadAll(null);

            //系统ID
            if (!string.IsNullOrEmpty(systems))
            {
                query = query.Where(p => p.FK_BELONGSYSTEM == systems);
            }
            else
            {
                //选择全部 查询所有用户所属系统的模块
                query = query.Where(p => this.CurrentUser.System_Id.Any(e => e == p.FK_BELONGSYSTEM));
            }
            //递归排序（无分页）
            var entity = this.ModuleManage.RecursiveModule(query.ToList()).Select(
                p => new
                {
                    p.ID,
                    MODULENAME = GetModuleName(p.NAME, p.LEVELS),
                    p.ALIAS,
                    p.MODULEPATH,
                    p.SHOWORDER,
                    p.ICON,
                    MODULETYPE = ((Common.Enums.enumModuleType)p.MODULETYPE).ToString(),
                    ISSHOW = p.ISSHOW ? "显示" : "隐藏",
                    p.NAME,
                    SYSNAME = p.SYS_SYSTEM.NAME,
                    p.FK_BELONGSYSTEM
                });
            //查询关键字
            if (!string.IsNullOrEmpty(base.keywords))
            {
                entity = entity.Where(p => p.NAME.Contains(keywords));
            }
            return Common.JsonConverter.JsonClass(entity);
        }

        /// <summary>
        /// 显示错层方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private object GetModuleName(string name, decimal? level)
        {
            if (level > 0)
            {
                string nbsp = "&nbsp;&nbsp;";
                for (int i = 0; i < level; i++)
                {
                    nbsp += "&nbsp;&nbsp;";
                }
                name = nbsp + "  |--" + name;
            }

            return name;
        }

        [UserAuthorizeAttribute(ModuleAlias = "Module", OperaAction = "Detail")]
        public ActionResult Detail(int? id)
        {
            try
            {
                var _entity = new Domain.SYS_MODULE() { ISSHOW = true, MODULEPATH = "javascript:void(0)", MODULETYPE = 1 };
                //父模块
                string parentId = Request.QueryString["parentId"];
                if (!string.IsNullOrEmpty(parentId))
                {
                    _entity.PARENTID = int.Parse(parentId);
                }
                else
                {
                    _entity.PARENTID = 0;
                }
                //所属系统
                string sys = Request.QueryString["sys"];
                if (!string.IsNullOrEmpty(sys))
                {
                    _entity.FK_BELONGSYSTEM = sys;
                }
                //详情
                if (id != null && id > 0)
                {
                    _entity = ModuleManage.Get(p => p.ID == id);
                }
                //页面类型 我们从枚举中读取
                ViewData["ModuleType"] = Enum.GetNames(typeof(enumModuleType));
                //加载用户可操作的系统
                ViewData["Systemlist"] = this.SystemManage.LoadSystemInfo(this.CurrentUser.System_Id);
                ViewData["Modules"] = BindList(_entity.FK_BELONGSYSTEM);
                return View(_entity);
            }
            catch (Exception e)
            {
                WriteLog(Common.Enums.enumOperator.Select, "模块管理加载详情", e);
                throw e.InnerException;
            }

            return View();
        }
    }
}