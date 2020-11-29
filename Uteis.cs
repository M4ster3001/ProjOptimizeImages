using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace ImageTools
{
    public class Uteis
    {
        public void CompressAndSave(string file, int qualidade)
        {
            string fileName = Path.GetFileName(file);
            string extension = fileName.Split(".")[1].ToLower();
            Image img = Image.FromFile(file);

            if (extension == "jpeg" || extension == "png")
            {
                SaveJPEG(img, fileName, qualidade);
            }/*else if(extension == "png")
            {
                SavePNG(img, fileName);
            }*/
            else if(extension == "tiff")
            {
                SaveTIFF(img, fileName, Session.TypeCompression);
            }
        }

        public void ChecarDirectory(string path_destino)
        {
            if (!Directory.Exists((string)path_destino))
            {
                Directory.CreateDirectory((string)path_destino);
            }
        }

        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }

        //public System.Drawing.Bitmap ResizeImage(string path, string destino, int qualidade)
        //{
        //    try
        //    {
        //        System.Drawing.Bitmap original_image = new System.Drawing.Bitmap(path);
        //        decimal porcent = decimal.Parse(quality);

        //        int width = original_image.Width;
        //        int target_width = Convert.ToInt32(width * (porcent / 100));

        //        int height = original_image.Height;
        //        int target_height = Convert.ToInt32(height * (porcent / 100));

        //        int new_width, new_height;

        //        if (height <= target_height && width <= target_width)
        //        {
        //            new_height = height;
        //            new_width = width;
        //        }
        //        else
        //        {
        //            float target_ratio = (float)target_width / (float)target_height;
        //            float image_ratio = (float)width / (float)height;

        //            if (target_ratio > image_ratio)
        //            {
        //                new_height = target_height;
        //                new_width = (int)Math.Floor(image_ratio * (float)target_height);
        //            }
        //            else
        //            {
        //                new_height = (int)Math.Floor((float)target_width / image_ratio);
        //                new_width = target_width;
        //            }

        //            new_width = new_width > target_width ? target_width : new_width;
        //            new_height = new_height > target_height ? target_height : new_height;
        //        }

        //        System.Drawing.Bitmap final_image = new System.Drawing.Bitmap(new_width, new_height);
        //        System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(final_image);
        //        graphic.FillRectangle
        //            (
        //                new System.Drawing.SolidBrush(System.Drawing.Color.Transparent),
        //                new System.Drawing.Rectangle(0, 0, target_width, target_height)
        //            );

        //        int paste_x = (target_width - new_width) / 2;
        //        int paste_y = (target_height - new_height) / 2;

        //        graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
        //        graphic.DrawImage(original_image, 0, 0, new_width, new_height);

        //        if (graphic != null) graphic.Dispose();

        //        if (original_image != null) original_image.Dispose();

        //        return final_image;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static EncoderParameters CreateJPEGEncoder(int quality)
        {
            EncoderParameters encoderParams = new EncoderParameters(1);
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            encoderParams.Param[0] = qualityParam;

            return encoderParams;
        }

        public EncoderParameters CreateTIFFEncoder(EncoderValue CompressionType)
        {
            System.Drawing.Imaging.Encoder _Encoder = System.Drawing.Imaging.Encoder.Compression;

            EncoderParameters _EncoderParameteres = new EncoderParameters(1);
            EncoderParameter _EncoderParameter = new EncoderParameter(_Encoder, (long)CompressionType);

            _EncoderParameteres.Param[0] = _EncoderParameter;

            return _EncoderParameteres;
        }

        public void SaveJPEG(Image image, string fileName, int qualidade)
        {
            using Bitmap bmp = new Bitmap(image);

            ImageCodecInfo _imageCodeInfo = GetEncoderInfo("image/jpeg");
            EncoderParameters _encoderParameters = CreateJPEGEncoder(qualidade);

            bmp.Save(Session.Path_destino + "\\" + fileName, _imageCodeInfo, _encoderParameters);
        }

        public void SaveTIFF(Image image, string fileName, string TypeCompression)
        {
            ImageCodecInfo _imageCodeInfo = GetEncoderInfo("image/tiff");
            EncoderParameters _encoderParameters = CreateTIFFEncoder(FindTypeCompression(TypeCompression.ToUpper()));

            image.Save(Session.Path_destino + "\\" + fileName, _imageCodeInfo, _encoderParameters);

        }

        private EncoderValue FindTypeCompression(string typeCompression)
        {
            switch (typeCompression)
            {
                case "LZW":
                    return EncoderValue.CompressionLZW;
                case "CCITT3":
                    return EncoderValue.CompressionCCITT3;
                case "CCITT4":
                    return EncoderValue.CompressionCCITT4;
                case "Rle":
                    return EncoderValue.CompressionRle;
                default:
                    return EncoderValue.CompressionNone;

            }
        }
    }
}
