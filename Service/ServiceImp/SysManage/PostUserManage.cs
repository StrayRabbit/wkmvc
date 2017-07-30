using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.ServiceImp
{
    /// <summary>
    /// 岗位人员关系业务实现类
    /// add yuangang by 2016-05-19
    /// </summary>
    public class PostUserManage : RepositoryBase<Domain.SYS_POST_USER>, IService.IPostUserManage
    {
        ///// <summary>
        ///// 根据岗位ID获取人员列表
        ///// </summary>
        //public List<Domain.SYS_USER> GetUserListByPostId(string postId)
        //{
        //    try
        //    {
        //        string sql = @"select * from sys_user t where exists(select u.fk_userid from sys_post_user u
        //                        inner join sys_post_department p
        //                        on u.fk_post_departmentid=p.id
        //                        where t.id=u.fk_userid and p.fk_post_id in (" + postId + ")  group by u.fk_userid)";
        //        return this.SelectBySql<Domain.SYS_USER>(sql);
        //    }
        //    catch (Exception e) { throw e.InnerException; }
        //}
        ///// <summary>
        ///// 根据用户ID获取所持有的岗位集合
        ///// </summary>
        //public List<Domain.SYS_POST> GetPostListByUserId(string userId)
        //{
        //    dbSet.Where()
        //    return this.LoadAll(p => userId.Contains(p.FK_USERID.ToString())).Select(p => p.FK_POSTID).ToList();
        //}

        ///// <summary>
        ///// 添加岗位人员关系
        ///// </summary>
        ///// <param name="userId">人员ID</param>
        ///// <param name="postId">岗位ID集合</param>
        ///// <returns></returns>
        //public bool SavePostUser(int userId, string postId)
        //{
        //    try
        //    {
        //        if (this.IsExist(p => p.FK_USERID == userId))
        //        {
        //            //存在之后再对比是否一致 
        //            var oldCount = this.LoadAll(p => p.FK_USERID == userId).Select(p => p.FK_POST_DEPARTMENTID).ToList().Cast<int>().ToList();
        //            var newpostId = postId.Trim(',').Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToList();
        //            if (oldCount.Count == newpostId.Count && oldCount.All(newpostId.Contains)) return true;
        //            //删除原有关系
        //            this.Delete(p => p.FK_USERID == userId);
        //        }
        //        if (!string.IsNullOrEmpty(postId))
        //        {
        //            //添加现有关系
        //            var list = postId.Split(',').Select(item => new Domain.SYS_POST_USER()
        //            {
        //                FK_USERID = userId,
        //                FK_POST_DEPARTMENTID = int.Parse(item)
        //            }).ToList();
        //            return this.SaveList(list) > 0;
        //        }
        //        return true;
        //    }
        //    catch (Exception e) { throw e.InnerException; }
        //}

        ///// <summary>
        ///// 根据岗位集合获取岗位名称，部门-岗位模式        
        ///// </summary>
        //public dynamic GetPostNameBySysPostUser(ICollection<Domain.SYS_POST_USER> collection)
        //{
        //    //岗位部门关系ID集合
        //    string post_departmentid = collection.Select(p => p.FK_POST_DEPARTMENTID).Aggregate(string.Empty, (current, t) => current + "'" + t + "',").TrimEnd(',');
        //    try
        //    {
        //        string sql = @"select d.name+'-'+p.postname as postname,s.id from sys_department d inner join
        //                sys_post_department s on d.id=s.fk_department_id
        //                inner join sys_post p on p.id=s.fk_post_id 
        //                where s.id in (" + post_departmentid + ")";
        //        return this.ExecuteSqlQuery(sql);
        //    }
        //    catch (Exception e) { throw e.InnerException; }
        //}
    }
}