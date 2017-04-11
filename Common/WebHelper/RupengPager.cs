using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Diagnostics;

namespace Common
{
    /// <summary>
    /// 分页组件调用实例
    /// var pager = new Common.RupengPager();
    ///        pager.UrlFormat = "测试分页.aspx?pagenum={n}";//设置分页URL
    ///        pager.PageSize = 10; //设置每页显示个数
    ///        pager.TryParseCurrentPageIndex(Request["pagenum"]);//获取当前页数
    ///        int startRowIndex = (pager.CurrentPageIndex - 1) * pager.PageSize;//开始行号计算
    ///        So_KeywordLogBLL bll = new So_KeywordLogBLL();//获取分页数据
    ///        pager.TotalCount = bll.GetTotalCount();//计算总个数
    ///        Repeater1.DataSource = bll.GetPagedData(startRowIndex, startRowIndex + pager.PageSize - 1); //设置数据绑定
    ///        Repeater1.DataBind(); 
    ///       PagerHTML = pager.Render();//渲染页码条HTML
    /// </summary>
    public class RupengPager
    {
        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 每页数据条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码（从1开始）
        /// </summary>
        public int CurrentPageIndex { get; set; }

        /// <summary>
        /// 显示出来最多的页码数量，因为假设有100页，不可能把100页都显示到界面上
        /// </summary>
        public int MaxPagerCount { get; set; }

        /// <summary>
        /// 页码链接的地址格式，页码用{n}占位。
        /// </summary>
        public string UrlFormat { get; set; }
        /// <summary>
        /// 默认初始化
        /// </summary>
        public RupengPager()
        {
            PageSize = 10;
            MaxPagerCount = 10;
        }

        /// <summary>
        /// 尝试从字符串pn中解析当前页面赋值给CurrentPageIndex
        /// </summary>
        /// <param name="pn"></param>
        public void TryParseCurrentPageIndex(string pn)
        {
            int temp;
            if (int.TryParse(pn, out temp))
            {
                CurrentPageIndex = temp;
            }
            else
            {
                CurrentPageIndex = 1;
            }
        }

        /// <summary>
        /// 创建页码链接
        /// </summary>
        /// <param name="i">页码</param>
        /// <param name="text">链接文本</param>
        /// <returns></returns>
        private string GetPageLink(int i, string text)
        {
            StringBuilder sb = new StringBuilder();
            string url = UrlFormat.Replace("{n}", i.ToString());
            sb.Append("<a href='").Append(url).Append("'>").Append(text).Append("</a>");
            return sb.ToString();
        }
        public string Render()
        {


            StringBuilder sb = new StringBuilder();
            //TotalCount=35,PageSize=10,pageCount=4

            //计算总页数，如果是30条，则是3页，31条也是3页，29条也是3页，因此是
            //天花板运算Ceiling
            double tempCount = TotalCount / PageSize;
            int pageCount = (int)Math.Ceiling(tempCount);

            //计算显示的页码数（当总页码大于MaxPagerCount）的起始页码
            int visibleStart = CurrentPageIndex - MaxPagerCount / 2;
            if (visibleStart < 1)
            {
                visibleStart = 1;
            }

            //计算显示的页码数（当总页码大于MaxPagerCount）的起始页码
            int visibleEnd = visibleStart + MaxPagerCount;
            //显示最多MaxPagerCount条
            //如果算出来的结束页码大于总页码的话则调整为最大页码
            if (visibleEnd > pageCount)
            {
                visibleEnd = pageCount;
            }

            if (CurrentPageIndex > 1)
            {
                sb.Append(GetPageLink(1, "首页"));
                sb.Append(GetPageLink(CurrentPageIndex - 1, "上一页"));
            }
            else
            {
                sb.Append("<span>首页</span>");
                //如果没有上一页了，则只显示一个上一页的文字，没有超链接
                sb.Append("<span>上一页</span>");
            }

            //绘制可视的页码链接
            for (int i = visibleStart; i <= visibleEnd; i++)
            {
                //当前页不是超链接
                if (i == CurrentPageIndex)
                {
                    sb.Append("<span>").Append(i).Append("</span>");
                }
                else
                {
                    sb.Append(GetPageLink(i, i.ToString()));
                }
            }
            if (CurrentPageIndex < pageCount)
            {
                sb.Append(GetPageLink(CurrentPageIndex + 1, "下一页"));
                sb.Append(GetPageLink(pageCount + 1, "末页"));
            }
            else
            {
                sb.Append("<span>下一页</span>");
                sb.Append("<span>末页</span>");
            }
            return sb.ToString();
        }
    }
}