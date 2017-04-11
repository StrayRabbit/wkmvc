using System.Collections.Generic;
using System.Linq;

namespace Service.IService
{
    /// <summary>
    /// Service层代码配置接口
    /// add yuangang by 2015-05-22
    /// </summary>
    public interface ICodeManage : IRepository<Domain.SYS_CODE>
    {
        /// <summary>
        /// 根据编码类型获取编码集合
        /// </summary>
        /// <param name="codetype">编码类型</param>
        /// <param name="codevalue">编码值</param>
        List<Domain.SYS_CODE> GetCode(string codetype, params string[] codevalue);
        /// <summary>
        /// 通过字典查询字典指向的编码集合
        /// </summary>
        IQueryable<Domain.SYS_CODE> GetDicType();
        /// <summary>
        /// 根据字典ID与类型获取一条数据
        /// </summary>
        string GetCodeByID(int id, string codetype);
        /// <summary>
        /// 根据字典编码值与类型获取一条数据
        /// </summary>
        string GetCodeNameByCodeValue(string codeType, string codevalue);
    }
}