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
    public class RoleController : BaseController
    {
        #region 生命容器
        IRoleManage RoleManage { get; set; }

        IUserRoleManage UserRoleManage { get; set; }

        IRolePermissionManage RolePermissionManage { get; set; }

        ISystemManage SystemManage { get; set; }
        #endregion

        /// <summary>
        /// 加载主页
        /// </summary>
        /// <returns></returns>
        [UserAuthorizeAttribute(ModuleAlias = "Role", OperaAction = "View")]
        public ActionResult Index()
        {
            try
            {
                #region 处理查询参数

                //系统ID
                string System = Request.QueryString["System"];
                ViewData["System"] = System;

                //搜索的关键字（用于输出给前台的Input显示）
                ViewBag.Search = base.keywords;
                #endregion

                //输出用户所拥有的系统列表到视图页
                ViewData["Systemlist"] = this.SystemManage.LoadSystemInfo(CurrentUser.System_Id);

                //输出分页查询列表
                return View(BindList(System));
            }
            catch (Exception e)
            {
                WriteLog(Common.Enums.enumOperator.Select, "加载角色列表：", e);
                throw e.InnerException;
            }
        }

        /// <summary>
        /// 加载详情
        /// </summary>
        /// <returns></returns>
        [UserAuthorizeAttribute(ModuleAlias = "Role", OperaAction = "Detail")]
        public ActionResult Detail(int? id)
        {
            var _entity = new Domain.SYS_ROLE() { ISCUSTOM = false };

            if (id != null && id > 0)
            {
                _entity = RoleManage.Get(p => p.ID == id);
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["systemId"]))
                {
                    _entity.FK_BELONGSYSTEM = Request.QueryString["systemId"];
                }
            }

            ViewData["Systemlist"] = this.SystemManage.LoadSystemInfo(CurrentUser.System_Id);

            return View(_entity);
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        [UserAuthorizeAttribute(ModuleAlias = "Role", OperaAction = "Add,Edit")]
        public ActionResult Save(Domain.SYS_ROLE entity)
        {
            bool isEdit = false;
            var json = new JsonHelper() { Msg = "保存成功", Status = "n" };
            try
            {
                if (entity != null)
                {
                    //判断角色名是否汉字
                    if (System.Text.RegularExpressions.Regex.IsMatch(entity.ROLENAME.Trim(), "^[\u4e00-\u9fa5]+$"))
                    {
                        if (entity.ROLENAME.Length > 36)
                        {
                            json.Msg = "角色名称最多只能能包含36个汉字";
                            return Json(json);
                        }

                        //添加
                        if (entity.ID <= 0)
                        {
                            entity.CREATEDATE = DateTime.Now;
                            entity.CREATEPERID = this.CurrentUser.Name;
                            entity.UPDATEDATE = DateTime.Now;
                            entity.UPDATEUSER = this.CurrentUser.Name;
                        }
                        else //修改
                        {
                            entity.UPDATEDATE = DateTime.Now;
                            entity.UPDATEUSER = this.CurrentUser.Name;
                            isEdit = true;
                        }
                        //判断角色是否重名 
                        if (!this.RoleManage.IsExist(p => p.ROLENAME == entity.ROLENAME && p.ID != entity.ID))
                        {
                            if (isEdit)
                            {
                                //系统更换 删除所有权限
                                var _entity = RoleManage.Get(p => p.ID == entity.ID);
                                if (_entity.FK_BELONGSYSTEM != entity.FK_BELONGSYSTEM)
                                {
                                    RolePermissionManage.Delete(p => p.ROLEID == _entity.ID);
                                }
                            }
                            if (RoleManage.SaveOrUpdate(entity, isEdit))
                            {
                                json.Status = "y";
                            }
                            else
                            {
                                json.Msg = "保存失败";
                            }
                        }
                        else
                        {
                            json.Msg = "角色名" + entity.ROLENAME + "已被使用，请修改角色名称再提交";
                        }

                    }
                    else
                    {
                        json.Msg = "角色名称只能包含汉字";
                    }

                }
                else
                {
                    json.Msg = "未找到需要保存的角色信息";
                }
                if (isEdit)
                {
                    WriteLog(Common.Enums.enumOperator.Edit, "修改用户角色，结果：" + json.Msg, Common.Enums.enumLog4net.INFO);
                }
                else
                {
                    WriteLog(Common.Enums.enumOperator.Add, "添加用户角色，结果：" + json.Msg, Common.Enums.enumLog4net.INFO);
                }
            }
            catch (Exception e)
            {
                json.Msg = "保存用户角色发生内部错误！";
                WriteLog(Common.Enums.enumOperator.None, "保存用户角色：", e);
            }
            return Json(json);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        [UserAuthorizeAttribute(ModuleAlias = "Role", OperaAction = "Remove")]
        public ActionResult Delete(string idList)
        {
            var json = new JsonHelper() { Msg = "删除角色完毕", Status = "n" };
            var id = idList.Trim(',').Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToList();
            if (id.Contains(Common.Enums.ClsDic.DicRole["超级管理员"]))
            {
                json.Msg = "删除失败，不能删除系统固有角色!";
                WriteLog(Common.Enums.enumOperator.Remove, "删除用户角色：" + json.Msg, Common.Enums.enumLog4net.ERROR);
                return Json(json);
            }
            if (this.UserRoleManage.IsExist(p => id.Contains(p.FK_ROLEID)))
            {
                json.Msg = "删除失败，不能删除系统中正在使用的角色!";
                WriteLog(Common.Enums.enumOperator.Remove, "删除用户角色：" + json.Msg, Common.Enums.enumLog4net.ERROR);
                return Json(json);
            }
            try
            {
                //1、删除角色权限
                RolePermissionManage.Delete(p => id.Contains(p.ROLEID));
                //2、删除角色
                RoleManage.Delete(p => id.Contains(p.ID));
                json.Status = "y";
                WriteLog(Common.Enums.enumOperator.Remove, "删除用户角色：" + json.Msg, Common.Enums.enumLog4net.WARN);
            }
            catch (Exception e)
            {
                json.Msg = "删除用户角色发生内部错误！";
                WriteLog(Common.Enums.enumOperator.Remove, "删除用户角色：", e);
            }
            return Json(json);
        }

        /// <summary>
        /// 用户角色分配
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [UserAuthorizeAttribute(ModuleAlias = "User", OperaAction = "AllocationRole")]
        public ActionResult RoleCall(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    //用户ID
                    ViewData["userId"] = id;
                    //获取用户角色信息
                    var userRoleList = this.UserRoleManage.LoadAll(p => p.FK_USERID == id).Select(p => p.FK_ROLEID).ToList();
                    return View(JsonConverter.JsonClass(this.RoleManage.LoadAll(p => this.CurrentUser.System_Id.Any(e => e == p.FK_BELONGSYSTEM)).OrderBy(p => p.FK_BELONGSYSTEM).OrderByDescending(p => p.ID).ToList().Select(p => new { p.ID, p.ROLENAME, ISCUSTOMSTATUS = p.ISCUSTOM == true ? "<i class=\"fa fa-circle text-navy\"></i>" : "<i class=\"fa fa-circle text-danger\"></i>", SYSNAME = SystemManage.Get(m => m.ID == p.FK_BELONGSYSTEM).NAME, IsChoosed = userRoleList.Contains(p.ID) })));
                }
                else
                {
                    return View();
                }

            }
            catch (Exception e)
            {
                WriteLog(Common.Enums.enumOperator.Select, "获取用户分配的角色：", e);
                throw e.InnerException;
            }
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        [UserAuthorizeAttribute(ModuleAlias = "Role", OperaAction = "Allocation")]
        public ActionResult UserRole()
        {
            var json = new JsonHelper()
            {
                Msg = "设置用户角色成功",
                Status = "n"
            };
            string userId = Request.Form["UserId"];
            string roleId = Request.Form["checkbox_name"] ?? "";
            if (string.IsNullOrEmpty(userId))
            {
                json.Msg = "未找到要分配角色用户";
                return Json(json);
            }
            roleId = roleId.TrimEnd(',');

            try
            {
                //设置用户角色
                this.UserRoleManage.SetUserRole(int.Parse(userId), roleId);
                json.Status = "y";
                WriteLog(Common.Enums.enumOperator.Allocation, "设置用户角色：" + json.Msg, Common.Enums.enumLog4net.INFO);
            }
            catch (Exception e)
            {
                json.Msg = "设置失败，错误：" + e.Message;
                WriteLog(Common.Enums.enumOperator.Allocation, "设置用户角色：", e);
            }
            return Json(json);
        }

        /// <summary>
        /// 分页查询角色列表
        /// </summary>
        private Common.PageInfo BindList(string system)
        {
            //基础数据
            var query = this.RoleManage.LoadAll(null);
            //系统
            if (!string.IsNullOrEmpty(system))
            {
                int SuperAdminId = Common.Enums.ClsDic.DicRole["超级管理员"];
                query = query.Where(p => p.FK_BELONGSYSTEM == system || p.ISCUSTOM == true);
            }
            else
            {
                query = query.Where(p => this.CurrentUser.System_Id.Any(e => e == p.FK_BELONGSYSTEM));
            }
            //查询关键字
            if (!string.IsNullOrEmpty(keywords))
            {
                query = query.Where(p => p.ROLENAME.Contains(keywords));
            }
            //排序
            query = query.OrderByDescending(p => p.CREATEDATE);
            //分页
            var result = this.RoleManage.Query(query, page, pagesize);
            var list = result.List.Select(p => new
            {
                //以下是视图需要展示的内容，加动态可循环
                p.CREATEDATE,
                p.ROLENAME,
                p.ROLEDESC,
                USERNAME = p.CREATEPERID,
                p.ID,
                SYSNAME = SystemManage.Get(m => m.ID == p.FK_BELONGSYSTEM).NAME,
                ISCUSTOMSTATUS = p.ISCUSTOM ? "<i class=\"fa fa-circle text-navy\"></i>" : "<i class=\"fa fa-circle text-danger\"></i>"
            }).ToList();

            return new Common.PageInfo(result.Index, result.PageSize, result.Count, Common.JsonConverter.JsonClass(list));
        }
    }
}