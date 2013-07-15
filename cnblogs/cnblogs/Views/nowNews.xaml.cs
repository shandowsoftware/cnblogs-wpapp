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

namespace PhoneApp1.Views
{
    public partial class nowNews : PhoneApplicationPage
    {
        public nowNews()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            HtmlAgilityPack.HtmlWeb htmlDoc = new HtmlAgilityPack.HtmlWeb();
            htmlDoc.LoadCompleted += new EventHandler<HtmlDocumentLoadCompleted>(htmlDocComplete);

            htmlDoc.LoadAsync("http://www.cnblogs.com/news");

        }

        private void htmlDocComplete(object sender, HtmlDocumentLoadCompleted e)
        {
            if (e.Error == null)
            {
                HtmlDocument htmlDoc = e.Document;
                if (htmlDoc != null)
                {
                    HtmlNode node = htmlDoc.GetElementbyId("post_list");
                    

                    List<ListContent> listContent = new List<ListContent>();
                    foreach (HtmlNode child in node.ChildNodes)
                    {

                        if (child.Attributes["class"] == null || child.Attributes["class"].Value != "post_item")
                            continue;
                        HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                        ListContent listContextObj = new ListContent();
                        

                        string title = hn.SelectSingleNode("//*[@class=\"titlelnk\"]").InnerText;
                        string jieshao = hn.SelectSingleNode("//*[@class=\"post_item_summary\"]").InnerText;
                        
                        //String xinxi = hn.SelectSingleNode("//*[@class=\"post_item_foot\"]").InnerText;
                        string xinxi2 = hn.SelectSingleNode("//*[@class=\"lightblue\"]").InnerText.Trim();
                        string xinxi3 = hn.SelectSingleNode("//*[@class=\"article_comment\"]").InnerText.Trim();
                        string xinxi4 = hn.SelectSingleNode("//*[@class=\"article_view\"]").InnerText.Trim();

                        //MessageBox.Show("xinxi2------" + xinxi2);
                        //String titleIDValue = hn.SelectSingleNode("//*[@class=\"diggnum\"]").InnerText;

                        string titlelnk = hn.SelectSingleNode("//*[@class=\"titlelnk\"]").GetAttributeValue("href", "");
						
						//string newsImage = hn.SelectSingleNode("//*[@class=\"news_topic\"]").GetAttributeValue("src", "");
						//MessageBox.Show("newsImage------" + newsImage);
						
                        listContextObj.Title = title;
                        listContextObj.titleLink = titlelnk;
                        listContextObj.Introduce = jieshao;
                        listContextObj.Info = xinxi2 + xinxi3 + xinxi4;
                        listContent.Add(listContextObj);

                    }
                    this.listBox1.ItemsSource = listContent;



                }
            }

        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlock textBlock = (TextBlock)e.OriginalSource;

            string tagLink = (string)textBlock.Tag;
            //MessageBox.Show(tagLink);
            NavigationService.Navigate(new Uri("/ContentViews/nowNewsContent.xaml?titleLinkValue=" + tagLink + "", UriKind.Relative));
        }
    }
}