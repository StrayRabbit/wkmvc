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
    public class DepartmentController : BaseController
    {
        IDepartmentManage DepartmentManage { get; set; }

        /// <summary>
        /// 岗位管理
        /// </summary>
        IPostManage PostManage { get; set; }

        /// <summary>
        /// 加载主页
        /// </summary>
        [UserAuthorizeAttribute(ModuleAlias = "Department", OperaAction = "View")]
        public ActionResult Index()
        {
            try
            {
                ViewBag.Search = base.keywords;
                return View(BindList());
            }
            catch (Exception e)
            {
                WriteLog(Common.Enums.enumOperator.Select, "部门管理加载主页：", e);
                throw e.InnerException;
            }
        }

        /// <summary>
        /// 加载详情页
        /// </summary>
        [UserAuthorizeAttribute(ModuleAlias = "Department", OperaAction = "Detail")]
        public ActionResult Detail(string id)
        {
            try
            {
                var entity = new Domain.SYS_DEPARTMENT();

                ViewBag.moduleparent = this.DepartmentManage.GetDepartmentByDetail();

                //添加子部门
                string parentId = Request.QueryString["parentId"];

                if (!string.IsNullOrEmpty(parentId))
                {
                    entity.PARENTID = parentId;
                }
                if (!string.IsNullOrEmpty(id))
                {
                    entity = this.DepartmentManage.Get(p => p.ID == id);
                }
                return View(entity);
            }
            catch (Exception e)
            {
                WriteLog(Common.Enums.enumOperator.Select, "部门管理加载详情页：", e);
                throw e.InnerException;
            }
        }


        /// <summary>
        /// 保存部门
        /// </summary>
        [ValidateInput(false)]
        [UserAuthorizeAttribute(ModuleAlias = "Department", OperaAction = "Add,Edit")]
        public ActionResult Save(Domain.SYS_DEPARTMENT entity)
        {
            bool isEdit = false;
            var json = new JsonHelper() { Msg = "保存成功", Status = "n" };
            try
            {
                var _entity = new Domain.SYS_DEPARTMENT();
                if (entity != null)
                {
                    if (!string.IsNullOrEmpty(entity.ID))
                    {
                        #region 修改
                        _entity = this.DepartmentManage.Get(p => p.ID == entity.ID);
                        entity.CREATEDATE = _entity.CREATEDATE;
                        entity.CREATEPERID = _entity.CREATEPERID;
                        entity.UPDATEDATE = DateTime.Now;
                        entity.UPDATEUSER = this.CurrentUser.Name;
                        if (entity.PARENTID != _entity.PARENTID)
                        {
                            entity.CODE = this.DepartmentManage.CreateCode(entity.PARENTID);
                        }
                        else
                        {
                            entity.CODE = _entity.CODE;
                        }
                        //获取父级记录
                        if (string.IsNullOrEmpty(_entity.PARENTID))
                        {
                            //业务等级
                            entity.BUSINESSLEVEL = 1;
                            entity.PARENTCODE = null;
                        }
                        else
                        {
                            var parententity = this.DepartmentManage.Get(p => p.ID == entity.PARENTID);
                            entity.BUSINESSLEVEL = parententity.BUSINESSLEVEL + 1;
                            entity.PARENTCODE = parententity.CODE;
                        }
                        #endregion
                        isEdit = true;
                        _entity = entity;
                    }
                    else
                    {
                        #region 添加
                        _entity = entity;
                        _entity.ID = Guid.NewGuid().ToString();
                        _entity.CREATEDATE = DateTime.Now;
                        _entity.CREATEPERID = this.CurrentUser.Name;
                        _entity.UPDATEDATE = DateTime.Now;
                        _entity.UPDATEUSER = this.CurrentUser.Name;
                        //根据上级部门的ID确定当前部门的CODE
                        _entity.CODE = this.DepartmentManage.CreateCode(entity.PARENTID);
                        //获取父级记录
                        if (string.IsNullOrEmpty(entity.PARENTID))
                        {
                            //业务等级
                            entity.BUSINESSLEVEL = 1;
                            entity.PARENTCODE = null;
                        }
                        else
                        {
                            var parententity = this.DepartmentManage.Get(p => p.ID == entity.PARENTID);
                            entity.BUSINESSLEVEL = parententity.BUSINESSLEVEL + 1;
                            entity.PARENTCODE = parententity.CODE;
                        }
                        #endregion
                    }
                    //判断同一个部门下，是否重名 
                    var predicate = PredicateBuilder.True<Domain.SYS_DEPARTMENT>();
                    predicate = predicate.And(p => p.PARENTID == _entity.PARENTID);
                    predicate = predicate.And(p => p.NAME == _entity.NAME);
                    predicate = predicate.And(p => p.ID != _entity.ID);
                    if (!this.DepartmentManage.IsExist(predicate))
                    {
                        if (this.DepartmentManage.SaveOrUpdate(_entity, isEdit))
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
                        json.Msg = "部门" + entity.NAME + "已存在，不能重复添加";
                    }
                }
                else
                {
                    json.Msg = "未找到需要保存的部门信息";
                }
                if (isEdit)
                {
                    WriteLog(Common.Enums.enumOperator.Edit, "修改部门信息，结果：" + json.Msg, Common.Enums.enumLog4net.INFO);
                }
                else
                {
                    WriteLog(Common.Enums.enumOperator.Add, "添加部门信息，结果：" + json.Msg, Common.Enums.enumLog4net.INFO);
                }
            }
            catch (Exception e)
            {
                json.Msg = "保存部门信息发生内部错误！";
                WriteLog(Common.Enums.enumOperator.None, "保存部门信息：", e);
            }
            return Json(json);

        }


        /// <summary>
        /// 删除部门
        /// </summary>
        [UserAuthorizeAttribute(ModuleAlias = "Department", OperaAction = "Remove")]
        public ActionResult Delete(string idList)
        {
            JsonHelper json = new JsonHelper() { Msg = "删除部门成功", ReUrl = "/Department/Index", Status = "n" };
            try
            {
                if (!string.IsNullOrEmpty(idList))
                {
                    idList = idList.TrimEnd(',');
                    //判断是否有下属部门
                    if (!this.DepartmentManage.DepartmentIsExists(idList))
                    {
                        //判断该部门是否有岗位
                        if (!this.PostManage.IsExist(p => idList.Contains(p.FK_DEPARTID)))
                        {
                            var idList1 = idList.Split(',').ToList();
                            this.DepartmentManage.Delete(p => idList.Contains(p.ID));
                            json.Status = "y";
                        }
                        else
                        {
                            json.Msg = "该部门有岗位信息不能删除";
                        }
                    }
                    else
                    {
                        json.Msg = "该部门有下属部门不能删除";
                    }
                }
                else
                {
                    json.Msg = "未找到要删除的部门记录";
                }
                WriteLog(Common.Enums.enumOperator.Remove, "删除部门：" + json.Msg, Common.Enums.enumLog4net.WARN);
            }
            catch (Exception e)
            {
                json.Msg = "删除部门发生内部错误！";
                WriteLog(Common.Enums.enumOperator.Remove, "删除部门：", e);
            }
            return Json(json);
        }

        /// <summary>
        /// 获取部门树形菜单
        /// </summary>
        public ActionResult GetTree()
        {
            var json = new JsonHelper() { Msg = "Success", Status = "y" };

            try
            {
                //获取部门列表 按照 SHOWORDER字段 升序排列
                var query = this.DepartmentManage.LoadAll(null).OrderBy(p => p.SHOWORDER).ToList();
                var result = query.Select(m => new
                {
                    id = m.ID,
                    parent = !string.IsNullOrEmpty(m.PARENTID) ? m.PARENTID.ToString() : "#",
                    text = m.NAME,
                    icon = m.BUSINESSLEVEL == 0 ? "fa fa-circle text-warning" : "fa fa-circle text-navy"
                }).ToList();

                json.Data = result;
            }
            catch (Exception e)
            {
                json.Status = "n";
                json.Msg = "服务器忙，请稍后再试！";
                WriteLog(Common.Enums.enumOperator.Select, "权限管理，获取模块树：", e);
            }
            return Json(json);
        }


        /// <summary>
        /// 分页查询部门列表
        /// </summary>
        private object BindList()
        {
            //基础数据
            var query = this.DepartmentManage.LoadAll(null);

            //递归排序（无分页）
            var result = this.DepartmentManage.RecursiveDepartment(query.ToList())
                .Select(p => new
                {
                    p.ID,
                    p.NAME,
                    DepartName = DepartmentManage.GetDepartmentName(p.NAME, p.BUSINESSLEVEL),
                    p.BUSINESSLEVEL,
                    p.SHOWORDER,
                    p.CREATEDATE
                });

            //查询关键字
            if (!string.IsNullOrEmpty(keywords))
            {
                result = result.Where(p => p.NAME.Contains(keywords));
            }

            return JsonConverter.JsonClass(result);
        }
    }
}