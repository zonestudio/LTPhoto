using System;
using System.Drawing;
using System.IO;

namespace LTPhoto.Helpers
{
    /// <summary>
    /// ImgWater 的摘要说明
    /// </summary>
    public class ImgWater
    {

        /// <summary>
        /// 图片水印
        /// </summary>
        /// <param name="ImgFile">原图文件地址</param>
        /// <param name="WaterImg">水印图片地址</param>
        /// <param name="sImgPath">水印图片保存地址</param>
        /// <param name="Alpha">水印透明度设置</param>
        /// <param name="iScale">水印图片在原图上的显示比例</param>
        /// <param name="intDistance">水印图片在原图上的边距确定,以图片的右边和下边为准,当设定的边距超过一定大小后参数会自动失效</param>
        public void zzsImgWater(
            string ImgFile
            , string WaterImg
            , string sImgPath
            , float Alpha
            , float iScale
            , int intDistance
        )
        {
            try
            {

                //装载图片


                FileStream fs = new FileStream(ImgFile, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                MemoryStream ms = new MemoryStream(bytes);

                System.Drawing.Image imgPhoto = System.Drawing.Image.FromStream(ms);



                //System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(ImgFile);



                int imgPhotoWidth = imgPhoto.Width;
                int imgPhotoHeight = imgPhoto.Height;

                System.Drawing.Image imgWatermark = new Bitmap(WaterImg);
                int imgWatermarkWidth = imgWatermark.Width;
                int imgWatermarkHeight = imgWatermark.Height;


                //计算水印图片尺寸

                decimal aScale = Convert.ToDecimal(iScale);
                decimal pScale = 0.05M;
                decimal MinScale = aScale - pScale;
                decimal MaxScale = aScale + pScale;

                int imgWatermarkWidthNew = imgWatermarkWidth;
                int imgWatermarkHeightNew = imgWatermarkHeight;

                if (imgPhotoWidth >= imgWatermarkWidth && imgPhotoHeight >= imgWatermarkHeight && imgPhotoWidth >= imgPhotoHeight)
                    if (imgWatermarkWidth > imgWatermarkHeight)

                        if ((MinScale <= Math.Round((Convert.ToDecimal(imgWatermarkWidth) / Convert.ToDecimal(imgPhotoWidth)), 7)) && (Math.Round((Convert.ToDecimal(imgWatermarkWidth) / Convert.ToDecimal(imgPhotoWidth)), 7) <= MaxScale))
                        {

                        }
                        else
                        {
                            imgWatermarkWidthNew = Convert.ToInt32(imgPhotoWidth * aScale);
                            imgWatermarkHeightNew = Convert.ToInt32((imgPhotoWidth * aScale / imgWatermarkWidth) * imgWatermarkHeight);
                        }
                    else
                    if ((MinScale <= Math.Round((Convert.ToDecimal(imgWatermarkHeight) / Convert.ToDecimal(imgPhotoHeight)), 7)) && (Math.Round((Convert.ToDecimal(imgWatermarkHeight) / Convert.ToDecimal(imgPhotoHeight)), 7) <= MaxScale))
                    {

                    }
                    else
                    {
                        imgWatermarkHeightNew = Convert.ToInt32(imgPhotoHeight * aScale);
                        imgWatermarkWidthNew = Convert.ToInt32((imgPhotoHeight * aScale / imgWatermarkHeight) * imgWatermarkWidth);
                    }
                if (imgWatermarkWidth >= imgPhotoWidth && imgWatermarkHeight >= imgPhotoHeight && imgWatermarkWidth >= imgWatermarkHeight)
                {
                    imgWatermarkWidthNew = Convert.ToInt32(imgPhotoWidth * aScale);
                    imgWatermarkHeightNew = Convert.ToInt32(((imgPhotoWidth * aScale) / imgWatermarkWidth) * imgWatermarkHeight);
                }

                if (imgWatermarkWidth >= imgPhotoWidth && imgWatermarkHeight <= imgPhotoHeight && imgPhotoWidth >= imgPhotoHeight)
                {
                    imgWatermarkWidthNew = Convert.ToInt32(imgPhotoWidth * aScale);
                    imgWatermarkHeightNew = Convert.ToInt32(((imgPhotoWidth * aScale) / imgWatermarkWidth) * imgWatermarkHeight);
                }

                if (imgWatermarkWidth <= imgPhotoWidth && imgWatermarkHeight >= imgPhotoHeight && imgPhotoWidth >= imgPhotoHeight)
                {
                    imgWatermarkHeightNew = Convert.ToInt32(imgPhotoHeight * aScale);
                    imgWatermarkWidthNew = Convert.ToInt32(((imgPhotoHeight * aScale) / imgWatermarkHeight) * imgWatermarkWidth);
                }

                if (imgPhotoWidth >= imgWatermarkWidth && imgPhotoHeight >= imgWatermarkHeight && imgPhotoWidth <= imgPhotoHeight)
                    if (imgWatermarkWidth > imgWatermarkHeight)
                        if ((MinScale <= Math.Round((Convert.ToDecimal(imgWatermarkWidth) / Convert.ToDecimal(imgPhotoWidth)), 7)) && (Math.Round((Convert.ToDecimal(imgWatermarkWidth) / Convert.ToDecimal(imgPhotoWidth)), 7) <= MaxScale))
                        {

                        }
                        else
                        {
                            imgWatermarkWidthNew = Convert.ToInt32(imgPhotoWidth * aScale);
                            imgWatermarkHeightNew = Convert.ToInt32(((imgPhotoWidth * aScale) / imgWatermarkWidth) * imgWatermarkHeight);
                        }
                    else
                    if ((MinScale <= Math.Round((Convert.ToDecimal(imgWatermarkHeight) / Convert.ToDecimal(imgPhotoHeight)), 7)) && (Math.Round((Convert.ToDecimal(imgWatermarkHeight) / Convert.ToDecimal(imgPhotoHeight)), 7) <= MaxScale))
                    {

                    }
                    else
                    {
                        imgWatermarkHeightNew = Convert.ToInt32(imgPhotoHeight * aScale);
                        imgWatermarkWidthNew = Convert.ToInt32(((imgPhotoHeight * aScale) / imgWatermarkHeight) * imgWatermarkWidth);
                    }

                if (imgWatermarkWidth >= imgPhotoWidth && imgWatermarkHeight >= imgPhotoHeight && imgWatermarkWidth <= imgWatermarkHeight)
                {
                    imgWatermarkHeightNew = Convert.ToInt32(imgPhotoHeight * aScale);
                    imgWatermarkWidthNew = Convert.ToInt32(((imgPhotoHeight * aScale) / imgWatermarkHeight) * imgWatermarkWidth);
                }

                if (imgWatermarkWidth >= imgPhotoWidth && imgWatermarkHeight <= imgPhotoHeight && imgPhotoWidth <= imgPhotoHeight)
                {
                    imgWatermarkWidthNew = Convert.ToInt32(imgPhotoWidth * aScale);
                    imgWatermarkHeightNew = Convert.ToInt32(((imgPhotoWidth * aScale) / imgWatermarkWidth) * imgWatermarkHeight);
                }

                if (imgWatermarkWidth <= imgPhotoWidth && imgWatermarkHeight >= imgPhotoHeight && imgPhotoWidth <= imgPhotoHeight)
                {
                    imgWatermarkHeightNew = Convert.ToInt32(imgPhotoHeight * aScale);
                    imgWatermarkWidthNew = Convert.ToInt32(((imgPhotoHeight * aScale) / imgWatermarkHeight) * imgWatermarkWidth);
                }


                //将原图画出来

                Bitmap bmPhoto = new Bitmap(imgPhotoWidth, imgPhotoHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(72, 72);
                Graphics gbmPhoto = Graphics.FromImage(bmPhoto);

                gbmPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gbmPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                gbmPhoto.Clear(Color.White);
                gbmPhoto.DrawImage(
                    imgPhoto
                    , new Rectangle(0, 0, imgPhotoWidth, imgPhotoHeight)
                    , 0
                    , 0
                    , imgPhotoWidth
                    , imgPhotoHeight
                    , GraphicsUnit.Pixel
                );



                Bitmap bmWatermark = new Bitmap(bmPhoto);
                bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

                Graphics gWatermark = Graphics.FromImage(bmWatermark);

                //指定高质量显示水印图片质量
                gWatermark.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gWatermark.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


                System.Drawing.Imaging.ImageAttributes imageAttributes = new System.Drawing.Imaging.ImageAttributes();

                //设置两种颜色,达到合成效果

                System.Drawing.Imaging.ColorMap colorMap = new System.Drawing.Imaging.ColorMap();
                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

                System.Drawing.Imaging.ColorMap[] remapTable = { colorMap };

                imageAttributes.SetRemapTable(remapTable, System.Drawing.Imaging.ColorAdjustType.Bitmap);

                //用矩阵设置水印图片透明度
                float[][] colorMatrixElements = {
                    new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                    new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                    new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                    new float[] {0.0f,  0.0f,  0.0f,  Alpha, 0.0f},
                    new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                };


                System.Drawing.Imaging.ColorMatrix wmColorMatrix = new System.Drawing.Imaging.ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(wmColorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);

                //确定水印边距
                int xPos = imgPhotoWidth - imgWatermarkWidthNew;
                int yPos = imgPhotoHeight - imgWatermarkHeightNew;
                int xPosOfWm = 0;
                int yPosOfWm = 0;

                if (xPos > intDistance)
                    xPosOfWm = xPos - intDistance;
                else
                    xPosOfWm = xPos;

                if (yPos > intDistance)
                    yPosOfWm = yPos - intDistance;
                else
                    yPosOfWm = yPos;

                gWatermark.DrawImage(
                    imgWatermark
                    , new Rectangle(xPosOfWm, yPosOfWm, imgWatermarkWidthNew, imgWatermarkHeightNew)
                    , 0
                    , 0
                    , imgWatermarkWidth
                    , imgWatermarkHeight
                    , GraphicsUnit.Pixel
                    , imageAttributes
                );

                imgPhoto = bmWatermark;



                //以jpg格式保存图片
                imgPhoto.Save(sImgPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                //销毁对象
                gbmPhoto.Dispose();
                gWatermark.Dispose();

                imgPhoto.Dispose();
                imgWatermark.Dispose();



            }
            catch (Exception ex)
            {
            }
        }


        /// <summary>
        /// 文字水印
        /// </summary>
        /// <param name="ImgFile">原图文件地址</param>
        /// <param name="TextFont">水印文字</param>
        /// <param name="sImgPath">文字水印图片保存地址</param>
        /// <param name="hScale">高度位置</param>
        /// <param name="widthFont">文字块在图片中所占宽度比例</param>
        /// <param name="Alpha">文字透明度 其数值的范围在0到255</param>

        public void zzsTextWater(
            string ImgFile
            , string TextFont
            , string sImgPath
            , float hScale
            , float widthFont
            , int Alpha
        )
        {
            try
            {

                FileStream fs = new FileStream(ImgFile, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                MemoryStream ms = new MemoryStream(bytes);

                System.Drawing.Image imgPhoto = System.Drawing.Image.FromStream(ms);



                //System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(ImgFile);

                int imgPhotoWidth = imgPhoto.Width;
                int imgPhotoHeight = imgPhoto.Height;

                Bitmap bmPhoto = new Bitmap(imgPhotoWidth, imgPhotoHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(72, 72);
                Graphics gbmPhoto = Graphics.FromImage(bmPhoto);

                gbmPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gbmPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                gbmPhoto.DrawImage(
                    imgPhoto
                    , new Rectangle(0, 0, imgPhotoWidth, imgPhotoHeight)
                    , 0
                    , 0
                    , imgPhotoWidth
                    , imgPhotoHeight
                    , GraphicsUnit.Pixel
                );

                //建立字体大小的数组,循环找出适合图片的水印字体

                int[] sizes = new int[] { 1000, 800, 700, 650, 600, 560, 540, 500, 450, 400, 380, 360, 340, 320, 300, 280, 260, 240, 220, 200, 180, 160, 140, 120, 100, 80, 72, 64, 48, 32, 28, 26, 24, 20, 28, 16, 14, 12, 10, 8, 6, 4, 2 };
                System.Drawing.Font crFont = null;
                System.Drawing.SizeF crSize = new SizeF();
                for (int i = 0; i < 43; i++)
                {
                    crFont = new Font("arial", sizes[i], FontStyle.Bold);
                    crSize = gbmPhoto.MeasureString(TextFont, crFont);

                    if ((ushort)crSize.Width < (ushort)imgPhotoWidth * widthFont)
                        break;
                }

                //设置水印字体的位置
                int yPixlesFromBottom = (int)(imgPhotoHeight * hScale);
                float yPosFromBottom = ((imgPhotoHeight - yPixlesFromBottom) - (crSize.Height / 2));
                float xCenterOfImg = (imgPhotoWidth * 1 / 2);

                //if (xCenterOfImg<crSize.Height)
                //    xCenterOfImg = (imgPhotoWidth * (1 - (crSize.Height)/ imgPhotoWidth));

                System.Drawing.StringFormat StrFormat = new System.Drawing.StringFormat();
                StrFormat.Alignment = System.Drawing.StringAlignment.Center;

                //
                System.Drawing.SolidBrush semiTransBrush2 = new System.Drawing.SolidBrush(Color.FromArgb(Alpha, 0, 0, 0));

                gbmPhoto.DrawString(
                    TextFont
                    , crFont
                    , semiTransBrush2
                    , new System.Drawing.PointF(xCenterOfImg + 1, yPosFromBottom + 1)
                    , StrFormat
                );

                System.Drawing.SolidBrush semiTransBrush = new System.Drawing.SolidBrush(Color.FromArgb(Alpha, 255, 255, 255));

                gbmPhoto.DrawString(
                    TextFont
                    , crFont
                    , semiTransBrush
                    , new System.Drawing.PointF(xCenterOfImg, yPosFromBottom)
                    , StrFormat
                );

                bmPhoto.Save(sImgPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                gbmPhoto.Dispose();
                imgPhoto.Dispose();
                bmPhoto.Dispose();
            }
            catch (Exception ex)
            {
            }
        }


        /// <summary>
        /// 文字和Logo图片水印
        /// </summary>
        /// <param name="ImgFile">原图文件地址</param>
        /// <param name="WaterImg">水印图片地址</param>
        /// <param name="TextFont">水印文字信息</param>
        /// <param name="sImgPath">生存水印图片后的保存地址</param>
        /// <param name="ImgAlpha">水印图片的透明度</param>
        /// <param name="imgiScale">水印图片在原图上的显示比例</param>
        /// <param name="intimgDistance">水印图片在原图上的边距确定,以图片的右边和下边为准,当设定的边距超过一定大小后参数会自动失效</param>
        /// <param name="texthScale">水印文字高度位置,从图片底部开始计算，0－1</param>
        /// <param name="textwidthFont">文字块在图片中所占宽度比例 0－1</param>
        /// <param name="textAlpha">文字透明度 其数值的范围在0到255</param>
        public void zzsImgTextWater(
            string ImgFile
            , string WaterImg
            , string TextFont
            , string sImgPath
            , float ImgAlpha
            , float imgiScale
            , int intimgDistance
            , float texthScale
            , float textwidthFont
            , int textAlpha
        )
        {
            try
            {

                FileStream fs = new FileStream(ImgFile, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                MemoryStream ms = new MemoryStream(bytes);

                System.Drawing.Image imgPhoto = System.Drawing.Image.FromStream(ms);



                //System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(ImgFile);


                int imgPhotoWidth = imgPhoto.Width;
                int imgPhotoHeight = imgPhoto.Height;

                Bitmap bmPhoto = new Bitmap(imgPhotoWidth, imgPhotoHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(72, 72);
                Graphics gbmPhoto = Graphics.FromImage(bmPhoto);

                gbmPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gbmPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                gbmPhoto.DrawImage(
                    imgPhoto
                    , new Rectangle(0, 0, imgPhotoWidth, imgPhotoHeight)
                    , 0
                    , 0
                    , imgPhotoWidth
                    , imgPhotoHeight
                    , GraphicsUnit.Pixel
                );


                //建立字体大小的数组,循环找出适合图片的水印字体

                int[] sizes = new int[] { 1000, 800, 700, 650, 600, 560, 540, 500, 450, 400, 380, 360, 340, 320, 300, 280, 260, 240, 220, 200, 180, 160, 140, 120, 100, 80, 72, 64, 48, 32, 28, 26, 24, 20, 28, 16, 14, 12, 10, 8, 6, 4, 2 };
                System.Drawing.Font crFont = null;
                System.Drawing.SizeF crSize = new SizeF();
                for (int i = 0; i < 43; i++)
                {
                    crFont = new Font("arial", sizes[i], FontStyle.Bold);
                    crSize = gbmPhoto.MeasureString(TextFont, crFont);

                    if ((ushort)crSize.Width < (ushort)imgPhotoWidth * textwidthFont)
                        break;
                }

                //设置水印字体的位置
                int yPixlesFromBottom = (int)(imgPhotoHeight * texthScale);
                float yPosFromBottom = ((imgPhotoHeight - yPixlesFromBottom) - (crSize.Height / 2));
                float xCenterOfImg = (imgPhotoWidth * 1 / 2);

                System.Drawing.StringFormat StrFormat = new System.Drawing.StringFormat();
                StrFormat.Alignment = System.Drawing.StringAlignment.Center;

                //
                System.Drawing.SolidBrush semiTransBrush2 = new System.Drawing.SolidBrush(Color.FromArgb(textAlpha, 0, 0, 0));

                gbmPhoto.DrawString(
                    TextFont
                    , crFont
                    , semiTransBrush2
                    , new System.Drawing.PointF(xCenterOfImg + 1, yPosFromBottom + 1)
                    , StrFormat
                );

                System.Drawing.SolidBrush semiTransBrush = new System.Drawing.SolidBrush(Color.FromArgb(textAlpha, 255, 255, 255));

                gbmPhoto.DrawString(
                    TextFont
                    , crFont
                    , semiTransBrush
                    , new System.Drawing.PointF(xCenterOfImg, yPosFromBottom)
                    , StrFormat
                );


                System.Drawing.Image imgWatermark = new Bitmap(WaterImg);
                int imgWatermarkWidth = imgWatermark.Width;
                int imgWatermarkHeight = imgWatermark.Height;


                //计算水印图片尺寸

                decimal aScale = Convert.ToDecimal(imgiScale);
                decimal pScale = 0.05M;
                decimal MinScale = aScale - pScale;
                decimal MaxScale = aScale + pScale;

                int imgWatermarkWidthNew = imgWatermarkWidth;
                int imgWatermarkHeightNew = imgWatermarkHeight;

                if (imgPhotoWidth >= imgWatermarkWidth && imgPhotoHeight >= imgWatermarkHeight && imgPhotoWidth >= imgPhotoHeight)
                    if (imgWatermarkWidth > imgWatermarkHeight)

                        if ((MinScale <= Math.Round((Convert.ToDecimal(imgWatermarkWidth) / Convert.ToDecimal(imgPhotoWidth)), 7)) && (Math.Round((Convert.ToDecimal(imgWatermarkWidth) / Convert.ToDecimal(imgPhotoWidth)), 7) <= MaxScale))
                        {

                        }
                        else
                        {
                            imgWatermarkWidthNew = Convert.ToInt32(imgPhotoWidth * aScale);
                            imgWatermarkHeightNew = Convert.ToInt32((imgPhotoWidth * aScale / imgWatermarkWidth) * imgWatermarkHeight);
                        }
                    else
                    if ((MinScale <= Math.Round((Convert.ToDecimal(imgWatermarkHeight) / Convert.ToDecimal(imgPhotoHeight)), 7)) && (Math.Round((Convert.ToDecimal(imgWatermarkHeight) / Convert.ToDecimal(imgPhotoHeight)), 7) <= MaxScale))
                    {

                    }
                    else
                    {
                        imgWatermarkHeightNew = Convert.ToInt32(imgPhotoHeight * aScale);
                        imgWatermarkWidthNew = Convert.ToInt32((imgPhotoHeight * aScale / imgWatermarkHeight) * imgWatermarkWidth);
                    }
                if (imgWatermarkWidth >= imgPhotoWidth && imgWatermarkHeight >= imgPhotoHeight && imgWatermarkWidth >= imgWatermarkHeight)
                {
                    imgWatermarkWidthNew = Convert.ToInt32(imgPhotoWidth * aScale);
                    imgWatermarkHeightNew = Convert.ToInt32(((imgPhotoWidth * aScale) / imgWatermarkWidth) * imgWatermarkHeight);
                }

                if (imgWatermarkWidth >= imgPhotoWidth && imgWatermarkHeight <= imgPhotoHeight && imgPhotoWidth >= imgPhotoHeight)
                {
                    imgWatermarkWidthNew = Convert.ToInt32(imgPhotoWidth * aScale);
                    imgWatermarkHeightNew = Convert.ToInt32(((imgPhotoWidth * aScale) / imgWatermarkWidth) * imgWatermarkHeight);
                }

                if (imgWatermarkWidth <= imgPhotoWidth && imgWatermarkHeight >= imgPhotoHeight && imgPhotoWidth >= imgPhotoHeight)
                {
                    imgWatermarkHeightNew = Convert.ToInt32(imgPhotoHeight * aScale);
                    imgWatermarkWidthNew = Convert.ToInt32(((imgPhotoHeight * aScale) / imgWatermarkHeight) * imgWatermarkWidth);
                }

                if (imgPhotoWidth >= imgWatermarkWidth && imgPhotoHeight >= imgWatermarkHeight && imgPhotoWidth <= imgPhotoHeight)
                    if (imgWatermarkWidth > imgWatermarkHeight)
                        if ((MinScale <= Math.Round((Convert.ToDecimal(imgWatermarkWidth) / Convert.ToDecimal(imgPhotoWidth)), 7)) && (Math.Round((Convert.ToDecimal(imgWatermarkWidth) / Convert.ToDecimal(imgPhotoWidth)), 7) <= MaxScale))
                        {

                        }
                        else
                        {
                            imgWatermarkWidthNew = Convert.ToInt32(imgPhotoWidth * aScale);
                            imgWatermarkHeightNew = Convert.ToInt32(((imgPhotoWidth * aScale) / imgWatermarkWidth) * imgWatermarkHeight);
                        }
                    else
                    if ((MinScale <= Math.Round((Convert.ToDecimal(imgWatermarkHeight) / Convert.ToDecimal(imgPhotoHeight)), 7)) && (Math.Round((Convert.ToDecimal(imgWatermarkHeight) / Convert.ToDecimal(imgPhotoHeight)), 7) <= MaxScale))
                    {

                    }
                    else
                    {
                        imgWatermarkHeightNew = Convert.ToInt32(imgPhotoHeight * aScale);
                        imgWatermarkWidthNew = Convert.ToInt32(((imgPhotoHeight * aScale) / imgWatermarkHeight) * imgWatermarkWidth);
                    }

                if (imgWatermarkWidth >= imgPhotoWidth && imgWatermarkHeight >= imgPhotoHeight && imgWatermarkWidth <= imgWatermarkHeight)
                {
                    imgWatermarkHeightNew = Convert.ToInt32(imgPhotoHeight * aScale);
                    imgWatermarkWidthNew = Convert.ToInt32(((imgPhotoHeight * aScale) / imgWatermarkHeight) * imgWatermarkWidth);
                }

                if (imgWatermarkWidth >= imgPhotoWidth && imgWatermarkHeight <= imgPhotoHeight && imgPhotoWidth <= imgPhotoHeight)
                {
                    imgWatermarkWidthNew = Convert.ToInt32(imgPhotoWidth * aScale);
                    imgWatermarkHeightNew = Convert.ToInt32(((imgPhotoWidth * aScale) / imgWatermarkWidth) * imgWatermarkHeight);
                }

                if (imgWatermarkWidth <= imgPhotoWidth && imgWatermarkHeight >= imgPhotoHeight && imgPhotoWidth <= imgPhotoHeight)
                {
                    imgWatermarkHeightNew = Convert.ToInt32(imgPhotoHeight * aScale);
                    imgWatermarkWidthNew = Convert.ToInt32(((imgPhotoHeight * aScale) / imgWatermarkHeight) * imgWatermarkWidth);
                }


                //将原图画出来


                Bitmap bmWatermark = new Bitmap(bmPhoto);
                bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

                Graphics gWatermark = Graphics.FromImage(bmWatermark);

                //指定高质量显示水印图片质量
                gWatermark.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gWatermark.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


                System.Drawing.Imaging.ImageAttributes imageAttributes = new System.Drawing.Imaging.ImageAttributes();

                //设置两种颜色,达到合成效果

                System.Drawing.Imaging.ColorMap colorMap = new System.Drawing.Imaging.ColorMap();
                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

                System.Drawing.Imaging.ColorMap[] remapTable = { colorMap };

                imageAttributes.SetRemapTable(remapTable, System.Drawing.Imaging.ColorAdjustType.Bitmap);

                //用矩阵设置水印图片透明度
                float[][] colorMatrixElements = {
                    new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                    new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                    new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                    new float[] {0.0f,  0.0f,  0.0f,  ImgAlpha, 0.0f},
                    new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                };


                System.Drawing.Imaging.ColorMatrix wmColorMatrix = new System.Drawing.Imaging.ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(wmColorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);

                //确定水印边距
                int xPos = imgPhotoWidth - imgWatermarkWidthNew;
                int yPos = imgPhotoHeight - imgWatermarkHeightNew;
                int xPosOfWm = 0;
                int yPosOfWm = 0;

                if (xPos > intimgDistance)
                    xPosOfWm = xPos - intimgDistance;
                else
                    xPosOfWm = xPos;

                if (yPos > intimgDistance)
                    yPosOfWm = yPos - intimgDistance;
                else
                    yPosOfWm = yPos;

                gWatermark.DrawImage(
                    imgWatermark
                    , new Rectangle(xPosOfWm, yPosOfWm, imgWatermarkWidthNew, imgWatermarkHeightNew)
                    , 0
                    , 0
                    , imgWatermarkWidth
                    , imgWatermarkHeight
                    , GraphicsUnit.Pixel
                    , imageAttributes
                );

                imgPhoto = bmWatermark;

                //以jpg格式保存图片
                imgPhoto.Save(sImgPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                //销毁对象
                gbmPhoto.Dispose();
                gWatermark.Dispose();
                bmPhoto.Dispose();
                imgPhoto.Dispose();
                imgWatermark.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 缩略图
        /// </summary>
        /// <param name="ImgFile">原图文件地址</param>
        /// <param name="sImgPath">缩略图保存地址</param>
        /// <param name="ResizeWidth">缩略图宽度</param>
        /// <param name="ResizeHeight">缩略图高度</param>
        /// <param name="BgColor">缩略图背景颜色,注意,背景颜色只能指定KnownColor中的值,如blue,red,green等</param>

        public void zzsResizeImg(
            string ImgFile
            , string sImgPath
            , int ResizeWidth
            , int ResizeHeight
            , string BgColor
        )
        {
            try
            {
                FileStream fs = new FileStream(ImgFile, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                MemoryStream ms = new MemoryStream(bytes);

                System.Drawing.Image imgPhoto = System.Drawing.Image.FromStream(ms);



                //System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(ImgFile);


                int imgPhotoWidth = imgPhoto.Width;
                int imgPhotoHeight = imgPhoto.Height;
                int startX = 0;
                int StartY = 0;
                int NewWidth = 0;
                int NewHeight = 0;

                if (imgPhotoWidth >= ResizeWidth && imgPhotoHeight >= ResizeHeight)
                {
                    NewWidth = ResizeWidth;
                    NewHeight = Convert.ToInt32(imgPhotoHeight * Math.Round(Convert.ToDecimal(ResizeWidth) / Convert.ToDecimal(imgPhotoWidth), 10));
                    startX = 0;
                    StartY = (ResizeHeight - NewHeight) / 2;
                }

                if (ResizeWidth > imgPhotoWidth && ResizeHeight < imgPhotoHeight)
                {
                    NewHeight = ResizeHeight;
                    NewWidth = Convert.ToInt32(imgPhotoWidth * Math.Round(Convert.ToDecimal(ResizeHeight) / Convert.ToDecimal(imgPhotoHeight), 10));
                    startX = (ResizeWidth - NewWidth) / 2;
                    StartY = 0;
                }

                if (ResizeWidth < imgPhotoWidth && ResizeHeight > imgPhotoHeight)
                {
                    NewWidth = ResizeWidth;
                    NewHeight = Convert.ToInt32(imgPhotoHeight * Math.Round(Convert.ToDecimal(ResizeWidth) / Convert.ToDecimal(imgPhotoWidth), 10));
                    startX = 0;
                    StartY = (ResizeHeight - NewHeight) / 2;
                }

                if (imgPhotoWidth < ResizeWidth && imgPhotoHeight < ResizeHeight)
                {
                    NewWidth = imgPhotoWidth;
                    NewHeight = imgPhotoHeight;
                    startX = (ResizeWidth - imgPhotoWidth) / 2;
                    StartY = (ResizeHeight - imgPhotoHeight) / 2;
                }

                //计算缩放图片尺寸



                Bitmap bmPhoto = new Bitmap(ResizeWidth, ResizeHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(72, 72);
                Graphics gbmPhoto = Graphics.FromImage(bmPhoto);
                gbmPhoto.Clear(Color.FromName(BgColor));
                gbmPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gbmPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                gbmPhoto.DrawImage(
                    imgPhoto
                    , new Rectangle(startX, StartY, NewWidth, NewHeight)
                    , new Rectangle(0, 0, imgPhotoWidth, imgPhotoHeight)
                    , GraphicsUnit.Pixel
                );
                bmPhoto.Save(sImgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                imgPhoto.Dispose();
                gbmPhoto.Dispose();
                bmPhoto.Dispose();
            }
            catch (Exception err)
            {
            }
        }

        /// <summary>
        /// 图片剪切
        /// </summary>
        /// <param name="ImgFile">原图文件地址</param>
        /// <param name="sImgPath">缩略图保存地址</param>
        /// <param name="PointX">剪切起始点 X坐标</param>
        /// <param name="PointY">剪切起始点 Y坐标</param>
        /// <param name="CutWidth">剪切宽度</param>
        /// <param name="CutHeight">剪切高度</param>

        public void zzsCutImg(
            string ImgFile
            , string sImgPath
            , int PointX
            , int PointY
            , int CutWidth
            , int CutHeight
        )
        {
            try
            {
                FileStream fs = new FileStream(ImgFile, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                MemoryStream ms = new MemoryStream(bytes);

                System.Drawing.Image imgPhoto = System.Drawing.Image.FromStream(ms);

                //System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(ImgFile);
                //此处只能用filestream，用 System.Drawing.Image则会报多过进程访问文件的错误，会锁定文件
                Bitmap bmPhoto = new Bitmap(CutWidth, CutHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(72, 72);
                Graphics gbmPhoto = Graphics.FromImage(bmPhoto);
                gbmPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gbmPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                gbmPhoto.DrawImage(
                    imgPhoto
                    , new Rectangle(0, 0, CutWidth, CutHeight)
                    , new Rectangle(PointX, PointY, CutHeight, CutHeight)
                    , GraphicsUnit.Pixel
                );
                bmPhoto.Save(sImgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                imgPhoto.Dispose();
                gbmPhoto.Dispose();
                bmPhoto.Dispose();
            }
            catch (Exception err)
            {
            }
        }
    }
}