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
using cnblogs;

namespace PhoneApp1.Views
{
    public partial class index : PhoneApplicationPage
    {
        public index()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            

            HtmlAgilityPack.HtmlWeb htmlDoc = new HtmlAgilityPack.HtmlWeb();
            htmlDoc.LoadCompleted += new EventHandler<HtmlDocumentLoadCompleted>(htmlDocComplete);

            htmlDoc.LoadAsync("http://www.cnblogs.com/");
        }


        private void htmlDocComplete(object sender, HtmlDocumentLoadCompleted e)
        {
            if (e.Error == null)
            {
                HtmlDocument htmlDoc = e.Document;
                if (htmlDoc != null)
                {
                    HtmlNode node = htmlDoc.GetElementbyId("post_list");

                    List<BlogIndexContent> listContent = new List<BlogIndexContent>();
                   

                    foreach (HtmlNode child in node.ChildNodes)
                    {

                        if (child.Attributes["class"] == null || child.Attributes["class"].Value != "post_item")
                            continue;
                        HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                        BlogIndexContent listContextObj = new BlogIndexContent();
                        string tuijian = hn.SelectSingleNode("//*[@class=\"diggnum\"]").InnerText;
                        string title = hn.SelectSingleNode("//*[@class=\"titlelnk\"]").InnerText;
                        string jieshao = hn.SelectSingleNode("//*[@class=\"post_item_summary\"]").InnerText;

                        string xinxi = hn.SelectSingleNode("//*[@class=\"post_item_foot\"]").InnerText;
                        string xinxi2 = hn.SelectSingleNode("//*[@class=\"lightblue\"]").InnerText.Trim();
                        string xinxi3 = hn.SelectSingleNode("//*[@class=\"article_comment\"]").InnerText.Trim();
                        string xinxi4 = hn.SelectSingleNode("//*[@class=\"article_view\"]").InnerText.Trim();


                        string titleIDValue = hn.SelectSingleNode("//*[@class=\"diggnum\"]").InnerText;
                        
                        string titlelnk = hn.SelectSingleNode("//*[@class=\"titlelnk\"]").GetAttributeValue("href", "");
                        



                        IEnumerable<HtmlNode> userImageNode = hn.SelectSingleNode("//*[@class=\"post_item_summary\"]").Descendants("img");
                        
                        
                            foreach (HtmlNode imgAuthor in userImageNode)
                            {


                                string userImage = imgAuthor.GetAttributeValue("src", "");
                                //MessageBox.Show("userimage----------"+userImage);
                               if (userImage != null && userImage.IndexOf(".png") > -1 || userImage.IndexOf(".jpg") > -1)
                                {
                                    listContextObj.userImage = userImage;
                                }
                                else {
                                    listContextObj.userImage = "/images/skydrive.png";
                                }
                                
                           

                                
                                 
                                
                            
                                

                            //MessageBox.Show("out--------" + item.InnerHtml.Trim());  //问题标题
                            }
                        
                        



                        //string userImage = hn.SelectSingleNode("//*[@class=\"pfs\"]").GetAttributeValue("src", "");
                     

                        listContextObj.Title = title;
                        listContextObj.Introduce = jieshao;
                        listContextObj.Info = xinxi2 + xinxi3 + xinxi4;
                        listContextObj.titleLink = titlelnk;
                        listContextObj.recommendCount = tuijian;
                        //listContextObj.userImage = userImage;
                        listContextObj.recommendImage = "/images/cream.png";
                        listContent.Add(listContextObj);

                    }
                    this.listBox1.ItemsSource = listContent;

                

                }
            }

        }

       

        private void TextBlock_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlock textBlock = (TextBlock)e.OriginalSource;

            string tagLink = (string)textBlock.Tag;
            //MessageBox.Show(tagLink);
            NavigationService.Navigate(new Uri("/ContentViews/indexContent.xaml?titleLinkValue=" + tagLink + "", UriKind.Relative));
        }

     

      

    }
}