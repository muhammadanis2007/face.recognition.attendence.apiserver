using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;



namespace FaceRecognition.Library.Utililties
{
    class FileHelper
    {
        public Bitmap Bitmap { get; set; }
        
  public static void Save(Image<Bgr, Byte> img, string filename, double quality)
        {
            
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(
                System.Drawing.Imaging.Encoder.Quality,
                (long)quality
                );

            var jpegCodec = (from codec in ImageCodecInfo.GetImageEncoders()
                             where codec.MimeType == "image/jpeg"
                             select codec).Single();

           
           // img.Save(filename, ImageFormat.Jpeg, encoderParams);
        }

  public static void Save(Image<Gray, Byte> img, string filename, double quality)
           {
               var encoderParams = new EncoderParameters(1);
               encoderParams.Param[0] = new EncoderParameter(
                   System.Drawing.Imaging.Encoder.Quality,
                   (long)quality
                   );

               var jpegCodec = (from codec in ImageCodecInfo.GetImageEncoders()
                                where codec.MimeType == "image/jpeg"
                                select codec).Single();

              // img.Save(filename, jpegCodec, encoderParams);
           }

      


        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality

            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = this.getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }



        public static string ImageExtensionFromImageFormat(ImageFormat imageFormat)
        {
            if (ImageFormat.Jpeg.Equals(imageFormat))
            {
                return ".jpg";
            }
            else if (ImageFormat.Gif.Equals(imageFormat))
            {
                return ".gif";
            }
            else if (ImageFormat.Png.Equals(imageFormat))
            {
                return ".png";
            }
            else if (ImageFormat.Bmp.Equals(imageFormat))
            {
                return ".bmp";
            }

            return ".jpg";
        }
    }
}
