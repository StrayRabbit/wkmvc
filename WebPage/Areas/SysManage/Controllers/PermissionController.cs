using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        IRolePermissionManage RolePermissionManage { get; set; }
        IUserPermissionManage UserPermissionManage { get; set; }
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

        /// <summary>
        /// 权限管理 权限列表
        /// </summary>
        /// <returns></returns>
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
                    //把模块ID转为Int
                    int module_Id = int.Parse(moduleId);
                    //模块信息
                    var module = this.ModuleManage.Get(p => p.ID == module_Id);
                    //绑定列表
                    var query = this.PermissionManage.LoadAll(p => p.MODULEID == module.ID);
                    //关键字查询
                    if (!string.IsNullOrEmpty(keywords))
                    {
                        query = query.Where(p => p.NAME.Contains(keywords));
                    }
                    //输出结果
                    var result = query.OrderBy(p => p.SHOWORDER).ToList();

                    ViewBag.Search = base.keywords;
                    ViewBag.Module = module;

                    return View(result);
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
            catch (Exception e)
            {
                json.Status = "n";
                json.Msg = "服务器忙，请稍后再试！";
                WriteLog(Common.Enums.enumOperator.Select, "权限管理，获取模块树：", e);
            }

            return Json(json);
        }

        /// <summary>
        /// 初始化权限，默认增删改查详情
        /// <param name="id">模块ID</param>
        /// </summary>
        [UserAuthorizeAttribute(ModuleAlias = "Permission", OperaAction = "Reset")]
        public ActionResult Reset(string id)
        {
            var json = new JsonHelper() { Status = "n", Msg = "初始化完毕" };
            try
            {
                //判断模块ID 是否符合规范
                if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, @"^\d+$"))
                {
                    json.Msg = "模块参数错误";
                    WriteLog(Common.Enums.enumOperator.Allocation, "初始化权限，结果：" + json.Msg, Common.Enums.enumLog4net.ERROR);
                    return Json(json);
                }
                //将 ID 转为 Int
                int newid = int.Parse(id);

                //判断权限里 模块是否有了权限
                if (this.PermissionManage.IsExist(p => p.MODULEID == newid))
                {
                    json.Msg = "该模块已存在权限，无法初始化";
                    WriteLog(Common.Enums.enumOperator.Allocation, "初始化权限，结果：" + json.Msg, Common.Enums.enumLog4net.ERROR);
                    return Json(json);
                }
                //添加默认权限 
                var per = new string[] { "查看,View", "列表,List", "详情,Detail", "添加,Add", "修改,Edit", "删除,Remove" };
                var list = new List<Domain.SYS_PERMISSION>();
                foreach (var item in per)
                {
                    list.Add(new Domain.SYS_PERMISSION()
                    {
                        CREATEDATE = DateTime.Now,
                        CREATEUSER = this.CurrentUser.Name,
                        NAME = item.Split(',')[0],
                        PERVALUE = item.Split(',')[1],
                        UPDATEDATE = DateTime.Now,
                        UPDATEUSER = this.CurrentUser.Name,
                        MODULEID = newid,
                        SHOWORDER = 0
                    });
                }
                //批量添加
                if (this.PermissionManage.SaveList(list))
                {
                    json.Status = "y";
                }
                else
                {
                    json.Msg = "初始化失败";
                }
                WriteLog(Common.Enums.enumOperator.Allocation, "初始化权限，结果：" + json.Msg, Common.Enums.enumLog4net.INFO);
            }
            catch (Exception e)
            {
                json.Msg = e.InnerException.Message;
                WriteLog(Common.Enums.enumOperator.Allocation, "对模块权限按钮的管理初始化权限：", e);
            }
            return Json(json);
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        [UserAuthorizeAttribute(ModuleAlias = "Permission", OperaAction = "Remove")]
        public ActionResult Delete(string idList)
        {
            var json = new JsonHelper() { Msg = "删除权限成功", Status = "n" };
            try
            {
                if (!string.IsNullOrEmpty(idList))
                {
                    var idList1 = idList.Trim(',').Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToList();
                    //判断查找角色是否调用
                    if (!this.RolePermissionManage.IsExist(p => idList1.Any(e => e == p.PERMISSIONID)))
                    {
                        //判断查找用户是否调用
                        if (!this.UserPermissionManage.IsExist(p => idList1.Any(e => e == p.FK_PERMISSIONID)))
                        {
                            this.PermissionManage.Delete(p => idList1.Any(e => e == p.ID));
                            json.Status = "y";
                        }
                        else
                        {
                            json.Msg = "有用户正在使用该权限，不能删除!";
                        }
                    }
                    else
                    {
                        json.Msg = "有角色正在使用该权限，不能删除!";
                    }
                }
                else
                {
                    json.Msg = "未找到要删除的权限记录";
                }
                WriteLog(Common.Enums.enumOperator.Remove, "删除权限，结果：" + json.Msg, Common.Enums.enumLog4net.WARN);
            }
            catch (Exception e)
            {
                json.Msg = e.InnerException.Message;
                WriteLog(Common.Enums.enumOperator.Remove, "对模块权限按钮的管理删除权限：", e);
            }
            return Json(json);
        }

    }
}