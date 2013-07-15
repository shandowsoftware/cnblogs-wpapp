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
    public partial class wait : PhoneApplicationPage
    {
        public wait()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
          

            HtmlAgilityPack.HtmlWeb htmlDoc = new HtmlAgilityPack.HtmlWeb();
            htmlDoc.LoadCompleted += new EventHandler<HtmlDocumentLoadCompleted>(htmlDocComplete);

            htmlDoc.LoadAsync("http://www.cnblogs.com/candidate");
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
                        String tuijian = hn.SelectSingleNode("//*[@class=\"diggnum\"]").InnerText;
                        String title = hn.SelectSingleNode("//*[@class=\"titlelnk\"]").InnerText;
                        String jieshao = hn.SelectSingleNode("//*[@class=\"post_item_summary\"]").InnerText;

                        String xinxi = hn.SelectSingleNode("//*[@class=\"post_item_foot\"]").InnerText;
                        String xinxi2 = hn.SelectSingleNode("//*[@class=\"lightblue\"]").InnerText.Trim();
                        String xinxi3 = hn.SelectSingleNode("//*[@class=\"article_comment\"]").InnerText.Trim();
                        String xinxi4 = hn.SelectSingleNode("//*[@class=\"article_view\"]").InnerText.Trim();


                        String titleIDValue = hn.SelectSingleNode("//*[@class=\"diggnum\"]").InnerText;

                        String titlelnk = hn.SelectSingleNode("//*[@class=\"titlelnk\"]").GetAttributeValue("href", "");

                        listContextObj.Title = title;
                        listContextObj.recommendImage = "/images/cream.png";
                        listContextObj.recommendCount = tuijian;
                        listContextObj.Introduce = jieshao;
                        listContextObj.Info = xinxi2 + xinxi3 + xinxi4;
                        listContextObj.titleLink = titlelnk;
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
            NavigationService.Navigate(new Uri("/ContentViews/waitContent.xaml?titleLinkValue=" + tagLink + "", UriKind.Relative));
        }

    }
}