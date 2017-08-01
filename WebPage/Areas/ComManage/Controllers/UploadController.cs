using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Common.JsonHelper;
using Service.IService;
using WebPage.Areas.ComManage.Models;
using WebPage.Controllers;

namespace WebPage.Areas.ComManage.Controllers
{
    public class UploadController : BaseController
    {
        IUploadManage UploadManage { get; set; }
        /// <summary>
        /// 文件管理默认页面
        /// </summary>
        /// <returns></returns>
        [UserAuthorizeAttribute(ModuleAlias = "Files", OperaAction = "View")]
        public ActionResult Home()
        {
            var fileExt = Request.QueryString["fileExt"] ?? "";
            ViewData["fileExt"] = fileExt;
            return View();
        }

        /// <summary>
        /// 通过路径获取所有文件
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllFileData()
        {
            string fileExt = Request.Form["fileExt"];
            var jsonM = new JsonHelper() { Status = "y", Msg = "success" };
            try
            {
                var images = ConfigurationManager.AppSettings["Image"].Trim(',').Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => "." + p).ToList();
                var videos = ConfigurationManager.AppSettings["Video"].Trim(',').Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => "." + p).ToList();
                var musics = ConfigurationManager.AppSettings["Music"].Trim(',').Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => "." + p).ToList();
                var documents = ConfigurationManager.AppSettings["Document"].Trim(',').Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => "." + p).ToList();

                switch (fileExt)
                {
                    case "images":

                        jsonM.Data = Utils.DataTableToList<FileModel>(FileHelper.GetAllFileTable(Server.MapPath(ConfigurationManager.AppSettings["uppath"]))).OrderByDescending(p => p.name).Where(p => images.Any(e => e == p.ext)).ToList();
                        break;
                    case "videos":

                        jsonM.Data = Utils.DataTableToList<FileModel>(FileHelper.GetAllFileTable(Server.MapPath(ConfigurationManager.AppSettings["uppath"]))).OrderByDescending(p => p.name).Where(p => videos.Any(e => e == p.ext)).ToList();
                        break;
                    case "musics":

                        jsonM.Data = Utils.DataTableToList<FileModel>(FileHelper.GetAllFileTable(Server.MapPath(ConfigurationManager.AppSettings["uppath"]))).OrderByDescending(p => p.name).Where(p => musics.Any(e => e == p.ext)).ToList();
                        break;
                    case "files":

                        jsonM.Data = Utils.DataTableToList<FileModel>(FileHelper.GetAllFileTable(Server.MapPath(ConfigurationManager.AppSettings["uppath"]))).OrderByDescending(p => p.name).Where(p => documents.Any(e => e == p.ext)).ToList();
                        break;
                    case "others":

                        jsonM.Data = Utils.DataTableToList<FileModel>(FileHelper.GetAllFileTable(Server.MapPath(ConfigurationManager.AppSettings["uppath"]))).OrderByDescending(p => p.name).Where(p => !images.Contains(p.ext) && !videos.Contains(p.ext) && !musics.Contains(p.ext) && !documents.Contains(p.ext)).ToList();
                        break;
                    default:
                        jsonM.Data = Utils.DataTableToList<FileModel>(FileHelper.GetAllFileTable(Server.MapPath(ConfigurationManager.AppSettings["uppath"]))).OrderByDescending(p => p.name).ToList();
                        break;
                }

            }
            catch (Exception e)
            {
                jsonM.Status = "err";
                jsonM.Msg = "获取文件失败！";
            }
            return Content(JsonConverter.Serialize(jsonM, true));
        }


        /// <summary>
        /// 删除文件或文件夹
        /// </summary>
        /// <returns></returns>
        [UserAuthorizeAttribute(ModuleAlias = "Files", OperaAction = "Remove")]
        public ActionResult DeleteBy()
        {
            var jsonM = new JsonHelper() { Status = "y", Msg = "success" };
            try
            {
                var path = Request.Form["path"].Trim(';').Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p).ToList();

                foreach (var file in path)
                {
                    //删除文件
                    FileHelper.DeleteFile(Server.MapPath(file));
                }

                WriteLog(Common.Enums.enumOperator.Remove, "删除文件：" + path, Common.Enums.enumLog4net.WARN);
            }
            catch (Exception ex)
            {
                jsonM.Status = "err";
                jsonM.Msg = "删除过程中发生错误！";
                WriteLog(Common.Enums.enumOperator.Remove, "删除文件发生错误：", ex);
            }
            return Json(jsonM);
        }



        /// <summary>
        /// 复制文件到文件夹
        /// </summary>
        /// <returns></returns>
        [UserAuthorizeAttribute(ModuleAlias = "Files", OperaAction = "Copy")]
        public ActionResult Copy(string files)
        {
            ViewData["Files"] = files;
            ViewData["spath"] = ConfigurationManager.AppSettings["uppath"];
            return View();
        }
        [UserAuthorizeAttribute(ModuleAlias = "Files", OperaAction = "Copy")]
        public ActionResult CopyFiles()
        {
            var json = new JsonHelper() { Msg = "复制文件完成", Status = "n" };

            try
            {
                var files = Request.Form["files"].Trim(';').Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p).ToList();
                var path = Request.Form["path"];
                foreach (var file in files)
                {
                    FileHelper.Copy(Server.MapPath(file), Server.MapPath(path) + FileHelper.GetFileName(Server.MapPath(file)));
                }
                WriteLog(Common.Enums.enumOperator.None, "复制文件：" + Request.Form["files"].Trim(';') + "，结果：" + json.Msg, Common.Enums.enumLog4net.WARN);
                json.Status = "y";
            }
            catch (Exception e)
            {
                json.Msg = "复制文件失败！";
                WriteLog(Common.Enums.enumOperator.None, "复制文件：", e);
            }

            return Json(json);
        }


        /// <summary>
        /// 移动文件到文件夹
        /// </summary>
        /// <returns></returns>
        [UserAuthorizeAttribute(ModuleAlias = "Files", OperaAction = "Cut")]
        public ActionResult Cut(string files)
        {
            ViewData["Files"] = files;
            ViewData["spath"] = ConfigurationManager.AppSettings["uppath"];
            return View();
        }
        [UserAuthorizeAttribute(ModuleAlias = "Files", OperaAction = "Cut")]
        public ActionResult CutFiles()
        {
            var json = new JsonHelper() { Msg = "移动文件完成", Status = "n" };

            try
            {
                var files = Request.Form["files"].Trim(';').Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p).ToList();
                var path = Request.Form["path"];
                foreach (var file in files)
                {
                    FileHelper.Move(Server.MapPath(file), Server.MapPath(path));
                }
                WriteLog(Common.Enums.enumOperator.None, "移动文件：" + Request.Form["files"].Trim(';') + "，结果：" + json.Msg, Common.Enums.enumLog4net.WARN);
                json.Status = "y";
            }
            catch (Exception e)
            {
                json.Msg = "移动文件失败！";
                WriteLog(Common.Enums.enumOperator.None, "移动文件：", e);
            }

            return Json(json);
        }


        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <returns></returns>
        [UserAuthorizeAttribute(ModuleAlias = "Files", OperaAction = "Compress")]
        public ActionResult Compress(string files)
        {
            ViewData["Files"] = files;
            ViewData["spath"] = ConfigurationManager.AppSettings["uppath"];
            return View();
        }
        [UserAuthorizeAttribute(ModuleAlias = "Files", OperaAction = "Compress")]
        public ActionResult CompressFiles()
        {
            var json = new JsonHelper() { Msg = "压缩文件完成", Status = "n" };

            try
            {
                var files = Request.Form["files"].Trim(';').Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p).ToList();
                var path = Request.Form["path"];
                foreach (var file in files)
                {
                    ZipHelper.ZipFile(Server.MapPath(file), Server.MapPath(path));
                }
                //ZipHelper.ZipDirectory(Server.MapPath("/upload/files/"), Server.MapPath(path));
                WriteLog(Common.Enums.enumOperator.None, "压缩文件：" + Request.Form["files"].Trim(';') + "，结果：" + json.Msg, Common.Enums.enumLog4net.WARN);
                json.Status = "y";
            }
            catch (Exception e)
            {
                json.Msg = "压缩文件失败！";
                WriteLog(Common.Enums.enumOperator.None, "压缩文件：", e);
            }

            return Json(json);
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <returns></returns>
        [UserAuthorizeAttribute(ModuleAlias = "Files", OperaAction = "Expand")]
        public ActionResult Expand(string files)
        {
            ViewData["Files"] = files;
            ViewData["spath"] = ConfigurationManager.AppSettings["uppath"];
            return View();
        }
        [UserAuthorizeAttribute(ModuleAlias = "Files", OperaAction = "Expand")]
        public ActionResult ExpandFiles()
        {
            var json = new JsonHelper() { Msg = "解压文件完成", Status = "n" };

            try
            {
                var files = Request.Form["files"];
                var path = Request.Form["path"];
                var password = Request.Form["password"];

                if (string.IsNullOrEmpty(password))
                {
                    json.Msg = "请输入解压密码！";
                    return Json(json);
                }

                string CurrentPassword = ConfigurationManager.AppSettings["ZipPassword"] != null ? new Common.CryptHelper.AESCrypt().Decrypt(ConfigurationManager.AppSettings["ZipPassword"].ToString()) : "guodongbudingxizhilang";

                if (password != CurrentPassword)
                {
                    json.Msg = "解压密码无效！";
                    return Json(json);
                }

                ZipHelper.UnZip(Server.MapPath(files), Server.MapPath(path), password);

                WriteLog(Common.Enums.enumOperator.None, "解压文件：" + Request.Form["files"].Trim(';') + "，结果：" + json.Msg, Common.Enums.enumLog4net.WARN);
                json.Status = "y";
            }
            catch (Exception e)
            {
                json.Msg = "解压文件失败！";
                WriteLog(Common.Enums.enumOperator.None, "解压文件：", e);
            }

            return Json(json);
        }


        /// <summary>
        /// 单文件上传视图
        /// </summary>
        /// <returns></returns>
        public ActionResult FileMain()
        {
            ViewData["spath"] = ConfigurationManager.AppSettings["uppath"];
            return View();
        }

        /// <summary>
        /// 单个文件上传
        /// </summary>
        [HttpPost]
        public ActionResult SignUpFile()
        {
            var jsonM = new JsonHelper() { Status = "n", Msg = "success" };
            try
            {
                //取得上传文件
                HttpPostedFileBase upfile = Request.Files["fileUp"];

                //原始文件路径
                string delpath = Request.QueryString["delpath"];

                //缩略图
                bool isThumbnail = string.IsNullOrEmpty(Request.QueryString["isThumbnail"]) ? false : Request.QueryString["isThumbnail"].ToLower() == "true" ? true : false;
                //水印
                bool isWater = string.IsNullOrEmpty(Request.QueryString["isWater"]) ? false : Request.QueryString["isWater"].ToLower() == "true" ? true : false;


                if (upfile == null)
                {
                    jsonM.Msg = "请选择要上传文件！";
                    return Json(jsonM);
                }
                jsonM = FileSaveAs(upfile, isThumbnail, isWater);

                #region 移除原始文件
                if (jsonM.Status == "y" && !string.IsNullOrEmpty(delpath))
                {
                    if (System.IO.File.Exists(Utils.GetMapPath(delpath)))
                    {
                        System.IO.File.Delete(Utils.GetMapPath(delpath));
                    }
                }
                #endregion

                if (jsonM.Status == "y")
                {
                    #region 记录上传数据
                    string unit = string.Empty;
                    var jsonValue = JsonConverter.ConvertJson(jsonM.Data.ToString());
                    var entity = new Domain.COM_UPLOAD()
                    {
                        ID = Guid.NewGuid().ToString(),
                        FK_USERID = CurrentUser.Id.ToString(),
                        UPOPEATOR = CurrentUser.Name,
                        UPTIME = DateTime.Now,
                        UPOLDNAME = jsonValue.oldname,
                        UPNEWNAME = jsonValue.newname,
                        UPFILESIZE = FileHelper.GetDiyFileSize(long.Parse(jsonValue.size), out unit),
                        UPFILEUNIT = unit,
                        UPFILEPATH = jsonValue.path,
                        UPFILESUFFIX = jsonValue.ext,
                        UPFILETHUMBNAIL = jsonValue.thumbpath,
                        UPFILEIP = Utils.GetIP(),
                        UPFILEURL = "http://" + Request.Url.AbsoluteUri.Replace("http://", "").Substring(0, Request.Url.AbsoluteUri.Replace("http://", "").IndexOf('/'))
                    };
                    this.UploadManage.Save(entity);
                    #endregion

                    #region 返回文件信息
                    jsonM.Data = "{\"oldname\": \"" + jsonValue.oldname + "\","; //原始名称
                    jsonM.Data += " \"newname\":\"" + jsonValue.newname + "\","; //新名称
                    jsonM.Data += " \"path\": \"" + jsonValue.path + "\", ";  //路径
                    jsonM.Data += " \"thumbpath\":\"" + jsonValue.thumbpath + "\","; //缩略图
                    jsonM.Data += " \"size\": \"" + jsonValue.size + "\",";   //大小
                    jsonM.Data += " \"id\": \"" + entity.ID + "\",";   //上传文件ID
                    jsonM.Data += " \"uptime\": \"" + entity.UPTIME + "\",";   //上传时间
                    jsonM.Data += " \"operator\": \"" + entity.UPOPEATOR + "\",";   //上传人
                    jsonM.Data += " \"unitsize\": \"" + entity.UPFILESIZE + unit + "\",";   //带单位大小
                    jsonM.Data += "\"ext\":\"" + jsonValue.ext + "\"}";//后缀名
                    #endregion
                }

            }
            catch (Exception ex)
            {
                jsonM.Msg = "上传过程中发生错误，消息：" + ex.Message;
                jsonM.Status = "n";
            }
            return Json(jsonM);
        }



        /// <summary>
        /// 通过路径获取文件
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFileData()
        {
            string path = Request.Form["path"];
            var jsonM = new JsonHelper() { Status = "y", Msg = "success" };
            try
            {
                if (!FileHelper.IsExistDirectory(Server.MapPath(path)))
                {
                    jsonM.Status = "n";
                    jsonM.Msg = "目录不存在！";
                }
                else if (FileHelper.IsEmptyDirectory(Server.MapPath(path)))
                {
                    jsonM.Status = "empty";
                }
                else
                {
                    jsonM.Data = Utils.DataTableToList<FileModel>(FileHelper.GetFileTable(Server.MapPath(path))).OrderByDescending(p => p.name).ToList();
                }
            }
            catch (Exception)
            {
                jsonM.Status = "err";
                jsonM.Msg = "获取文件失败！";
            }
            return Content(JsonConverter.Serialize(jsonM, true));
        }

        #region private
        /// <summary>
        /// 文件上传方法
        /// </summary>
        /// <param name="postedFile">文件流</param>
        /// <param name="isThumbnail">是否生成缩略图</param>
        /// <param name="isWater">是否生成水印</param>
        /// <returns>上传后文件信息</returns>
        private JsonHelper FileSaveAs(HttpPostedFileBase postedFile, bool isThumbnail, bool isWater)
        {
            var jsons = new JsonHelper { Status = "n" };
            try
            {
                string fileExt = Utils.GetFileExt(postedFile.FileName); //文件扩展名，不含“.”
                int fileSize = postedFile.ContentLength; //获得文件大小，以字节为单位
                string fileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\") + 1); //取得原文件名
                string newFileName = Utils.GetRamCode() + "." + fileExt; //随机生成新的文件名
                string upLoadPath = GetUpLoadPath(fileExt); //上传目录相对路径
                string fullUpLoadPath = Utils.GetMapPath(upLoadPath); //上传目录的物理路径
                string newFilePath = upLoadPath + newFileName; //上传后的路径
                string newThumbnailFileName = "thumb_" + newFileName; //随机生成缩略图文件名

                //检查文件扩展名是否合法
                if (!CheckFileExt(fileExt))
                {
                    jsons.Msg = "不允许上传" + fileExt + "类型的文件！";
                    return jsons;
                }

                //检查文件大小是否合法
                if (!CheckFileSize(fileExt, fileSize))
                {
                    jsons.Msg = "文件超过限制的大小啦！";
                    return jsons;
                }

                //检查上传的物理路径是否存在，不存在则创建
                if (!Directory.Exists(fullUpLoadPath))
                {
                    Directory.CreateDirectory(fullUpLoadPath);
                }

                //检查文件是否真实合法
                //如果文件真实合法 则 保存文件 关闭文件流
                //if (!CheckFileTrue(postedFile, fullUpLoadPath + newFileName))
                //{
                //    jsons.Msg = "不允许上传不可识别的文件!";
                //    return jsons;
                //}

                //保存文件
                postedFile.SaveAs(fullUpLoadPath + newFileName);

                string thumbnail = string.Empty;

                //如果是图片，检查是否需要生成缩略图，是则生成
                if (IsImage(fileExt) && isThumbnail && ConfigurationManager.AppSettings["ThumbnailWidth"].ToString() != "0" && ConfigurationManager.AppSettings["ThumbnailHeight"].ToString() != "0")
                {
                    Thumbnail.MakeThumbnailImage(fullUpLoadPath + newFileName, fullUpLoadPath + newThumbnailFileName,
                       int.Parse(ConfigurationManager.AppSettings["ThumbnailWidth"]), int.Parse(ConfigurationManager.AppSettings["ThumbnailHeight"]), "W");
                    thumbnail = upLoadPath + newThumbnailFileName;
                }
                //如果是图片，检查是否需要打水印
                if (IsImage(fileExt) && isWater)
                {
                    switch (ConfigurationManager.AppSettings["WatermarkType"].ToString())
                    {
                        case "1":
                            WaterMark.AddImageSignText(newFilePath, newFilePath,
                                ConfigurationManager.AppSettings["WatermarkText"], int.Parse(ConfigurationManager.AppSettings["WatermarkPosition"]),
                                int.Parse(ConfigurationManager.AppSettings["WatermarkImgQuality"]), ConfigurationManager.AppSettings["WatermarkFont"], int.Parse(ConfigurationManager.AppSettings["WatermarkFontsize"]));
                            break;
                        case "2":
                            WaterMark.AddImageSignPic(newFilePath, newFilePath,
                                ConfigurationManager.AppSettings["WatermarkPic"], int.Parse(ConfigurationManager.AppSettings["WatermarkPosition"]),
                                int.Parse(ConfigurationManager.AppSettings["WatermarkImgQuality"]), int.Parse(ConfigurationManager.AppSettings["WatermarkTransparency"]));
                            break;
                    }
                }

                string unit = string.Empty;

                //处理完毕，返回JOSN格式的文件信息
                jsons.Data = "{\"oldname\": \"" + fileName + "\",";     //原始文件名
                jsons.Data += " \"newname\":\"" + newFileName + "\",";  //文件新名称
                jsons.Data += " \"path\": \"" + newFilePath + "\", ";   //文件路径
                jsons.Data += " \"thumbpath\":\"" + thumbnail + "\",";  //缩略图路径
                jsons.Data += " \"size\": \"" + fileSize + "\",";       //文件大小
                jsons.Data += "\"ext\":\"" + fileExt + "\"}";           //文件格式
                jsons.Status = "y";
                return jsons;
            }
            catch
            {
                jsons.Msg = "上传过程中发生意外错误！";
                return jsons;
            }
        }

        private bool CheckFileExt(string _fileExt)
        {
            string[] array = new[]
            {
                "asp",
                "aspx",
                "php",
                "jsp",
                "htm",
                "html"
            };
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].ToLower() == _fileExt.ToLower())
                {
                    return false;
                }
            }
            List<string> list = (from p in ConfigurationManager.AppSettings["AttachExtension"].Trim(',').Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                 select p).ToList();
            return list.Contains(_fileExt.ToLower());
        }
        private bool IsImage(string _fileExt)
        {
            List<string> list = (from p in ConfigurationManager.AppSettings["Image"].Trim(',').Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                 select p).ToList();
            return list.Contains(_fileExt.ToLower());
        }
        private bool IsDocument(string _fileExt)
        {
            List<string> list = (from p in ConfigurationManager.AppSettings["Document"].Trim(',').Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                 select p).ToList<string>();
            return list.Contains(_fileExt.ToLower());
        }
        private bool IsVideos(string _fileExt)
        {
            List<string> list = (from p in ConfigurationManager.AppSettings["Video"].Trim(',').Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                 select p).ToList<string>();
            return list.Contains(_fileExt.ToLower());
        }
        private bool IsMusics(string _fileExt)
        {
            List<string> list = (from p in ConfigurationManager.AppSettings["Music"].Trim(',').Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                 select p).ToList();
            return list.Contains(_fileExt.ToLower());
        }

        private bool CheckFileSize(string _fileExt, int _fileSize)
        {
            if (IsImage(_fileExt))
            {
                if (_fileSize / 1024 > int.Parse(ConfigurationManager.AppSettings["AttachImagesize"].ToString()))
                {
                    return false;
                }
            }
            else if (IsVideos(_fileExt))
            {
                if (_fileSize / 1024 > int.Parse(ConfigurationManager.AppSettings["AttachVideosize"].ToString()))
                {
                    return false;
                }
            }
            else if (IsDocument(_fileExt))
            {
                if (_fileSize / 1024 > int.Parse(ConfigurationManager.AppSettings["AttachDocmentsize"].ToString()))
                {
                    return false;
                }
            }
            else if (_fileSize / 1024 > int.Parse(ConfigurationManager.AppSettings["AttachFilesize"].ToString()))
            {
                return false;
            }
            return true;
        }
        private string GetUpLoadPath(string _fileExt)
        {
            string text = ConfigurationManager.AppSettings["uppath"];
            if (IsImage(_fileExt))
            {
                text += "images/";
            }
            else if (IsVideos(_fileExt))
            {
                text += "videos/";
            }
            else if (IsDocument(_fileExt))
            {
                text += "files/";
            }
            else if (IsMusics(_fileExt))
            {
                text += "musics/";
            }
            else if (_fileExt == "bak")
            {
                text = "/App_Data/BackUp/DataBaseBackUp/";
            }
            else
            {
                text += "others/";
            }
            if (!CurrentUser.IsAdmin)
            {
                text = text + CurrentUser.PinYin + "/";
            }
            string text2 = text;
            return string.Concat(new[]
            {
                text2,
                DateTime.Now.ToString("yyyy"),
                "/",
                DateTime.Now.ToString("MM"),
                "/"
            });
        }


        #endregion


    }
}