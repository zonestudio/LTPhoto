using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace LTPhoto.Helpers
{
    public class Comm
    {
        public static Rectangle HorizRect;
        public static Rectangle VertRect;
        static Comm()
        {
            HorizRect = new Rectangle(66, 82, 513, 386);
            VertRect = new Rectangle(104, 84, 428, 470);
        }


        /// <summary> 
        /// 保存一个Cookie 
        /// </summary> 
        /// <param name="cookieName">Cookie名称</param> 
        /// <param name="cookieValue">Cookie值</param> 
        /// <param name="cookieTime">Cookie过期时间(小时),0为关闭页面失效</param> 
        public static void SaveCookie(string cookieName, string cookieValue, double cookieTime)
        {
            var myCookie = HttpContext.Current.Request.Cookies[cookieName] ?? new HttpCookie(cookieName);

            myCookie.Value = HttpUtility.UrlEncode(cookieValue, Encoding.GetEncoding("UTF-8"));
            myCookie.Expires = DateTime.Now.AddHours(cookieTime);
           // myCookie.Domain = Domain;
            myCookie.Path = "/";
            HttpContext.Current.Response.Cookies.Add(myCookie);

        }


        /// <summary> 
        /// 取得CookieValue 
        /// </summary> 
        /// <param name="cookieName">Cookie名称</param> 
        /// <returns>Cookie的值</returns> 
        public static string GetCookie(string cookieName)
        {
            var myCookie = HttpContext.Current.Request.Cookies[cookieName];
            return myCookie == null ? null : HttpUtility.UrlDecode(myCookie.Value, Encoding.GetEncoding("UTF-8"));

        }

        public static string ProcWaterImage(string lpSavePath)
        {
            //获取图片大小
            var img = Image.FromFile(lpSavePath);
            var W = img.Width;
            var H = img.Height;
            img.Dispose();
            var lpFileExt = Path.GetExtension(lpSavePath);//.png
            var lpTumbPath = lpSavePath.Replace(lpFileExt, $"_{Guid.NewGuid().ToString("N")}" + lpFileExt);
            var horiz = W > H;
            var procImage = String.Empty;
            if (horiz)
            {
                //测试横版图片

                //缩小，裁剪图片
                Imager.MakeThumbnail(lpSavePath, lpTumbPath,HorizRect.Width, HorizRect.Height,
                    "Cut", lpFileExt.Replace(".", ""), 0,
                    0);
                //dbFilePath = $"/uploads/{lpDateDir}/{UserPath}/{lpNewFileName}";
                File.Delete(lpSavePath);
                //叠加背景
                var HorizImage = HostingEnvironment.MapPath("~/background/p2.png"); //横版
                var tumbImage = Image.FromFile(lpTumbPath);
                procImage = Imager.CreateImageWaterMark(HorizImage, tumbImage, HorizRect);
                //删除缩略图
                tumbImage.Dispose();
                try
                {
                    File.Delete(lpTumbPath);
                }
                catch
                {
                }

            }
            else
            {
                //测试竖版图片

                //缩小，裁剪图片
                Imager.MakeThumbnail(lpSavePath, lpTumbPath, VertRect.Width, VertRect.Height,
                    "Cut", lpFileExt.Replace(".", ""), 0,
                    0);
                //dbFilePath = $"/uploads/{lpDateDir}/{UserPath}/{lpNewFileName}";
                File.Delete(lpSavePath);
                //叠加背景

                var VertImage = HostingEnvironment.MapPath("~/background/p3.png"); //竖版
                var tumbImage = Image.FromFile(lpTumbPath);
                procImage = Imager.CreateImageWaterMark(VertImage, Image.FromFile(lpTumbPath),
                    VertRect);
                //删除缩略图 
                tumbImage.Dispose();
                try
                {
                    File.Delete(lpTumbPath);
                }
                catch
                {
                }
            }
            return procImage;
        }
    }
}