using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using GIBS.Modules.FlexMLS.Components;
using DotNetNuke.Services.Exceptions;

namespace GIBS.Modules.FlexMLS
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {

        static int MaxSize;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();


            // if (!String.IsNullOrEmpty(context.Request.QueryString["MaxSize"]))
            if (context.Request.QueryString["MaxSize"] != null)
            {
                if (!String.IsNullOrEmpty(context.Request.QueryString["MaxSize"]))
                {
                    MaxSize = Int32.Parse(context.Request.QueryString["MaxSize"]);
                }
                else
                {
                    MaxSize = 80;
                }

            }
            else
            {
                MaxSize = 80;
            }


            if (!String.IsNullOrEmpty(context.Request.QueryString["MlsNumber"]))
            {
                int id = Int32.Parse(context.Request.QueryString["MlsNumber"]);

                // Now you have the id, do what you want with it, to get the right image
                // More than likely, just pass it to the method, that builds the image
                Image image = GetImage(id, MaxSize);

                // Of course set this to whatever your format is of the image
                context.Response.ContentType = "image/jpeg";
                // Save the image to the OutputStream
                image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            }
            else
            {
                context.Response.ContentType = "text/html";
                context.Response.Write("<p>No Picture</p>");
            }


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public bool ThumbnailCallback()
        {
            return true;
        }


        private Image GetImage(int MlsNumber, int MaxSize)
        {
            string checkImage = "http://mls.gibs.com/images/" + MlsNumber.ToString() + ".jpg";
            string checkImage_1 = "http://mls.gibs.com/images/" + MlsNumber.ToString() + "_1.jpg";

            if (UrlExists(checkImage.ToString()) == true)
            {

                //OKAY
                checkImage = checkImage.ToString();

            }
            else
            {
                if (UrlExists(checkImage_1.ToString()) == true)
                {
                    checkImage = checkImage_1.ToString();
                    //OKAY

                }
                else
                {
                    checkImage = "http://mls.gibs.com/images/NoImage.jpg";

                    ImageNeeded(MlsNumber.ToString());

                }


            }


            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(checkImage.ToString());
            MemoryStream ms = new MemoryStream(bytes);

            // create an image object, using the filename we just retrieved
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

            // Compute thumbnail size.
            Size thumbnailSize = GetThumbnailSize(image, MaxSize);
            //CalculateNewSize
            // Size thumbnailSize = CalculateNewSize(image, System.Drawing.Size(400), 400);

            // create the actual thumbnail image
            System.Drawing.Image thumbnailImage = image.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

            // make a memory stream to work with the image bytes
            MemoryStream imageStream = new MemoryStream();

            // put the image into the memory stream
            thumbnailImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //thumbnailImage.Save(imageStream, System.Drawing.Imaging.Imageformat.Jpeg);

            // make byte array the same size as the image
            byte[] imageContent = new Byte[imageStream.Length];

            // rewind the memory stream
            imageStream.Position = 0;

            // load the byte array with the image
            imageStream.Read(imageContent, 0, (int)imageStream.Length);
            return Image.FromStream(imageStream);
            // return byte array to caller with image type

        }

        public void ImageNeeded(string listingNumber)
        {


            FlexMLSController controller = new FlexMLSController();
            FlexMLSInfo item = new FlexMLSInfo();

            item.ListingNumber = listingNumber;

            controller.FlexMLS_ImagesNeeded_Insert(item);


        }


        static Size GetThumbnailSize(Image original, int maxPixels)
        {
            // Maximum size of any dimension.
            //  const int maxPixels = 80;

            // Width and height.
            int originalWidth = original.Width;
            int originalHeight = original.Height;

            // Compute best factor to scale entire image based on larger dimension.
            float factor;
            if (originalWidth > originalHeight)
            {
                factor = (float)maxPixels / originalWidth;
                //  factor = GCD(originalWidth, originalHeight);
            }
            else
            {
                factor = (float)maxPixels / originalHeight;
                //  factor = GCD(originalWidth, originalHeight);
            }


            // Return thumbnail size.
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));



        }


        static Size CalculateNewSize(Image img, Size newSize, Size max)
        {
            float nPercent = 1; float nPercentW = 1; float nPercentH = 1;

            // STEP 1. RESIZE TO FIX SIZES
            if (newSize.Width > 0 && newSize.Height > 0)
            {
                // The smaller gets priority:
                if (img.Width != newSize.Width) nPercentW = ((float)newSize.Width / (float)img.Width);
                if (img.Height != newSize.Height) nPercentH = ((float)newSize.Height / (float)img.Height);
                if (nPercentH < nPercentW) { nPercent = nPercentH; } else { nPercent = nPercentW; }
            }
            else if (newSize.Width > 0)
            {
                nPercent = ((float)newSize.Width / (float)img.Width);
            }
            else if (newSize.Height > 0)
            {
                nPercent = ((float)newSize.Height / (float)img.Height);
            }

            // Calculate new sizes:
            int resWidth = (int)(img.Width * nPercent);
            int resHeight = (int)(img.Height * nPercent);

            // ==================================
            // STEP 2. CHECK THE LIMITS
            if (max.Width > 0 && resWidth > max.Width)
            {
                nPercent = ((float)max.Width / (float)resWidth);
                resWidth = (int)(resWidth * nPercent);
                resHeight = (int)(resHeight * nPercent);
            }
            if (max.Height > 0 && resHeight > max.Height)
            {
                nPercent = ((float)max.Height / (float)resHeight);
                resWidth = (int)(resWidth * nPercent);
                resHeight = (int)(resHeight * nPercent);
            }

            return new Size(resWidth, resHeight);
        }


        static int GCD(int a, int b)
        {
            int Remainder;

            while (b != 0)
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        }

        //return string.Format("{0}:{1}",x/GCD(x,y), y/GCD(x,y));

        private static bool UrlExists(string url)
        {
            try
            {
                new System.Net.WebClient().DownloadData(url);
                return true;
            }
            catch (System.Net.WebException e)
            {
                if (((System.Net.HttpWebResponse)e.Response).StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
                else
                    throw;
            }
        }


    }
}