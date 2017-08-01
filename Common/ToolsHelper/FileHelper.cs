using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;


namespace Common
{
    public class FileHelper
    {
        /// <summary>
        /// 获取目录下所有文件（包含子目录）
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>

        public static DataTable GetAllFileTable(string Path)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("ext", typeof(string));
            dt.Columns.Add("size", typeof(string));
            dt.Columns.Add("icon", typeof(string));
            dt.Columns.Add("isfolder", typeof(bool));
            dt.Columns.Add("isImage", typeof(bool));
            dt.Columns.Add("fullname", typeof(string));
            dt.Columns.Add("path", typeof(string));
            dt.Columns.Add("time", typeof(DateTime));

            string[] folders = Directory.GetDirectories(Path, "*", SearchOption.AllDirectories);

            List<string> Listfloders = new List<string>() { Path };

            if (folders != null && folders.Count() > 0)
            {
                foreach (var folder in folders)
                {
                    Listfloders.Add(folder);
                }
            }

            foreach (var f in Listfloders)
            {
                DirectoryInfo dirinfo = new DirectoryInfo(f);
                FileInfo fi;
                string FileName = string.Empty, FileExt = string.Empty, FileSize = string.Empty, FileIcon = string.Empty, FileFullName = string.Empty, FilePath = string.Empty;
                bool IsFloder = false, IsImage = false;
                DateTime FileModify;
                try
                {
                    foreach (FileSystemInfo fsi in dirinfo.GetFiles())
                    {

                        fi = (FileInfo)fsi;
                        //获取文件名称
                        FileName = fi.Name.Substring(0, fi.Name.LastIndexOf('.'));
                        FileFullName = fi.Name;
                        //获取文件扩展名
                        FileExt = fi.Extension.ToLower();
                        //获取文件大小
                        FileSize = GetDiyFileSize(fi);
                        //获取文件最后修改时间
                        FileModify = fi.LastWriteTime;
                        //文件图标
                        FileIcon = GetFileIcon(FileExt);
                        //是否为图片
                        IsImage = IsImageFile(FileExt.Substring(1, FileExt.Length - 1));
                        //文件路径
                        FilePath = urlconvertor(fi.FullName);

                        DataRow dr = dt.NewRow();
                        dr["name"] = FileName;
                        dr["fullname"] = FileFullName;
                        dr["ext"] = FileExt;
                        dr["size"] = FileSize;
                        dr["time"] = FileModify;
                        dr["icon"] = FileIcon;
                        dr["isfolder"] = IsFloder;
                        dr["isImage"] = IsImage;
                        dr["path"] = FilePath;
                        dt.Rows.Add(dr);
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }

            return dt;
        }


        /// <summary>
        /// 读取指定位置文件列表到集合中
        /// </summary>
        /// <param name="Path">指定路径</param>
        /// <returns></returns>
        public static DataTable GetFileTable(string Path)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("ext", typeof(string));
            dt.Columns.Add("size", typeof(string));
            dt.Columns.Add("icon", typeof(string));
            dt.Columns.Add("isfolder", typeof(bool));
            dt.Columns.Add("isImage", typeof(bool));
            dt.Columns.Add("fullname", typeof(string));
            dt.Columns.Add("path", typeof(string));
            dt.Columns.Add("time", typeof(DateTime));

            DirectoryInfo dirinfo = new DirectoryInfo(Path);
            FileInfo fi;
            DirectoryInfo dir;
            string FileName = string.Empty, FileExt = string.Empty, FileSize = string.Empty, FileIcon = string.Empty, FileFullName = string.Empty, FilePath = string.Empty;
            bool IsFloder = false, IsImage = false;
            DateTime FileModify;
            try
            {
                foreach (FileSystemInfo fsi in dirinfo.GetFileSystemInfos())
                {
                    if (fsi is FileInfo)
                    {
                        fi = (FileInfo)fsi;
                        //获取文件名称
                        FileName = fi.Name.Substring(0, fi.Name.LastIndexOf('.'));
                        FileFullName = fi.Name;
                        //获取文件扩展名
                        FileExt = fi.Extension;
                        //获取文件大小
                        FileSize = GetDiyFileSize(fi);
                        //获取文件最后修改时间
                        FileModify = fi.LastWriteTime;
                        //文件图标
                        FileIcon = GetFileIcon(FileExt);
                        //是否为图片
                        IsImage = IsImageFile(FileExt.Substring(1, FileExt.Length - 1));
                        //文件路径
                        FilePath = urlconvertor(fi.FullName);
                    }
                    else
                    {
                        dir = (DirectoryInfo)fsi;
                        //获取目录名
                        FileName = dir.Name;
                        //获取目录最后修改时间
                        FileModify = dir.LastWriteTime;
                        //设置目录文件为文件夹
                        FileExt = "folder";
                        //文件夹图标
                        FileIcon = "fa fa-folder";
                        IsFloder = true;
                        //文件路径
                        FilePath = urlconvertor(dir.FullName);

                    }
                    DataRow dr = dt.NewRow();
                    dr["name"] = FileName;
                    dr["fullname"] = FileFullName;
                    dr["ext"] = FileExt;
                    dr["size"] = FileSize;
                    dr["time"] = FileModify;
                    dr["icon"] = FileIcon;
                    dr["isfolder"] = IsFloder;
                    dr["isImage"] = IsImage;
                    dr["path"] = FilePath;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return dt;
        }

        public static bool IsExistDirectory(string Path)
        {
            return Directory.Exists(Path);
        }

        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        public static void CreateFolder(string FolderPath)
        {
            if (!FileHelper.IsExistDirectory(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
        }

        public static bool IsCanEdit(string strExtension)
        {
            strExtension = strExtension.ToLower();
            if (strExtension.LastIndexOf(".") >= 0)
            {
                strExtension = strExtension.Substring(strExtension.LastIndexOf("."));
            }
            else
            {
                strExtension = ".txt";
            }
            string[] array = new string[]
            {
                ".htm",
                ".html",
                ".txt",
                ".js",
                ".css",
                ".xml",
                ".sitemap"
            };
            for (int i = 0; i < array.Length; i++)
            {
                if (strExtension.Equals(array[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsSafeName(string strExtension)
        {
            strExtension = strExtension.ToLower();
            if (strExtension.LastIndexOf(".") >= 0)
            {
                strExtension = strExtension.Substring(strExtension.LastIndexOf("."));
            }
            else
            {
                strExtension = ".txt";
            }
            string[] array = new string[]
            {
                ".jpg",
                ".gif",
                ".png"
            };
            for (int i = 0; i < array.Length; i++)
            {
                if (strExtension.Equals(array[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsZipName(string strExtension)
        {
            strExtension = strExtension.ToLower();
            if (strExtension.LastIndexOf(".") >= 0)
            {
                strExtension = strExtension.Substring(strExtension.LastIndexOf("."));
            }
            else
            {
                strExtension = ".txt";
            }
            string[] array = new string[]
            {
                ".zip",
                ".rar"
            };
            for (int i = 0; i < array.Length; i++)
            {
                if (strExtension.Equals(array[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static void CreateSuffic(string filename)
        {
            try
            {
                if (!Directory.Exists(filename))
                {
                    Directory.CreateDirectory(filename);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CreateFiles(string fileName)
        {
            try
            {
                if (!IsExistFile(fileName))
                {
                    FileInfo fileInfo = new FileInfo(fileName);
                    FileStream fileStream = fileInfo.Create();
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CreateFile(string filePath, byte[] buffer)
        {
            try
            {
                if (!IsExistFile(filePath))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    FileStream fileStream = fileInfo.Create();
                    fileStream.Write(buffer, 0, buffer.Length);
                    fileStream.Close();
                }
                else
                {
                    File.WriteAllBytes(filePath, buffer);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Move(string sourceFilePath, string descDirectoryPath)
        {
            string fileName = GetFileName(sourceFilePath);
            if (IsExistDirectory(descDirectoryPath))
            {
                if (IsExistFile(descDirectoryPath + "\\" + fileName))
                {
                    DeleteFile(descDirectoryPath + "\\" + fileName);
                    return;
                }
                File.Move(sourceFilePath, descDirectoryPath + "\\" + fileName);
            }
        }

        public static void Copy(string sourceFilePath, string descDirectoryPath)
        {
            File.Copy(sourceFilePath, descDirectoryPath, true);
        }

        public static string GetFileName(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Name;
        }

        public static string GetExtension(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Extension;
        }

        public static void DeleteFile(string filePath)
        {
            if (IsExistFile(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static void DeleteDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath))
            {
                Directory.Delete(directoryPath);
            }
        }

        public static void ClearDirectory(string directoryPath)
        {
            if (FileHelper.IsExistDirectory(directoryPath))
            {
                string[] fileNames = GetFileNames(directoryPath);
                for (int i = 0; i < fileNames.Length; i++)
                {
                    FileHelper.DeleteFile(fileNames[i]);
                }
                string[] directories = FileHelper.GetDirectories(directoryPath);
                for (int j = 0; j < directories.Length; j++)
                {
                    FileHelper.DeleteDirectory(directories[j]);
                }
            }
        }

        public bool FileMove(string source, string destination)
        {
            bool result = false;
            FileInfo fileInfo = new FileInfo(source);
            FileInfo fileInfo2 = new FileInfo(destination);
            if (fileInfo.Exists && !fileInfo2.Exists)
            {
                fileInfo.MoveTo(destination);
                result = true;
            }
            return result;
        }

        public static bool IsEmptyDirectory(string directoryPath)
        {
            bool result;
            try
            {
                string[] fileNames = GetFileNames(directoryPath);
                if (fileNames.Length > 0)
                {
                    result = false;
                }
                else
                {
                    string[] directories = FileHelper.GetDirectories(directoryPath);
                    if (directories.Length > 0)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            catch
            {
                result = true;
            }
            return result;
        }

        public static string[] GetFileNames(string directoryPath)
        {
            if (!FileHelper.IsExistDirectory(directoryPath))
            {
                throw new System.IO.FileNotFoundException();
            }
            return Directory.GetFiles(directoryPath);
        }

        public static string[] GetDirectories(string directoryPath)
        {
            string[] directories;
            try
            {
                directories = Directory.GetDirectories(directoryPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return directories;
        }

        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            string[] directories;
            try
            {
                if (isSearchChild)
                {
                    directories = Directory.GetDirectories(directoryPath, searchPattern, System.IO.SearchOption.AllDirectories);
                }
                else
                {
                    directories = Directory.GetDirectories(directoryPath, searchPattern, System.IO.SearchOption.TopDirectoryOnly);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return directories;
        }

        public static string GetDiyFileSize(FileInfo fi)
        {
            string result = string.Empty;
            if (fi.Length < 1024L)
            {
                result = fi.Length.ToString() + "byte";
            }
            else if (fi.Length > 1024L && fi.Length < 1048576L)
            {
                result = Math.Round(FileHelper.GetFileSizeByKB(fi.Length), 2).ToString() + "KB";
            }
            else if (fi.Length > 1048576L && fi.Length < 1073741824L)
            {
                result = Math.Round(GetFileSizeByMB(fi.Length), 2).ToString() + "MB";
            }
            else
            {
                result = Math.Round(FileHelper.GetFileSizeByGB(fi.Length), 2).ToString() + "GB";
            }
            return result;
        }

        public static decimal GetDiyFileSize(long Length, out string unit)
        {
            unit = string.Empty;
            decimal result;
            if (Length < 1024L)
            {
                result = Length;
                unit = "byte";
            }
            else if (Length > 1024L && Length < 1048576L)
            {
                result = Math.Round(GetFileSizeByKB(Length), 2);
                unit = "KB";
            }
            else if (Length > 1048576L && Length < 1073741824L)
            {
                result = Math.Round(GetFileSizeByMB(Length), 2);
                unit = "MB";
            }
            else
            {
                result = Math.Round(GetFileSizeByGB(Length), 2);
                unit = "GB";
            }
            return result;
        }

        public static long GetFileSize(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }

        public static decimal GetFileSizeByKB(long filelength)
        {
            return System.Convert.ToDecimal(filelength) / 1024m;
        }

        public static decimal GetFileSizeByMB(long filelength)
        {
            return System.Convert.ToDecimal(System.Convert.ToDecimal(filelength / 1024L) / 1024m);
        }

        public static decimal GetFileSizeByGB(long filelength)
        {
            return System.Convert.ToDecimal(System.Convert.ToDecimal(System.Convert.ToDecimal(filelength / 1024L) / 1024m) / 1024m);
        }

        private static string urlconvertor(string Url)
        {
            string oldValue = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath.ToString());
            string text = Url.Replace(oldValue, "");
            text = text.Replace("\\", "/");
            return "/" + text;
        }

        public static string GetFileIcon(string _fileExt)
        {
            List<string> list = (from p in ConfigurationManager.AppSettings["Image"].Trim(new char[]
            {
                ','
            }).Split(new string[]
            {
                ","
            }, StringSplitOptions.RemoveEmptyEntries)
                                 select p).ToList<string>();
            List<string> list2 = (from p in ConfigurationManager.AppSettings["Video"].Trim(new char[]
            {
                ','
            }).Split(new string[]
            {
                ","
            }, StringSplitOptions.RemoveEmptyEntries)
                                  select p).ToList<string>();
            List<string> list3 = (from p in ConfigurationManager.AppSettings["Music"].Trim(new char[]
            {
                ','
            }).Split(new string[]
            {
                ","
            }, StringSplitOptions.RemoveEmptyEntries)
                                  select p).ToList<string>();
            if (list.Contains(_fileExt.ToLower().Remove(0, 1)))
            {
                return "fa fa-image";
            }
            if (list2.Contains(_fileExt.ToLower().Remove(0, 1)))
            {
                return "fa fa-film";
            }
            if (list3.Contains(_fileExt.ToLower().Remove(0, 1)))
            {
                return "fa fa-music";
            }
            string key;
            switch (key = _fileExt.ToLower())
            {
                case ".doc":
                case ".docx":
                    return "fa fa-file-word-o";
                case ".xls":
                case ".xlsx":
                    return "fa fa-file-excel-o";
                case ".ppt":
                case ".pptx":
                    return "fa fa-file-powerpoint-o";
                case ".pdf":
                    return "fa fa-file-pdf-o";
                case ".txt":
                    return "fa fa-file-text-o";
                case ".zip":
                case ".rar":
                    return "fa fa-file-zip-o";
            }
            return "fa fa-file";
        }

        private static bool IsImageFile(string _fileExt)
        {
            List<string> list = (from p in ConfigurationManager.AppSettings["Image"].Trim(new char[]
            {
                ','
            }).Split(new string[]
            {
                ","
            }, StringSplitOptions.RemoveEmptyEntries)
                                 select p).ToList<string>();
            return list.Contains(_fileExt.ToLower());
        }
    }
}
