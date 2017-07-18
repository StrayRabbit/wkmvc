using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Service.IService;
using WebPage.Controllers;

namespace WebPage.Areas.SysManage.Controllers
{
    public class PermissionController : BaseController
    {

        #region 声明容器
        /// <summary>
        /// 系统管理
        /// </summary>
        ISystemManage SystemManage { get; set; }
        /// <summary>
        /// 权限管理
        /// </summary>
        IPermissionManage PermissionManage { get; set; }
        /// <summary>
        /// 模块管理
        /// </summary>
        IModuleManage ModuleManage { get; set; }
        #endregion

        [UserAuthorizeAttribute(ModuleAlias = "Permission", OperaAction = "View")]
        public ActionResult Home()
        {
            try
            {
                ViewData["Systemlist"] = this.SystemManage.LoadSystemInfo(CurrentUser.System_Id);
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        [UserAuthorizeAttribute(ModuleAlias = "Permission", OperaAction = "View")]
        public ActionResult Index()
        {
            try
            {
                //获取模块ID
                var moduleId = Request.QueryString["moduleId"] ?? (Request["moduleId"] ?? "");

                //如果模块ID不为空或NULL
                if (!string.IsNullOrEmpty(moduleId))
                {
                    
                }
                    return View();
            }
            catch (Exception e)
            {
                WriteLog(Common.Enums.enumOperator.Select, "对模块权限按钮的管理加载主页：", e);
                throw e.InnerException;
            }
        }

        public ActionResult GetTree()
        {
            var json = new JsonHelper() { Msg = "Success", Status = "y" };

            //获取系统ID
            var sysId = Request.Form["sysId"];

            //判断系统ID是否传入
            if (string.IsNullOrEmpty(sysId))
            {
                json.Status = "n";
                json.Msg = "获取模块失败！";
                return Json(json);
            }

            try
            {
                //获取系统下的模块列表 按照 SHOWORDER字段 升序排列
                var query =
                    this.ModuleManage.LoadAll(p => p.FK_BELONGSYSTEM == sysId).OrderBy(p => p.SHOWORDER).ToList();

                //这里就是按照jsTree的格式 输出一下 模块信息
                var result = query.Select(m => new
                {
                    id = m.ID,
                    parent = m.PARENTID > 0 ? m.PARENTID.ToString() : "#",
                    text = m.NAME,
                    icon = m.LEVELS == 0 ? "fa fa-circle text-danger" : "fa fa-circle text-navy"
                }).ToList();

                json.Data = result;
            }
            catch (Exception)
            {
                json.Status = "n";
                json.Msg = "服务器忙，请稍后再试！";
                WriteLog(Common.Enums.enumOperator.Select, "权限管理，获取模块树：", e);
            }

            return Json(json);
        }
    }
}