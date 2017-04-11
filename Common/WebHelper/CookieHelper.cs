using System;
using System.Web;

namespace Common
{
    /// <summary>
    /// Cookie辅助类
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                cookie.Expires = DateTime.Now.Add(ts);
                HttpContext.Current.Response.AppendCookie(cookie);
                HttpContext.Current.Request.Cookies.Remove(cookiename);
            }
        }
        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                str = cookie.Value;
            }
            return str;
        }
        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="cookiename"></param>
        /// <returns></returns>
        public static HttpCookie GetCookie(string cookiename)
        {
            return HttpContext.Current.Request.Cookies[cookiename];
        }
        /// <summary>
        /// 添加一个Cookie，默认浏览器关闭过期
        /// </summary>
        public static void SetCookie(string cookiename, System.Collections.Specialized.NameValueCollection cookievalue, int? days)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookiename);
            }
            ClearCookie(cookiename);
            cookie.Values.Add(cookievalue);
            var siteurl = System.Configuration.ConfigurationManager.AppSettings["siteurl"];
            if (!string.IsNullOrEmpty(siteurl))
            {
                cookie.Domain = siteurl.Replace("www.", "");
            }
            if (days != null && days > 0) { cookie.Expires = DateTime.Now.AddDays(Convert.ToInt32(days)); }
            HttpContext.Current.Response.AppendCookie(cookie);

        }
        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 null为浏览器过期</param>
        public static void SetCookie(string cookiename, string cookievalue, int? expires)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookiename);
            }
            ClearCookie(cookiename);
            cookie = new HttpCookie(cookiename);
            cookie.Value = cookievalue;
            var siteurl = System.Configuration.ConfigurationManager.AppSettings["siteurl"];
            if (!string.IsNullOrEmpty(siteurl))
            {
                cookie.Domain = siteurl.Replace("www.", "");
            }
            if (expires != null && expires > 0) { cookie.Expires = DateTime.Now.AddDays(Convert.ToInt32(expires)); }
            HttpContext.Current.Response.AppendCookie(cookie);

        }
    }
}