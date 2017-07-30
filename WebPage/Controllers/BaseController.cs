using Common;
using Common.Enums;
using log4net.Ext;
using Service;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPage.Controllers
{
    public class BaseController : Controller
    {
        #region 公用变量
        /// <summary>
        /// 查询关键词
        /// </summary>
        public string keywords { get; set; }
        /// <summary>
        /// 视图传递的分页页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 视图传递的分页条数
        /// </summary>
        public int pagesize { get; set; }
        /// <summary>
        /// 用户容器，公用
        /// </summary>
        public IUserManage UserManage = Spring.Context.Support.ContextRegistry.GetContext().GetObject("Service.User") as IUserManage;
        #endregion

        protected IExtLog _log = ExtLogManager.GetLogger("dblog");

        #region 用户对象
        /// <summary>
        /// 获取当前用户对象
        /// </summary>
        public Account CurrentUser
        {
            get
            {
                //从Session中获取用户对象
                if (SessionHelper.GetSession("CurrentUser") != null)
                {
                    return SessionHelper.GetSession("CurrentUser") as Account;
                }
                //Session过期 通过Cookies中的信息 重新获取用户对象 并存储于Session中
                var account = UserManage.GetAccountByCookie();
                SessionHelper.SetSession("CurrentUser", account);
                return account;
            }
        }
        #endregion

        /// <summary>
        /// 重写控制器方法 实现登录验证和公共变量的获取
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region 登录用户验证
            //1、判断Session对象是否存在
            if (filterContext.HttpContext.Session == null)
            {
                filterContext.HttpContext.Response.Write(
                       " <script type='text/javascript'> alert('~登录已过期，请重新登录');window.top.location='/'; </script>");
                filterContext.RequestContext.HttpContext.Response.End();
                filterContext.Result = new EmptyResult();
                return;
            }
            //2、登录验证
            if (this.CurrentUser == null)
            {
                filterContext.HttpContext.Response.Write(
                    " <script type='text/javascript'> alert('登录已过期，请重新登录'); window.top.location='/';</script>");
                filterContext.RequestContext.HttpContext.Response.End();
                filterContext.Result = new EmptyResult();
                return;
            }

            #endregion

            #region 公共Get变量
            //分页页码
            object p = filterContext.HttpContext.Request["page"];
            if (p == null || p.ToString() == "") { page = 1; } else { page = int.Parse(p.ToString()); }

            //搜索关键词
            string search = filterContext.HttpContext.Request.QueryString["Search"];
            if (!string.IsNullOrEmpty(search)) { keywords = search; }
            //显示分页条数
            string size = filterContext.HttpContext.Request.QueryString["example_length"];
            if (!string.IsNullOrEmpty(size) && System.Text.RegularExpressions.Regex.IsMatch(size.ToString(), @"^\d+$")) { pagesize = int.Parse(size.ToString()); } else { pagesize = 10; }
            #endregion
        }

        public void WriteLog(enumOperator action, string message, enumLog4net logLevel)
        {
            switch (logLevel)
            {
                case enumLog4net.INFO:
                    _log.Info(Utils.GetIP(), CurrentUser.Name, Request.Url.ToString(), action.ToString(), message);
                    return;
                case enumLog4net.WARN:
                    _log.Warn(Utils.GetIP(), CurrentUser.Name, Request.Url.ToString(), action.ToString(), message);
                    return;
                default:
                    _log.Error(Utils.GetIP(), CurrentUser.Name, Request.Url.ToString(), action.ToString(), message);
                    return;
            }
        }

        public void WriteLog(enumOperator action, string message, Exception e)
        {
            _log.Fatal(Utils.GetIP(), CurrentUser.Name, Request.Url.ToString(), action.ToString(), message + e.Message, e);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        #region 字段和属性
        /// <summary>
        /// 模块别名，可配置更改
        /// </summary>
        public string ModuleAlias { get; set; }
        /// <summary>
        /// 权限动作
        /// </summary>
        public string OperaAction { get; set; }
        /// <summary>
        /// 权限访问控制器参数
        /// </summary>
        private string Sign { get; set; }
        /// <summary>
        /// 基类实例化
        /// </summary>
        public BaseController baseController = new BaseController();

        #endregion

        /// <summary>
        /// 权限认证
        /// </summary>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //1、判断模块是否对应
            if (string.IsNullOrEmpty(ModuleAlias))
            {
                filterContext.HttpContext.Response.Write(" <script type='text/javascript'> alert('^您没有访问该页面的权限！'); </script>");
                filterContext.RequestContext.HttpContext.Response.End();
                filterContext.Result = new EmptyResult();
                return;
            }

            //2、判断用户是否存在
            if (baseController.CurrentUser == null)
            {
                filterContext.HttpContext.Response.Write(" <script type='text/javascript'> alert('^登录已过期，请重新登录！');window.top.location='/'; </script>");
                filterContext.RequestContext.HttpContext.Response.End();
                filterContext.Result = new EmptyResult();
                return;
            }

            //对比变量，用于权限认证
            var alias = ModuleAlias;

            #region 配置Sign调取控制器标识
            Sign = filterContext.RequestContext.HttpContext.Request.QueryString["sign"];
            if (!string.IsNullOrEmpty(Sign))
            {
                if (("," + ModuleAlias.ToLower()).Contains("," + Sign.ToLower()))
                {
                    alias = Sign;
                    filterContext.Controller.ViewData["Sign"] = Sign;
                }
            }
            #endregion

            //3、调用下面的方法，验证是否有访问此页面的权限，查看加操作
            var moduleId = baseController.CurrentUser.Modules.Where(p => p.ALIAS.ToLower() == alias.ToLower()).Select(p => p.ID).FirstOrDefault();
            bool _blAllowed = this.IsAllowed(baseController.CurrentUser, moduleId, OperaAction);
            if (!_blAllowed)
            {
                filterContext.HttpContext.Response.Write(" <script type='text/javascript'> alert('您没有访问当前页面的权限！');</script>");
                filterContext.RequestContext.HttpContext.Response.End();
                filterContext.Result = new EmptyResult();
                return;
            }

            //4、有权限访问页面，将此页面的权限集合传给页面
            filterContext.Controller.ViewData["PermissionList"] = GetPermissByJson(baseController.CurrentUser, moduleId);
        }

        /// <summary>
        /// 获取操作权限Json字符串，供视图JS判断使用
        /// </summary>
        string GetPermissByJson(Account account, int moduleId)
        {
            //操作权限
            var _varPerListThisModule = account.Permissions.Where(p => p.MODULEID == moduleId).Select(R => new { R.PERVALUE }).ToList();
            return Common.JsonHelper.JsonConverter.Serialize(_varPerListThisModule);
        }

        /// <summary>
        /// 功能描述：判断用户是否有此模块的操作权限
        /// </summary>
        bool IsAllowed(Account user, int moduleId, string action)
        {
            //判断入口
            if (user == null || user.Id <= 0 || moduleId == 0 || string.IsNullOrEmpty(action)) return false;
            //验证权限
            var permission = user.Permissions.Where(p => p.MODULEID == moduleId);
            action = action.Trim(',');
            if (action.IndexOf(',') > 0)
            {
                permission = permission.Where(p => action.ToLower().Contains(p.PERVALUE.ToLower()));
            }
            else
            {
                permission = permission.Where(p => p.PERVALUE.ToLower() == action.ToLower());
            }
            return permission.Any();
        }

    }

    /// <summary>
    /// 模型去重，非常重要
    /// add yuangang by 2016-05-25
    /// </summary>
    public class ModuleDistinct : IEqualityComparer<Domain.SYS_MODULE>
    {
        public bool Equals(Domain.SYS_MODULE x, Domain.SYS_MODULE y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(Domain.SYS_MODULE obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}