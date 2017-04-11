using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common
{
    public interface ICache
    {
        /// <summary>
        /// 获取全局应用缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetApplicationCache(string key);
        /// <summary>
        /// 设置全局应用缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        void SetApplicationCache(string key, object obj);
        /// <summary>
        /// 删除全局应用缓存
        /// </summary>
        /// <param name="key"></param>
        void RemoveApplicationCache(string key);
    }
    /// <summary>
    /// 全局应用缓存
    /// </summary>
    public class ApplicationCache : ICache
    {
        #region ICache 成员

        public object GetApplicationCache(string key)
        {
            return HttpContext.Current.Application[key];
        }

        public void SetApplicationCache(string key, object obj)
        {
            HttpContext.Current.Application.Add(key, obj);
        }

        public void RemoveApplicationCache(string key)
        {
            HttpContext.Current.Application.Remove(key);
        }
        #endregion
    }
}