using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPage.Controllers;

namespace WebPage.Areas.SysManage.Controllers
{
    public class HomeController : BaseController
    {
        #region 声明容器
        IModuleManage ModuleManage { get; set; }
        #endregion

        // GET: SysManage/Home
        public ActionResult Index()
        {
            //获取系统模块列表（如果用bui可以写个方法输出Json给BUI）
            ViewData["Module"] = ModuleManage.GetModule(this.CurrentUser.Id, this.CurrentUser.Permissions, this.CurrentUser.System_Id);
            return View(this.CurrentUser);

            var ModuleList = ViewData["Module"] as List<Domain.SYS_MODULE>;
            
        }
    }
}