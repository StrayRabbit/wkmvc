using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPage.Areas.ComManage.Models
{
    public class FileModel
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 文件全称
        /// </summary>
        public string fullname { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 文件格式
        /// </summary>
        public string ext { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// 文件图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 是否为文件夹
        /// </summary>
        public bool isfolder { get; set; }
        /// <summary>
        /// 是否为图片
        /// </summary>
        public bool isImage { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime time { get; set; }
    }
}