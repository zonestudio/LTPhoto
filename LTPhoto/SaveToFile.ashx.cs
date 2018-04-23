using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using LTPhoto.Helpers;

namespace LTPhoto
{
    /// <summary>
    /// SaveToFile 的摘要说明
    /// </summary>
    public class SaveToFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.Files.Count == 0) return;
            HttpPostedFile file = context.Request.Files[0];
            //创建随机文件名
            var lpFileName = file.FileName;
            var lpFileExt = Path.GetExtension(lpFileName);
            var lpNewFileName = $"{DateTime.Now:yyyyMMddHHmmssffff}{lpFileExt}";
            var lpOldFileName = $"{DateTime.Now:yyyyMMddHHmmssffff}_src{lpFileExt}";
            var path = HostingEnvironment.MapPath("~/uploads");
            var lpDateDir = DateTime.Now.ToString("yyyyMMdd");
            var UserDirectory = Path.Combine(path, lpDateDir); //所要创建文件夹的名字(年月日)
            // Thread.Sleep(1000 * 2);
            var lpDirectory = Path.Combine(UserDirectory, "images");
            if (!Directory.Exists(lpDirectory))
            {
                Directory.CreateDirectory(lpDirectory);
            }
           // var lpSavePath = Path.Combine(lpDirectory, lpNewFileName);
            var lpOldSavePath = Path.Combine(lpDirectory, lpOldFileName);
            var dbFilePath = "";
            file.SaveAs(lpOldSavePath); //保存图像原图
            
            context.Response.Write(Comm.ProcWaterImage(lpOldSavePath));

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}