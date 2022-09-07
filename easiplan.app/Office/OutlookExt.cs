using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Finx.App.Office
{
    public static class MailItemExtension
    {
        public static string FormatHtmlBody(this Microsoft.Office.Interop.Outlook.MailItem mailItem,string htmlBody)
        {

            try
            {
                //Parse the HTML body using AngleSharp

                HtmlParser htmlParser = new HtmlParser();
                IHtmlDocument htmlDocument = htmlParser.Parse(htmlBody);

                //Process Inline Images
                IList<LinkedResource> _linkedResources = new List<LinkedResource>();

                int imgNo = 0;

                foreach (var node in htmlDocument.All.Where(m => m.LocalName.ToLower() == "img"))
                {
                    imgNo++;

                    string imgSource = node.Attributes["src"].Value;

                    string contentid =string.Format("image{0}.bmp@{1}",imgNo, Guid.NewGuid().ToString().Replace("-", "."));
                    node.Attributes["src"].Value = string.Format("cid:{0}", contentid);

                    // node.Attributes["alt"].Value = string.Format("{0}", imgSource);
                    //node.Attributes["style"].Value = string.Format("width:{0};height:{1}", 1024,768);

                    Byte[] bitmapData = Convert.FromBase64String(FixBase64ForImage(imgSource));
                    string imgPath = string.Format("{0}{1}",Path.Combine(Path.GetTempPath()), string.Format("imgNo-{0}.png",imgNo));
                    File.WriteAllBytes(imgPath, bitmapData);

                        
                    #region using Interop

                    Microsoft.Office.Interop.Outlook.Attachment attc = mailItem.Attachments.Add(imgPath, Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, null,null);//,, null, null
                    
                    //The property Accessor to be able to refer to the image from Email HTML Body using CID
                    attc.PropertyAccessor.SetProperty("http://schemas.microsoft.com/mapi/proptag/0x3712001E", contentid);
                    //Set property Accesor to hide attachment
                    attc.PropertyAccessor.SetProperty("http://schemas.microsoft.com/mapi/proptag/0x7FFE000B", true);

                    attc.PropertyAccessor.SetProperty("http://schemas.microsoft.com/mapi/proptag/0x370E001F", "image/png");

                    mailItem.PropertyAccessor.SetProperty("http://schemas.microsoft.com/mapi/id/{00062008-0000-0000-C000-000000000046}/8514000B", true);

                    #endregion

                    #region using LinkedResources AlternateViews
                    byte[] imgArray = null;
                    //if (imgSource.StartsWith("http://"))
                    //{
                    //    imgArray = FileUtils.GetFileContentsFromUrl(imgSource);
                    //}
                    //else
                    //{ // image exists in emailTemplatePath
                    //    string imagesPath = Path.Combine(emailTemplatePath.Substring(0, emailTemplatePath.LastIndexOf("/")), imgSource);
                    //    imgArray = FileUtils.GetFileContentsFromUrl(imagesPath);
                    //}

                    //imgArray = ImageSource.ToByteArray();

                    if (imgArray != null)
                    {
                        if (imgArray.Count() > 0)
                        {
                            MemoryStream mstr = new MemoryStream(imgArray);
                            mstr.Position = 0;
                            LinkedResource res = new LinkedResource(mstr, new ContentType(@"image/png"));
                            res.ContentId = contentid;

                            _linkedResources.Add(res);
                        }
                    }

                    #endregion
                }

                

                //Clean up the Html
                htmlBody = htmlDocument.ToHtml();//.Replace(details.Variables);

                #region  Add view to the Email Message


                if (_linkedResources.Count > 0)
                {
                    AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

                    foreach (var res in _linkedResources)
                        alternateView.LinkedResources.Add(res);

                   // mailItem.AlternateViews.Add(alternateView);
                }

                #endregion


            }
            catch(Exception x) { }
            finally
            {
            }

            return htmlBody;
        }

        public static string FixBase64ForImage(string Image)
        {
            Image = Image.Substring(Image.IndexOf(",".ToCharArray()[0]) + 1);
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);
            sbText.Replace("\r\n", string.Empty); sbText.Replace(" ", string.Empty);
            return sbText.ToString();
        }

    }
}
