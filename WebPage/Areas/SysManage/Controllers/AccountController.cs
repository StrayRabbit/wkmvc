using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPage.Areas.SysManage.Controllers
{
    public class AccountController : Controller
    {
        #region 声明容器
        Service.IService.IUserManage UserManage { get; set; }

        log4net.Ext.IExtLog log = log4net.Ext.ExtLogManager.GetLogger("dblog");
        #endregion

        #region 基本视图
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult Login(Domain.SYS_USER item)
        {
            var json = new JsonHelper() { Msg = "登录成功", Status = "n" };
            try
            {
                //获取表单验证码
                var code = Request.Form["code"];
                //if (Session["gif"] != null)
                if (true)
                {
                    //判断用户输入的验证码是否正确
                    //if (!string.IsNullOrEmpty(code) && code.ToLower() == Session["gif"].ToString().ToLower())
                    if (true)
                    {
                        //调用登录验证接口 返回用户实体类
                        var users = UserManage.UserLogin(item.ACCOUNT.Trim(), item.PASSWORD.Trim());
                        if (users != null)
                        {
                            //是否锁定
                            if (users.ISCANLOGIN)
                            {
                                json.Msg = "用户已锁定，禁止登录，请联系管理员进行解锁";
                                log.Warn(Utils.GetIP(), item.ACCOUNT, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                                return Json(json);
                            }

                            var account = this.UserManage.GetAccountByUser(users);

                            //写入Session当前登录用户
                            SessionHelper.SetSession("CurrentUser", account);

                            //记录用户信息到Cookies
                            string cookievalue = @"{id:" + account.Id + ",username:" + account.LogName + ",password:" + account.PassWord + ",ToKen:" + Session.SessionID + "}";
                            CookieHelper.SetCookie("cookie_remeberme", new Common.CryptHelper.AESCrypt().Encrypt(cookievalue), null);

                            //更新用户本次登录IP
                            //users.LastLoginIP = Utils.GetIP();
                            //UserManage.Update(users);

                            json.Status = "y";
                            json.ReUrl = "/Sys/Home/Index";
                            log.Info(Utils.GetIP(), item.ACCOUNT, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                        }
                        else
                        {
                            json.Msg = "用户名或密码不正确";
                            log.Error(Utils.GetIP(), item.ACCOUNT, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                        }
                    }
                    else
                    {
                        json.Msg = "验证码不正确";
                        log.Error(Utils.GetIP(), item.ACCOUNT, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                    }
                }
                else
                {
                    json.Msg = "验证码已过期，请刷新验证码";
                    log.Error(Utils.GetIP(), item.ACCOUNT, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                }

            }
            catch (Exception ex)
            {
                json.Msg = ex.Message;
                log.Error(Utils.GetIP(), item.ACCOUNT, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 帮助方法
        public FileContentResult ValidateCode()
        {
            string code = string.Empty;
            System.IO.MemoryStream ms = new Models.verify_code().Create(out code);
            Session["gif"] = code;      //验证码存储在Session中，供验证
            Response.ClearContent();        //清空输出流
            return File(ms.ToArray(), @"image/png");
        }
        #endregion
    }
}