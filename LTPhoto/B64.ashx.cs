using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using LTPhoto.Helpers;
using Newtonsoft.Json;

namespace LTPhoto
{
    /// <summary>
    /// B64 的摘要说明
    /// </summary>
    public class B64 : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var strBase64 = context.Request.Form["base64Img"];
            var xm = context.Request.Form["xm"];
            var mobile = context.Request.Form["mobile"];
            strBase64 = Regex.Replace(strBase64, "data:image.*?,", ""); //砍掉 data:image/png;base64, 前缀
            byte[] arr = Convert.FromBase64String(strBase64);
            MemoryStream ms = new MemoryStream(arr);
            Bitmap bmp = new Bitmap(ms);

            var file_id = Guid.NewGuid().ToString("N");

            var fileName = file_id + ".png";
            var yy = DateTime.Now.ToString("yyMMdd");
            var savepath = PathHelper.MapPath("~/uploads/images/b64/" + yy);
            if (!Directory.Exists(savepath))
            {
                Directory.CreateDirectory(savepath);
            }
            savepath = PathHelper.Combine(savepath, fileName);
            bmp.Save(savepath, System.Drawing.Imaging.ImageFormat.Png);
            ms.Close();
            bmp.Dispose();
            var picture = Comm.ProcWaterImage(savepath);
            LtDataHelper.Save(xm, mobile, picture);
            Comm.SaveCookie("__uinfo", JsonConvert.SerializeObject(new {xm, mobile}), 2000);
            context.Response.Write(picture);
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