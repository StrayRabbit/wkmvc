using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace log4net.Ext
{
    public interface IExtLog : ILog
    {
        void Info(string clientIP, string clientUser, string requestUri, string action, object message);
        void Info(string clientIP, string clientUser, string requestUri, string action, object message, Exception t);

        void Warn(string clientIP, string clientUser, string requestUri, string action, object message);
        void Warn(string clientIP, string clientUser, string requestUri, string action, object message, Exception t);

        void Error(string clientIP, string clientUser, string requestUri, string action, object message);
        void Error(string clientIP, string clientUser, string requestUri, string action, object message, Exception t);

        void Fatal(string clientIP, string clientUser, string requestUri, string action, object message);
        void Fatal(string clientIP, string clientUser, string requestUri, string action, object message, Exception t);
    }
}