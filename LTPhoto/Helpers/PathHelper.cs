using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace LTPhoto.Helpers
{
    public static class PathHelper
    {
        /// <summary>
        /// 本地路径转换成URL相对路径
        /// </summary>
        /// <param name="absolutepath">绝对路径</param>
        /// <param name="appfullpath">程序根目录</param>
        /// <returns>返回URL相对路径</returns>
        public static string ConvertToRelativePath(string absolutepath,string appfullpath,string relroot="")
        {
            string relativeroot = absolutepath.ToLower().Replace(appfullpath.ToLower(), ""); //转换成相对路径
            relativeroot = relativeroot.Replace(@"\", @"/");
            return relroot+relativeroot;
        }
        /// <summary>
        /// 相对路径转换成服务器本地物理路径
        /// </summary>
        /// <param name="relativepath"></param>
        /// <returns></returns>
        public static string ConvertToAbsolutePath(string relativepath)
        {
          
            return MapPath(relativepath); //转换成绝对路径
        }
        /// <summary>
        /// 组合路径
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string Combine(params string[] paths)
        {
            if (paths.Length == 0)
            {
                throw new ArgumentException("please input path");
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                string spliter = "\\";

                string firstPath = paths[0];

                if (firstPath.StartsWith("HTTP", StringComparison.OrdinalIgnoreCase))
                {
                    spliter = "/";
                }

                if (!firstPath.EndsWith(spliter))
                {
                    firstPath = firstPath + spliter;
                }
                builder.Append(firstPath);

                for (int i = 1; i < paths.Length; i++)
                {
                    string nextPath = paths[i];
                    if (nextPath.StartsWith("/") || nextPath.StartsWith("\\"))
                    {
                        nextPath = nextPath.Substring(1);
                    }

                    if (i != paths.Length - 1)//not the last one
                    {
                        if (nextPath.EndsWith("/") || nextPath.EndsWith("\\"))
                        {
                            nextPath = nextPath.Substring(0, nextPath.Length - 1) + spliter;
                        }
                        else
                        {
                            nextPath = nextPath + spliter;
                        }
                    }

                    builder.Append(nextPath);
                }

                return builder.ToString().Replace("/","\\");
            }
        }
        /// <summary>
        /// 多线程下MapPath
        /// </summary>
        /// <param name="strPath">相对路径</param>
        /// <returns></returns>
        public static string MapPath(string strPath)
        {
            if (string.IsNullOrEmpty(strPath))
            {
                return strPath;
            }
            if (strPath.IndexOf(":") != -1) //已是绝对路径
            {
                return strPath;
            }

            if (strPath.StartsWith("../")) //使用父目录的相对路径转绝对路径 注意，只支持../开头
            {
           
                var reg = new Regex(@"\.\./");
                var count =reg.Matches(strPath).Count;
                var rootDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);//当前根目录
    
                for (var i = 0; i < count; i++)
                {
                    rootDir = rootDir.Parent;
                }

                var lp = reg.Replace(strPath, "").Replace("/", "\\");

                return Path.Combine(rootDir.FullName, lp);
            }
            if (System.Web.HttpContext.Current != null)
            {
                return System.Web.HttpContext.Current.Server.MapPath(strPath);
            }
            //strPath = strPath.Replace("\\", "/");
            strPath = strPath.Replace("~", "");
            if (strPath.StartsWith("//"))
            {
                strPath = strPath.TrimStart('/').TrimStart('/').Replace('/','\\');
            }
            return Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }

        /// <summary>
        /// 是否是物理路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsPhyicPath(string path)
        {
            return Regex.IsMatch(path, @"\b[a-z]:\\.*", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
        }
     
     
    }
}
