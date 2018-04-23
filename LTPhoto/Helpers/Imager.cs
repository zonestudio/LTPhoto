using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace LTPhoto.Helpers
{
    public class Imager
    {
        public static void rotating(Bitmap img, ref int width, ref int height, int orien)
        {
            int ow = width;
            switch (orien)
            {
                case 2:
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);//horizontal flip
                    break;
                case 3:
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);//right-top
                    break;
                case 4:
                    img.RotateFlip(RotateFlipType.RotateNoneFlipY);//vertical flip
                    break;
                case 5:
                    img.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 6:
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);//right-top
                    width = height;
                    height = ow;
                    break;
                case 7:
                    img.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
                case 8:
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);//left-bottom
                    width = height;
                    height = ow;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 等比例缩放上传的图片
        /// </summary>
        /// <param name="httpPostedFile">上传文件对象</param>
        /// <param name="maxw">最大宽，不限制使用0</param>
        /// <param name="maxh">最大高，不限制使用0</param>
        /// <param name="filename">保存路径（文件夹/文件名）不需要后缀</param>
        public static string SaveAsThumbImage(HttpPostedFile httpPostedFile,int maxw,int maxh,string filename)
        {
            //生成原图
            Byte[] oFileByte = new byte[httpPostedFile.ContentLength];
            System.IO.Stream oStream = httpPostedFile.InputStream; //图像字节流
            System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream, false);

            int oWidth = oImage.Width; //原图宽度
            int oHeight = oImage.Height; //原图高度
            if (maxw > 0 && maxh == 0)
            {
                maxw = maxw > oWidth ? oWidth : maxw;
                maxh = (int)Math.Floor(Convert.ToDouble(oHeight) / Convert.ToDouble(oWidth) * Convert.ToDouble(maxw));
            }
            else if (maxh > 0 && maxw == 0)
            {
                maxh = maxh > oHeight ? oHeight : maxh;
                maxw = (int)Math.Floor(Convert.ToDouble(oWidth) / Convert.ToDouble(oHeight) * Convert.ToDouble(maxh));
            }

            //生成缩略原图
            Bitmap tImage = new Bitmap(maxw, maxh);
            Graphics g = Graphics.FromImage(tImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            //设置高质量插值法
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //设置高质量,低速度呈现平滑程度
            g.Clear(Color.Transparent); //清空画布并以透明背景色填充
            g.DrawImage(oImage, new Rectangle(0, 0, maxw, maxh),
                new Rectangle(0, 0, oWidth, oHeight), GraphicsUnit.Pixel);
            var filenameex = filename.Replace("/", "\\");
            string tFullName = HttpContext.Current.Server.MapPath("~/") + filenameex + ".jpg";
            //保存缩略图的物理路径
            //HttpContext.Current.Response.Write(tFullName);
            try
            {
                //检查目录是否存在
                var dic = Path.GetDirectoryName(tFullName);
                if (!Directory.Exists(dic))
                {
                    Directory.CreateDirectory(dic);
                }
                if (System.IO.File.Exists(tFullName))
                {
                    System.IO.File.Delete(tFullName);
                }

                tImage.Save(tFullName, System.Drawing.Imaging.ImageFormat.Jpeg);

                return filename + ".jpg";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //释放资源
                oImage.Dispose();
                g.Dispose();
                tImage.Dispose();
            }
        }

        /// <summary>  
        /// 根据源图片生成缩略图  
        /// </summary>  
        /// <param name="imgPathOld">源图(大图)物理路径</param>  
        /// <param name="imgPathNew">缩略图物理路径(生成的缩略图将保存到该物理位置)</param>  
        /// <param name="width">缩略图宽度</param>  
        /// <param name="height">缩略图高度</param>  
        /// <param name="mode">缩略图缩放模式(
        /// 取值"HW":指定高宽缩放,可能变形；
        /// 取值"W":按指定宽度,高度按比例缩放；
        /// 取值"H":按指定高度,宽度按比例缩放；
        /// 取值"Cut":按指定高度和宽度裁剪,不变形)；
        /// 取值"DB":等比缩放,以值较大的作为标准进行等比缩放
        /// </param>  
        /// <param name="imageType">即将生成缩略图的文件的扩展名(仅限：JPG、GIF、PNG、BMP)</param>  
        public static void MakeThumbnail(string imgPathOld,string imgPathNew,int width,int height,string mode,string imageType,int xx,int yy)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(imgPathOld);
            int towidth = width; int toheight = height;
            int x = 0; int y = 0; int ow = img.Width;
            int oh = img.Height; switch (mode)
            {
                case "HW":  //指定高宽压缩
                    if ((double)img.Width / (double)img.Height > (double)width / (double)height)//判断图形是什么形状
                    {
                        towidth = width;
                        toheight = img.Height * width / img.Width;
                    }
                    else if ((double)img.Width / (double)img.Height == (double)width / (double)height)
                    {
                        towidth = width;
                        toheight = height;
                    }
                    else
                    {
                        toheight = height;
                        towidth = img.Width * height / img.Height;
                    }
                    break;
                case "W":  //指定宽，高按比例   
                    toheight = img.Height * width / img.Width;
                    break;
                case "H":  //指定高，宽按比例  
                    towidth = img.Width * height / img.Height;
                    break;
                case "Cut":   //指定高宽裁减（不变形）   
                    if ((double)img.Width / (double)img.Height > (double)towidth / (double)toheight)
                    {
                        oh = img.Height;
                        ow = img.Height * towidth / toheight;
                        y = yy; x = (img.Width - ow) / 2;
                    }
                    else
                    {
                        ow = img.Width;
                        oh = img.Width * height / towidth;
                        x = xx; y = (img.Height - oh) / 2;
                    }
                    break;
                case "DB":    // 按值较大的进行等比缩放（不变形）   
                    if ((double)img.Width / (double)towidth < (double)img.Height / (double)toheight)
                    {
                        toheight = height;
                        towidth = img.Width * height / img.Height;
                    }
                    else
                    {
                        towidth = width;
                        toheight = img.Height * width / img.Width;
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片  
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板  
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //设置高质量,低速度呈现平滑程度  
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充  
            g.Clear(System.Drawing.Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分  
            g.DrawImage(img, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);
            try
            {

                //以jpg格式保存缩略图 
                switch (imageType.ToLower())
                {
                    case "gif":
                        img.Save(imgPathNew, ImageFormat.Jpeg);//生成缩略图
                        break;
                    case "jpg":
                        bitmap.Save(imgPathNew, ImageFormat.Jpeg);
                        break;
                    case "bmp":
                        bitmap.Save(imgPathNew, ImageFormat.Bmp);
                        break;
                    case "png":
                        bitmap.Save(imgPathNew, ImageFormat.Png);
                        break;
                    default:
                        bitmap.Save(imgPathNew, ImageFormat.Jpeg);
                        break;
                }
                ////保存缩略图  
                // bitmap.Save(imgPath_new);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                bitmap.Dispose();
                img.Dispose();
                bitmap.Dispose(); g.Dispose();
            }
        }
        /// <summary>
        /// 对一个指定的图片加上图片水印效果。
        /// </summary>
        /// <param name="imageFile">图片文件地址</param>
        /// <param name="waterImage">水印图片（Image对象）</param>
        public static string CreateImageWaterMark(string imageFile, System.Drawing.Image waterImage,Rectangle wateRectangle)
        {
            if (string.IsNullOrEmpty(imageFile) || !File.Exists(imageFile) || waterImage == null)
            {
                return string.Empty;
            }
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(imageFile);
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(originalImage.Width, originalImage.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);

            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;


            int width = waterImage.Width;
            int height = waterImage.Height;

            graphics.DrawImage(waterImage, wateRectangle, 0, 0, width, height, GraphicsUnit.Pixel);

            graphics.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height), 0, 0,
                originalImage.Width, originalImage.Height, GraphicsUnit.Pixel);

            graphics.Dispose();
            
            MemoryStream stream = new MemoryStream();
           
            bitmap.Save(stream, ImageFormat.Jpeg);
            bitmap.Dispose();
            originalImage.Dispose();


            System.Drawing.Image imageWithWater = System.Drawing.Image.FromStream(stream);
            
            var path = HostingEnvironment.MapPath("~/uploads");
            var lpDateDir = DateTime.Now.ToString("yyyyMMdd");
            var userDirectory = Path.Combine(path, lpDateDir);
            var file =  $"{Guid.NewGuid():N}.jpg";
            if (!Directory.Exists(userDirectory))
                Directory.CreateDirectory(userDirectory);

            SaveAndCompress(imageWithWater, userDirectory + "\\" + file, 80); //设定压缩比

            imageWithWater.Dispose();

            return $"/uploads/{lpDateDir}/{file}";
        }
        //将图片按百分比压缩，flag取值1到100，越小压缩比越大

        public static bool SaveAndCompress(Image iSource, string outPath, int flag)

        {


            ImageFormat tFormat = iSource.RawFormat;

            EncoderParameters ep = new EncoderParameters();

            long[] qy = new long[1];

            qy[0] = flag;

            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);

            ep.Param[0] = eParam;

            try

            {

                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageDecoders();

                ImageCodecInfo jpegICIinfo = null;

                for (int x = 0; x < arrayICI.Length; x++)

                {

                    if (arrayICI[x].FormatDescription.Equals("JPEG"))

                    {

                        jpegICIinfo = arrayICI[x];

                        break;

                    }

                }

                if (jpegICIinfo != null)

                    iSource.Save(outPath, jpegICIinfo, ep);

                else

                    iSource.Save(outPath, tFormat);

                return true;

            }

            catch

            {

                return false;

            }


        }
    }
}
