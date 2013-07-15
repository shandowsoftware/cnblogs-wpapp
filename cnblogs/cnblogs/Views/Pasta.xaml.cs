using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using HtmlAgilityPack;
using System.IO;
using System.Text;

namespace PhoneApp1.Views
{
    public partial class Pasta : PhoneApplicationPage
    {
        public Pasta()
        {
            InitializeComponent();
        }
       
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

            if (NavigationContext.QueryString.Count > 0) {
                string titleHttpUri = NavigationContext.QueryString["titleLinkValue"];
                //myWebBrowser.Navigate(new Uri(titleHttpUri));

                //MessageBox.Show("title------" + titleHttpUri);

                HtmlAgilityPack.HtmlWeb htmlDoc = new HtmlAgilityPack.HtmlWeb();

                htmlDoc.LoadCompleted += new EventHandler<HtmlDocumentLoadCompleted>(htmlDocComplete);
                htmlDoc.LoadAsync(titleHttpUri);  
            
            }
        }


        private void htmlDocComplete(object sender, HtmlDocumentLoadCompleted e)
        {
            if (e.Error == null)
            {
                HtmlDocument htmlDoc = e.Document;
                if (htmlDoc != null)
                {
                    HtmlNode node = htmlDoc.GetElementbyId("cnblogs_post_body");

                    string str = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' />" +
                    "<title></title></head><body bgcolor=\"#000000\"><font color=\"#FFFFFF\">" + node.InnerHtml + "</font></body></html>";
                    
                    string result=ConvertEncode(str);
                    myWebBrowser.NavigateToString(result);

                }
            }

        }



       
      

        /// <summary>
        /// 将字符串编码格式转换为unicode
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        private string ConvertEncode(string strSource)
        {
            MemoryStream mstream = new MemoryStream(Encoding.UTF8.GetBytes(strSource));
            StreamReader reader = new StreamReader(mstream, Encoding.Unicode);
            string strResult = reader.ReadToEnd();
            reader.Close();
            mstream.Dispose();
            reader.Dispose();
            return strResult;
        }

       

      




      




    }
}