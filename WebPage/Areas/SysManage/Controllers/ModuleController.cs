using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPage.Controllers;
using Common.Enums;
using Domain;
using Common;
using System.Text.RegularExpressions;

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


        #region Index   显示模块列表
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
        #endregion

        #region Detail  模块管理加载详情
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
        #endregion

        #region Save    保存模块
        [UserAuthorizeAttribute(ModuleAlias = "Module", OperaAction = "Add,Edit")]
        public ActionResult Save(SYS_MODULE entity)
        {
            bool isEdit = false;
            var json = new JsonHelper { Status = "n", Msg = "保存成功!" };

            try
            {
                if (entity != null)

                {
                    //验证
                    if (!Regex.IsMatch(entity.ALIAS, @"^[A-Za-z0-9]{1,20}$"))
                    {
                        json.Msg = "模块别名只能以字母数字组成，长度不能超过20个字符";
                        return Json(json);
                    }

                    //级别加1，一级模块默认0
                    if (entity.PARENTID <= 0)
                    {
                        entity.LEVELS = 0;
                    }
                    else
                    {
                        entity.LEVELS = ModuleManage.Get(p => p.ID == entity.PARENTID).LEVELS + 1;
                    }

                    //添加
                    if (entity.ID <= 0)
                    {
                        entity.CREATEDATE = DateTime.Now;
                        entity.CREATEUSER = this.CurrentUser.Name;
                        entity.UPDATEDATE = DateTime.Now;
                        entity.UPDATEUSER = this.CurrentUser.Name;
                    }
                    //修改
                    else
                    {
                        entity.UPDATEDATE = DateTime.Now;
                        entity.UPDATEUSER = this.CurrentUser.Name;
                        isEdit = true;
                    }

                    //模块别名同系统下不能重名
                    if (
                        ModuleManage.IsExist(
                            p =>
                                p.FK_BELONGSYSTEM == entity.FK_BELONGSYSTEM &&
                                p.ALIAS.ToLower() == entity.ALIAS.ToLower() && p.ID != entity.ID))
                    {
                        json.Msg = "同系统下模块别名不能重复";
                        return Json(json);
                    }

                    //判断同一个模块下是否重名
                    if (
                        this.ModuleManage.IsExist(
                            p =>
                                p.PARENTID == entity.PARENTID && p.FK_BELONGSYSTEM == entity.FK_BELONGSYSTEM &&
                                p.NAME == entity.NAME && p.ID != entity.ID))
                    {
                        json.Msg = "模块" + entity.NAME + "已存在，不能重复添加";
                        return Json(json);
                    }

                    if (this.ModuleManage.SaveOrUpdate(entity, isEdit))
                    {
                        json.Status = "y";
                    }
                    else
                    {
                        json.Msg = "保存失败!";
                    }

                    //变更下级模块的级别
                    if (isEdit)
                    {
                        this.ModuleManage.MoreModifyModule(entity.ID, Convert.ToInt32(entity.LEVELS));
                    }
                }
                else
                {
                    json.Msg = "未找到需要保存的模块!";
                }
            }
            catch (Exception ex)
            {
                json.Msg = "保存模块发生内部错误!";
                WriteLog(Common.Enums.enumOperator.None, "保存模块", ex);
            }

            return Json(json);
        }
        #endregion

        #region Delete  删除模块

        [UserAuthorizeAttribute(ModuleAlias = "Module", OperaAction = "Remove")]
        public ActionResult Delete(string idList)
        {
            var json = new JsonHelper { Msg = "删除模块成功", Status = "n" };

            try
            {
                if (!string.IsNullOrEmpty(idList))
                {
                    var id_List =
                        idList.Trim(',')
                            .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(p => Convert.ToInt32(p))
                            .ToList();

                    if (!this.PermissionManage.IsExist(p => id_List.Any(e => e == p.MODULEID)))
                    {
                        //存在子模块与否
                        if (!this.ModuleManage.IsExist(p => id_List.Any(e => e == p.PARENTID)))
                        {
                            this.ModuleManage.Delete(p => id_List.Any(e => e == p.ID));
                            json.Status = "y";
                        }
                        else
                        {
                            json.Msg = "该模块下有子模块，不能删除";
                        }
                    }
                    else
                    {
                        json.Msg = "该模块下有权限节点，不能删除";
                    }
                }
                else
                {
                    json.Msg = "未找到要删除的模块";
                }
                WriteLog(Common.Enums.enumOperator.Remove, "删除模块，结果：" + json.Msg, Common.Enums.enumLog4net.WARN);
            }
            catch (Exception e)
            {
                json.Msg = "删除模块发生内部错误！";
                WriteLog(Common.Enums.enumOperator.Remove, "删除模块：", e);
            }

            return Json(json);
        }
        #endregion

        #region 私有函数
        #region 加载所有模块
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
        #endregion

        #region 显示错层方法
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
        #endregion
        #endregion
    }
}