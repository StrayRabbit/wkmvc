using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebPages
{
    public class CommonHtmlHelper
    {
        public static MvcHtmlString RtfTextToHtml(string rtfText)
        {
            if (string.IsNullOrWhiteSpace(rtfText))
            {
                return new MvcHtmlString("");
            }
            string value = rtfText.Replace(" ", "&nbsp;").Replace("\t", string.Format("{0}{0}{0}{0}", "&nbsp;")).Replace("<", "&lt;").Replace(">", "&gt;").Replace("\n", "<br/>");
            return new MvcHtmlString(value);
        }

        public static object GetDepartmentName(string name, decimal? level)
        {
            if (level > 1m)
            {
                string str = "&nbsp;&nbsp;";
                int num = 0;
                while (num < level)
                {
                    str += "&nbsp;&nbsp;";
                    num++;
                }
                name = str + "|--" + name;
            }
            return name;
        }

        public static MvcHtmlString PaginationPager(int Page, int PageSize, int Count, int PageNum, string Url)
        {
            if (Count <= PageSize)
            {
                return new MvcHtmlString("");
            }
            if (Count == 0)
            {
                return new MvcHtmlString("");
            }
            int num;
            if (Count % PageSize == 0)
            {
                num = Count / PageSize;
            }
            else
            {
                num = Count / PageSize + 1;
            }
            if (string.IsNullOrEmpty(Url))
            {
                Url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            }
            StringBuilder stringBuilder = new StringBuilder(15000);
            int num2;
            if (Url.IndexOf("@p@") < 0)
            {
                num2 = Url.IndexOf("?");
                if (num2 > 0 && num2 < Url.Length)
                {
                    int num3 = Url.ToLower().IndexOf("page=", num2);
                    if (num3 > 0)
                    {
                        int num4 = Url.IndexOf("&", num3 + 1);
                        if (num4 > 0)
                        {
                            Url = Url.Substring(0, num3) + Url.Substring(num4);
                            Url += "&";
                        }
                        else
                        {
                            Url = Url.Substring(0, num3);
                        }
                    }
                    else
                    {
                        Url += "&";
                    }
                }
                else
                {
                    Url = "?";
                }
            }
            stringBuilder.Append("<div class=\"dataTables_paginate paging_full_numbers\" style=\"padding-top: 0px;\" id=\"example_paginate\">");
            stringBuilder.Append("<ul class=\"pagination\">");
            if (Page > 1)
            {
                stringBuilder.Append("<li class=\"paginate_button previous\" id=\"example_first\">");
                stringBuilder.Append("<a href=\"" + ((Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", "1") : (Url + "page=1")) + "\" aria-controls=\"example\" data-dt-idx=\"1\" tabindex=\"0\">首页</a></li>");
            }
            else
            {
                stringBuilder.Append("<li class=\"paginate_button previous disabled\" id=\"example_first\"><a href=\"\" aria-controls=\"example\" data-dt-idx=\"1\" tabindex=\"0\">首页</a></li></li>");
            }
            if (Page > 1)
            {
                num2 = Page - 1;
                stringBuilder.Append("<li class=\"paginate_button previous\" id=\"example_previous\">");
                stringBuilder.Append("<a href=\"" + ((Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", num2.ToString()) : (Url + "page=" + num2.ToString())) + "\" aria-controls=\"example\" data-dt-idx=\"1\" tabindex=\"0\">上一页</a></li>");
            }
            else
            {
                stringBuilder.Append("<li class=\"paginate_button previous disabled\" id=\"example_previous\">");
                stringBuilder.Append("<a href=\"\" aria-controls=\"example\" data-dt-idx=\"1\" tabindex=\"0\">上一页</a></li>");
            }
            stringBuilder.Append("");
            if (Page + PageNum / 2 <= num)
            {
                num2 = Page + PageNum / 2 - PageNum;
            }
            else
            {
                num2 = num - PageNum + 1;
            }
            if (num2 <= 0)
            {
                num2 = 1;
            }
            int num5 = 1;
            while (num5 <= PageNum && num2 <= num)
            {
                if (num2 != Page)
                {
                    stringBuilder.Append(string.Concat(new string[]
                    {
                        "<li class=\"paginate_button \"><a href=\"",
                        (Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", num2.ToString()) : (Url + "page=" + num2.ToString()),
                        "\" aria-controls=\"example\" data-dt-idx=\"3\" tabindex=\"0\">",
                        num2.ToString(),
                        "</a></li>"
                    }));
                }
                else
                {
                    stringBuilder.Append("<li class=\"paginate_button active\"><a href=\"JavaScript:void(0)\" aria-controls=\"example\" data-dt-idx=\"2\" tabindex=\"0\">" + num2.ToString() + "</a></li>");
                }
                num2++;
                num5++;
            }
            if (num > PageNum && num - Page > 4)
            {
                stringBuilder.Append("<li class=\"dot\"><span>......</span></li>");
                stringBuilder.Append("<li class=\"paginate_button\">");
                stringBuilder.Append(string.Concat(new object[]
                {
                    "<a  href=\"",
                    (Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", num.ToString()) : (Url + "page=" + num.ToString()),
                    "\" >",
                    num,
                    "</a></li>"
                }));
            }
            if (Page < num)
            {
                num2 = Page + 1;
                stringBuilder.Append("<li class=\"paginate_button next\" id=\"example_next\">");
                stringBuilder.Append("<a href=\"" + ((Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", num2.ToString()) : (Url + "page=" + num2.ToString())) + "\"  aria-controls=\"example\" data-dt-idx=\"4\" tabindex=\"0\">下一页</a></li>");
            }
            else
            {
                stringBuilder.Append("<li class=\"paginate_button next disabled\" id=\"example_next\"><a href=\"javascript:void(0)\" aria-controls=\"example\" data-dt-idx=\"4\" tabindex=\"0\">下一页</a></li>");
            }
            if (Page < num)
            {
                stringBuilder.Append("<li class=\"paginate_button next\" id=\"example_last\">");
                stringBuilder.Append("<a href=\"" + ((Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", num.ToString()) : (Url + "page=" + num.ToString())) + "\"  aria-controls=\"example\" data-dt-idx=\"5\" tabindex=\"0\">末页</a></li>");
            }
            else
            {
                stringBuilder.Append("<li class=\"paginate_button next disabled\" id=\"example_last\"><a href=\"\" aria-controls=\"example\" data-dt-idx=\"1\" tabindex=\"0\">末页</a></li></li>");
            }
            stringBuilder.Append("</ul>");
            stringBuilder.Append("</div>");
            return new MvcHtmlString(stringBuilder.ToString());
        }

        public static MvcHtmlString PaginationUlPager(int Page, int PageSize, int Count, int PageNum, string Url)
        {
            if (Count <= PageSize)
            {
                return new MvcHtmlString("");
            }
            if (Count == 0)
            {
                return new MvcHtmlString("");
            }
            int num;
            if (Count % PageSize == 0)
            {
                num = Count / PageSize;
            }
            else
            {
                num = Count / PageSize + 1;
            }
            if (string.IsNullOrEmpty(Url))
            {
                Url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            }
            StringBuilder stringBuilder = new StringBuilder(15000);
            if (Url.IndexOf("@p@") < 0)
            {
                int num2 = Url.IndexOf("?");
                if (num2 > 0 && num2 < Url.Length)
                {
                    int num3 = Url.ToLower().IndexOf("page=", num2);
                    if (num3 > 0)
                    {
                        int num4 = Url.IndexOf("&", num3 + 1);
                        if (num4 > 0)
                        {
                            Url = Url.Substring(0, num3) + Url.Substring(num4);
                            Url += "&";
                        }
                        else
                        {
                            Url = Url.Substring(0, num3);
                        }
                    }
                    else
                    {
                        Url += "&";
                    }
                }
                else
                {
                    Url = "?";
                }
            }
            stringBuilder.Append("<div class=\"btn-group pull-right\">");
            if (Page > 1)
            {
                int num2 = Page - 1;
                stringBuilder.Append("<a class=\"btn btn-white btn-sm\" href=\"" + ((Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", num2.ToString()) : (Url + "page=" + num2.ToString())) + "\" title=\"上一页\" ><i class=\"fa fa-angle-left\"></i></a>");
            }
            else
            {
                stringBuilder.Append("<a class=\"btn btn-white btn-sm\" href=\"javascript:void(0)\"  disabled=\"disabled\" title=\"上一页\" ><i class=\"fa fa-angle-left\"></i></a>");
            }
            if (Page < num)
            {
                int num2 = Page + 1;
                stringBuilder.Append("<a class=\"btn btn-white btn-sm\" href=\"" + ((Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", num2.ToString()) : (Url + "page=" + num2.ToString())) + "\" title=\"下一页\" > <i class=\"fa fa-angle-right\"></i></a>");
            }
            else
            {
                stringBuilder.Append("<a class=\"btn btn-white btn-sm\" href=\"javascript:void(0)\" disabled=\"disabled\" title=\"下一页\" > <i class=\"fa fa-angle-right\"></i></a>");
            }
            stringBuilder.Append("</div>");
            return new MvcHtmlString(stringBuilder.ToString());
        }
    }
}
