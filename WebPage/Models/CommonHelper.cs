using Common;
using Common.Enums;
using Domain;
using Service.IService;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
namespace Models
{
    public class CommonHelper
    {
        //public ICodeAreaManage CodeAreaManage = ContextRegistry.GetContext().GetObject("Service.CodeArea") as ICodeAreaManage;
        public ICodeManage CodeManage = ContextRegistry.GetContext().GetObject("Service.Code") as ICodeManage;
        public IUserManage UserManage = ContextRegistry.GetContext().GetObject("Service.User") as IUserManage;
        //public IProjectFilesManage ProjectFilesManage = ContextRegistry.GetContext().GetObject("Service.Pro.ProjectFilesManage") as IProjectFilesManage;
        //public IProjectStageManage ProjectStageManage = ContextRegistry.GetContext().GetObject("Service.Pro.ProjectStageManage") as IProjectStageManage;
        //public IContentManage ContentManage = ContextRegistry.GetContext().GetObject("Service.Com.Content") as IContentManage;
        public MvcHtmlString GetModuleMenu(SYS_MODULE module, List<SYS_MODULE> moduleList)
        {
            StringBuilder stringBuilder = new StringBuilder(15000);
            List<SYS_MODULE> list = (
                from p in moduleList.FindAll((SYS_MODULE p) => p.PARENTID == module.ID && p.LEVELS == 1)
                orderby p.SHOWORDER
                select p).ToList<SYS_MODULE>();
            if (list != null && list.Count > 0)
            {
                foreach (SYS_MODULE current in list)
                {
                    stringBuilder.Append("<li data-id=\"" + module.ALIAS + "\" class=\"FirstModule\" >");
                    stringBuilder.Append(string.Concat(new string[]
                    {
                        "<a class=\"",
                        this.ChildModuleList(current, moduleList, stringBuilder, false) ? "" : "J_menuItem",
                        "\" href=\"",
                        string.IsNullOrEmpty(current.MODULEPATH) ? "javascript:void(0)" : current.MODULEPATH,
                        "\" ><i class=\"",
                        current.ICON,
                        "\"></i> <span class=\"nav-label\">",
                        current.NAME,
                        "</span>",
                        this.ChildModuleList(current, moduleList, stringBuilder, false) ? "<span class=\"fa arrow\"></span>" : "",
                        "</a>"
                    }));
                    this.ChildModuleList(current, moduleList, stringBuilder, true);
                    stringBuilder.Append("</li>");
                }
            }
            return new MvcHtmlString(stringBuilder.ToString());
        }
        private bool ChildModuleList(SYS_MODULE module, List<SYS_MODULE> moduleList, StringBuilder str, bool IsAppend)
        {
            List<SYS_MODULE> list = (
                from p in moduleList.FindAll((SYS_MODULE p) => p.PARENTID == module.ID)
                orderby p.SHOWORDER
                select p).ToList<SYS_MODULE>();
            if (list != null && list.Count > 0)
            {
                if (IsAppend)
                {
                    str.Append("<ul class=\"nav nav-second-level\">");
                    foreach (SYS_MODULE current in list)
                    {
                        str.Append("<li>");
                        str.Append(string.Concat(new string[]
                        {
                            "<a class=\"",
                            this.ChildModuleList(current, moduleList, str, false) ? "" : "J_menuItem",
                            "\" href=\"",
                            string.IsNullOrEmpty(current.MODULEPATH) ? "javascript:void(0)" : current.MODULEPATH,
                            "\" ><i class=\"",
                            current.ICON,
                            "\"></i> <span class=\"nav-label\">",
                            current.NAME,
                            "</span>",
                            this.ChildModuleList(current, moduleList, str, false) ? "<span class=\"fa arrow\"></span>" : "",
                            "</a>"
                        }));
                        this.ChildModuleList(current, moduleList, str, true);
                        str.Append("</li>");
                    }
                    str.Append("</ul>");
                }
                return true;
            }
            return false;
        }
        public string GetUserZW(string levels)
        {
            return this.CodeManage.Get((SYS_CODE m) => m.CODEVALUE == levels && m.CODETYPE == "ZW").NAMETEXT;
        }
        public string GetCodeAreaName(string codearealist)
        {
            //if (string.IsNullOrEmpty(codearealist))
            //{
            //    return string.Empty;
            //}
            //List<string> list = (
            //    from p in codearealist.Trim(new char[]
            //    {
            //        ','
            //    }).Split(new string[]
            //    {
            //        ","
            //    }, StringSplitOptions.RemoveEmptyEntries)
            //    select p).ToList<string>();
            //if (list != null && list.Count > 0)
            //{
            //    string text = string.Empty;
            //    using (List<string>.Enumerator enumerator = list.GetEnumerator())
            //    {
            //        while (enumerator.MoveNext())
            //        {
            //            string item = enumerator.Current;
            //            text = text + this.CodeAreaManage.Get((SYS_CODE_AREA p) => p.ID == item).NAME + "&nbsp;";
            //        }
            //    }
            //    return text;
            //}
            return string.Empty;
        }
        public static string PrettyTime(DateTime dt)
        {
            TimeSpan timeSpan = DateTime.Now - dt;
            if (timeSpan.TotalSeconds < 60.0)
            {
                return "刚刚";
            }
            if (timeSpan.TotalMinutes < 60.0)
            {
                return (int)Math.Round(timeSpan.TotalMinutes) + "分钟前";
            }
            if (timeSpan.TotalHours < 24.0)
            {
                return (int)Math.Round(timeSpan.TotalHours) + "小时前";
            }
            if (timeSpan.TotalDays < 60.0)
            {
                return (int)Math.Round(timeSpan.TotalDays) + "天前";
            }
            return "N久以前";
        }
        public string GetSurplusTime(DateTime dt)
        {
            if (dt >= DateTime.Now)
            {
                TimeSpan timeSpan = dt - DateTime.Now;
                if (timeSpan.TotalSeconds < 60.0)
                {
                    return "<small class=\"label label-danger\"><i class=\"fa fa-clock-o\"></i> 即将超时</small>";
                }
                if (timeSpan.TotalMinutes < 60.0)
                {
                    return "<small class=\"label label-warning\"><i class=\"fa fa-clock-o\"></i> " + (int)Math.Round(timeSpan.TotalMinutes) + " 分钟</small>";
                }
                if (timeSpan.TotalHours < 24.0)
                {
                    return "<small class=\"label label-info\"><i class=\"fa fa-clock-o\"></i> " + (int)Math.Round(timeSpan.TotalHours) + " 小时</small>";
                }
                return "<small class=\"label label-primary\"><i class=\"fa fa-clock-o\"></i> " + (int)Math.Round(timeSpan.TotalDays) + " 天</small>";
            }
            else
            {
                TimeSpan timeSpan2 = DateTime.Now - dt;
                if (timeSpan2.TotalSeconds < 60.0)
                {
                    return "<small class=\"label label-danger\"><i class=\"fa fa-clock-o\"></i> 已超时 " + (int)Math.Round(timeSpan2.TotalSeconds) + " 秒</small>";
                }
                if (timeSpan2.TotalMinutes < 60.0)
                {
                    return "<small class=\"label label-danger\"><i class=\"fa fa-clock-o\"></i> 已超时 " + (int)Math.Round(timeSpan2.TotalMinutes) + " 分钟</small>";
                }
                if (timeSpan2.TotalHours < 24.0)
                {
                    return "<small class=\"label label-danger\"><i class=\"fa fa-clock-o\"></i> 已超时 " + (int)Math.Round(timeSpan2.TotalHours) + " 小时</small>";
                }
                return "<small class=\"label label-danger\"><i class=\"fa fa-clock-o\"></i> 已超时 " + (int)Math.Round(timeSpan2.TotalDays) + " 天</small>";
            }
        }
        public string GetSurplusTimeNoClass(DateTime dt)
        {
            if (dt >= DateTime.Now)
            {
                TimeSpan timeSpan = dt - DateTime.Now;
                if (timeSpan.TotalSeconds < 60.0)
                {
                    return "<span class=\"text-danger\"><i class=\"fa fa-clock-o\"></i> 即将超时</span>";
                }
                if (timeSpan.TotalMinutes < 60.0)
                {
                    return "<span class=\"text-warning\"><i class=\"fa fa-clock-o\"></i> " + (int)Math.Round(timeSpan.TotalMinutes) + " 分钟</span>";
                }
                if (timeSpan.TotalHours < 24.0)
                {
                    return "<span class=\"text-info\"><i class=\"fa fa-clock-o\"></i> " + (int)Math.Round(timeSpan.TotalHours) + " 小时</span>";
                }
                return "<span><i class=\"fa fa-clock-o\"></i> " + (int)Math.Round(timeSpan.TotalDays) + " 天</span>";
            }
            else
            {
                TimeSpan timeSpan2 = DateTime.Now - dt;
                if (timeSpan2.TotalSeconds < 60.0)
                {
                    return "<span class=\"text-danger\"><i class=\"fa fa-clock-o\"></i> 已超时 " + (int)Math.Round(timeSpan2.TotalSeconds) + " 秒</span>";
                }
                if (timeSpan2.TotalMinutes < 60.0)
                {
                    return "<span class=\"text-danger\"><i class=\"fa fa-clock-o\"></i> 已超时 " + (int)Math.Round(timeSpan2.TotalMinutes) + " 分钟</span>";
                }
                if (timeSpan2.TotalHours < 24.0)
                {
                    return "<span class=\"text-danger\"><i class=\"fa fa-clock-o\"></i> 已超时 " + (int)Math.Round(timeSpan2.TotalHours) + " 小时</span>";
                }
                return "<span class=\"text-danger\"><i class=\"fa fa-clock-o\"></i> 已超时 " + (int)Math.Round(timeSpan2.TotalDays) + " 天</span>";
            }
        }
        public string GetStageTeams(PRO_PROJECT_STAGES Stages)
        {
            string text = string.Empty;
            if (Stages != null && Stages.PRO_PROJECT_TEAMS != null && Stages.PRO_PROJECT_TEAMS.Count > 0)
            {
                using (IEnumerator<PRO_PROJECT_TEAMS> enumerator = Stages.PRO_PROJECT_TEAMS.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        PRO_PROJECT_TEAMS team = enumerator.Current;
                        if (team.JionStatus == ClsDic.DicStatus["通过"])
                        {
                            text = text + "&nbsp;" + (string.IsNullOrEmpty(this.UserManage.Get((SYS_USER p) => p.ID == team.FK_UserId).FACE_IMG) ? string.Concat(new string[]
                            {
                                "<img alt=\"image\" class=\"img-circle\" src=\"/Pro/Project/User_Default_Avatat?name=",
                                this.UserManage.Get((SYS_USER p) => p.ID == team.FK_UserId).NAME.Substring(0, 1).ToUpper(),
                                "\" data-toggle=\"tooltip\" data-placement=\"top\"  data-original-title=\"",
                                this.UserManage.Get((SYS_USER p) => p.ID == team.FK_UserId).NAME,
                                "\" />"
                            }) : string.Concat(new string[]
                            {
                                "<img alt=\"image\" class=\"img-circle\" src=\"",
                                this.UserManage.Get((SYS_USER p) => p.ID == team.FK_UserId).FACE_IMG,
                                "\" data-toggle=\"tooltip\" data-placement=\"top\"  data-original-title=\"",
                                this.UserManage.Get((SYS_USER p) => p.ID == team.FK_UserId).NAME,
                                "\" />"
                            }));
                        }
                    }
                }
            }
            return text;
        }
        public SYS_USER GetUserById(int id)
        {
            return this.UserManage.Get((SYS_USER p) => p.ID == id);
        }
        public string GetUserNameByAccount(string account)
        {
            if (this.UserManage.Get((SYS_USER p) => p.ACCOUNT == account) != null)
            {
                return this.UserManage.Get((SYS_USER p) => p.ACCOUNT == account).NAME;
            }
            return "";
        }
        public SYS_USER GetUserByAccount(string account)
        {
            return this.UserManage.Get((SYS_USER p) => p.ACCOUNT == account);
        }
        //public List<PRO_PROJECT_FILES> GetStageFiles(int stageId, int userId)
        //{
        //    return (
        //        from p in this.ProjectFilesManage.LoadAll((PRO_PROJECT_FILES p) => p.DocStyle == "projectstage" && p.Fk_ForeignId == stageId && p.FK_UserId == userId)
        //        orderby p.UploadDate descending
        //        select p).ToList<PRO_PROJECT_FILES>();
        //}
        public string RankFoums(int rank)
        {
            switch (rank)
            {
                case 1:
                    return "<span class=\"text-danger\" style=\"padding:5px 10px;font-size:15px;\"><img src=\"/Content/images/expression/e_59.png\" />&nbsp;神功绝世</span>";
                case 2:
                    return "<span class=\"text-warning\" style=\"padding:5px 10px;font-size:15px;\"><img src=\"/Content/images/expression/e_64.png\" />&nbsp;出神入化</span>";
                case 3:
                    return "<span class=\"text-info\" style=\"padding:5px 10px;font-size:15px;\"><img src=\"/Content/images/expression/e_02.png\" />&nbsp;登峰造极</span>";
                case 4:
                    return "<span class=\"text-success\" style=\"padding:5px 10px;font-size:15px;\"><img src=\"/Content/images/expression/e_27.png\" />&nbsp;功行圆满</span>";
                case 5:
                    return "<span class=\"text-primary\" style=\"padding:5px 10px;font-size:15px;\"><img src=\"/Content/images/expression/e_37.png\" />&nbsp;已臻大成</span>";
                case 6:
                    return "<span class=\"text-default\" style=\"padding:5px 10px;font-size:15px;\"><img src=\"/Content/images/expression/e_18.png\" />&nbsp;自成一派</span>";
                case 7:
                    return "<span class=\"text-default\" style=\"padding:5px 10px;font-size:15px;\"><img src=\"/Content/images/expression/e_24.png\" />&nbsp;炉火纯青</span>";
                case 8:
                    return "<span class=\"text-default\" style=\"padding:5px 10px;font-size:15px;\"><img src=\"/Content/images/expression/e_16.png\" />&nbsp;渐入佳境</span>";
                case 9:
                    return "<span class=\"text-default\" style=\"padding:5px 10px;font-size:15px;\"><img src=\"/Content/images/expression/e_08.png\" />&nbsp;略有小成</span>";
                case 10:
                    return "<span class=\"text-default\" style=\"padding:5px 10px;font-size:15px;\"><img src=\"/Content/images/expression/e_17.png\" />&nbsp;初亏堂奥</span>";
                default:
                    return "<span class=\"text-default\" style=\"padding:5px 10px;font-size:15px;\"><img src=\"/Content/images/expression/e_18.png\" />&nbsp;初学乍练</span>";
            }
        }
        //public string GetContentText(string FK_RELATIONID, string TableName)
        //{
        //    if (this.ContentManage.Get((COM_CONTENT p) => p.FK_RELATIONID == FK_RELATIONID && p.FK_TABLE == TableName) != null)
        //    {
        //        return Utils.DropHTML(this.ContentManage.Get((COM_CONTENT p) => p.FK_RELATIONID == FK_RELATIONID && p.FK_TABLE == TableName).CONTENT);
        //    }
        //    return "";
        //}
        //public int GetProgress(int projectId)
        //{
        //    List<PRO_PROJECT_STAGES> list = this.ProjectStageManage.LoadListAll((PRO_PROJECT_STAGES p) => p.FK_ProjectId == projectId);
        //    int result = 0;
        //    if (list != null && list.Count > 0)
        //    {
        //        int count = list.Count;
        //        int count2 = (
        //            from p in list
        //            where p.StageStatus == ClsDic.DicProject["已验收"] || p.StageStatus == ClsDic.DicProject["已超时"]
        //            select p).ToList<PRO_PROJECT_STAGES>().Count;
        //        result = ((count > 0) ? ((int)Math.Floor((double)count2 / ((double)count * 1.0) * 100.0)) : 0);
        //    }
        //    return result;
        //}
    }
}
