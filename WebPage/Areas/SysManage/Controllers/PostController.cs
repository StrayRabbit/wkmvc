using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Common.JsonHelper;
using Service.IService;
using WebPage.Controllers;

namespace WebPage.Areas.SysManage.Controllers
{
    public class PostController : BaseController
    {
        #region 声明容器
        /// <summary>
        /// 岗位
        /// </summary>
        IPostManage PostManage { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        IDepartmentManage DepartmentManage { get; set; }
        /// <summary>
        /// 字典编码
        /// </summary>
        ICodeManage CodeManage { get; set; }
        /// <summary>
        /// 岗位人员
        /// </summary>
        IPostUserManage PostUserManage { get; set; }
        #endregion

        [UserAuthorize(ModuleAlias = "Post", OperaAction = "View")]
        public ActionResult Home()
        {
            return base.View();
        }
        /// <summary>
        /// 岗位管理 岗位列表
        /// </summary>
        /// <returns></returns>
        [UserAuthorizeAttribute(ModuleAlias = "Post", OperaAction = "View")]
        public ActionResult Index()
        {
            try
            {
                #region 处理查询参数
                //获取部门ID
                var departId = Request.QueryString["departId"] ?? (Request.Form["departId"] ?? "");
                //岗位类型
                string posttype = Request.QueryString["posttype"];

                ViewBag.Search = base.keywords;
                #endregion

                //如果部门ID不为空或NULL
                if (!string.IsNullOrEmpty(departId))
                {
                    //部门信息
                    var department = this.DepartmentManage.Get(p => p.ID == departId);

                    ViewBag.Department = department;

                    ViewData["post"] = posttype;

                    ViewData["PostType"] = this.CodeManage.GetCode("POSTTYPE");

                    return View(BindList(posttype, departId));
                }

                return View();
            }
            catch (Exception e)
            {
                WriteLog(Common.Enums.enumOperator.Select, "对模块权限按钮的管理加载主页：", e);
                throw e.InnerException;
            }
        }

        /// <summary>
        /// 分页查询岗位列表
        /// </summary>
        private Common.PageInfo BindList(string posttype, string departId)
        {
            //基础数据
            var query = this.PostManage.LoadAll(null);
            //岗位类型
            if (!string.IsNullOrEmpty(posttype))
            {
                query = query.Where(p => p.POSTTYPE == posttype && p.FK_DEPARTID == departId);
            }
            else
            {
                query = query.Where(p => p.FK_DEPARTID == departId);
            }
            //查询关键字
            if (!string.IsNullOrEmpty(keywords))
            {
                query = query.Where(p => p.POSTNAME.Contains(keywords));
            }
            //排序
            query = query.OrderBy(p => p.SHOWORDER);
            //分页
            var result = this.PostManage.Query(query, page, pagesize);

            var list = result.List.Select(p => new
            {
                p.ID,
                p.POSTNAME,
                POSTTYPE = this.CodeManage.GetCode("POSTTYPE", p.POSTTYPE).First().NAMETEXT,
                p.CREATEDATE,
                USERCOUNT = PostUserManage.LoadAll(m => m.FK_POSTID == p.ID).Count()

            }).ToList();

            return new Common.PageInfo(result.Index, result.PageSize, result.Count, JsonConverter.JsonClass(list));
        }

        /// <summary>
        /// 加载详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UserAuthorizeAttribute(ModuleAlias = "Post", OperaAction = "Detail")]
        public ActionResult Detail(string id)
        {
            try
            {
                //岗位类型
                ViewData["PostType"] = this.CodeManage.GetCode("POSTTYPE");
                //获取部门ID
                var departId = Request.QueryString["departId"];

                var _entity = this.PostManage.Get(p => p.ID == id) ?? new Domain.SYS_POST() { FK_DEPARTID = departId };

                return View(_entity);
            }
            catch (Exception e)
            {
                WriteLog(Common.Enums.enumOperator.Select, "岗位管理加载详情：", e);
                throw e.InnerException;
            }
        }

        /// <summary>
        /// 保存岗位
        /// </summary>
        [UserAuthorizeAttribute(ModuleAlias = "Post", OperaAction = "Add,Edit")]
        public ActionResult Save(Domain.SYS_POST entity)
        {

            bool isEdit = false;
            var json = new JsonHelper() { Msg = "保存岗位成功", Status = "n", ReUrl = "/Post/Index" };
            try
            {
                if (entity != null)
                {
                    //添加
                    if (string.IsNullOrEmpty(entity.ID))
                    {
                        entity.ID = Guid.NewGuid().ToString();
                        entity.CREATEDATE = DateTime.Now;
                        entity.CREATEUSER = CurrentUser.Name;
                        entity.UPDATEDATE = DateTime.Now;
                        entity.UPDATEUSER = this.CurrentUser.Name;
                    }
                    else //修改
                    {
                        entity.UPDATEDATE = DateTime.Now;
                        entity.UPDATEUSER = this.CurrentUser.Name;
                        isEdit = true;
                    }
                    //判断岗位是否重名 
                    if (!this.PostManage.IsExist(p => p.POSTNAME == entity.POSTNAME && p.ID != entity.ID))
                    {
                        if (PostManage.SaveOrUpdate(entity, isEdit))
                        {
                            json.Status = "y";
                        }
                        else
                        {
                            json.Msg = "保存失败";
                        }
                    }
                    else
                    {
                        json.Msg = "岗位" + entity.POSTNAME + "已存在，不能重复添加";
                    }
                }
                else
                {
                    json.Msg = "未找到需要保存的岗位";
                }
                if (isEdit)
                {
                    WriteLog(Common.Enums.enumOperator.Edit, "修改岗位，结果：" + json.Msg, Common.Enums.enumLog4net.INFO);
                }
                else
                {
                    WriteLog(Common.Enums.enumOperator.Add, "添加岗位，结果：" + json.Msg, Common.Enums.enumLog4net.INFO);
                }
            }
            catch (Exception e)
            {
                json.Msg = "保存岗位发生内部错误！";
                WriteLog(Common.Enums.enumOperator.None, "保存岗位：", e);
            }
            return Json(json);


        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        [UserAuthorizeAttribute(ModuleAlias = "Post", OperaAction = "Remove")]
        public ActionResult Delete(string idList)
        {
            JsonHelper json = new JsonHelper() { Msg = "删除岗位完毕", Status = "n" };
            try
            {
                if (!string.IsNullOrEmpty(idList))
                {
                    idList = idList.Trim(',');
                    //判断岗位是否分配人员
                    if (!this.PostUserManage.IsExist(p => idList.Contains(p.FK_POSTID)))
                    {
                        this.PostManage.Delete(p => idList.Contains(p.ID));
                        json.Status = "y";
                    }
                    else
                    {
                        json.Msg = "该岗位已经分配人员，不能删除";
                    }
                }
                else
                {
                    json.Msg = "未找到要删除的岗位记录";
                }
                WriteLog(Common.Enums.enumOperator.Remove, "删除岗位，结果：" + json.Msg, Common.Enums.enumLog4net.WARN);
            }
            catch (Exception e)
            {
                json.Msg = "删除岗位发生内部错误！";
                WriteLog(Common.Enums.enumOperator.Remove, "删除岗位：", e);
            }
            return Json(json);
        }

        /// <summary>
        /// 获取岗位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPostByDepart()
        {
            var departId = Request.Form["departId"];
            if (!string.IsNullOrEmpty(departId))
            {
                return Json(this.PostManage.LoadAll(p => p.FK_DEPARTID == departId)
                .Select(p => new
                {
                    ID = p.ID,
                    NAME = p.POSTNAME
                }).ToList(), JsonRequestBehavior.AllowGet);
            }
            return new EmptyResult();
        }
    }
}