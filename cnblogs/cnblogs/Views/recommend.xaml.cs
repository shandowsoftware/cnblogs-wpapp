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
    public partial class recommend : PhoneApplicationPage
    {
        public recommend()
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
                    HtmlNode node = htmlDoc.GetElementbyId("headline_block");
                    //IEnumerable<HtmlNode>  nodeList =htmlDoc.DocumentNode.Descendants("a");
                    HtmlNode hn = HtmlNode.CreateNode(node.OuterHtml);
                    IEnumerable<HtmlNode> nodeList=hn.Descendants("a");
                    List<Recom> listContent = new List<Recom>();

                    foreach (HtmlNode item in nodeList)
                    {
                        //MessageBox.Show("out--------"+item.InnerHtml);
                        //MessageBox.Show("out--------" + item.InnerText);
                        string title = item.InnerText;

                        if(title==null||title.Length<2)
                            continue;
                            string recomTitle = title;
                            //MessageBox.Show("out--------" + title);
                            string titlelnk = item.GetAttributeValue("href", "");
                            //MessageBox.Show("titlelnk--------" + titlelnk);
                            Recom recom = new Recom();
                            recom.recomTitle = recomTitle;
                            recom.titleLink = titlelnk;
                            listContent.Add(recom);

                    }

                    this.listBox1.ItemsSource = listContent;

                }
            }

        }

       

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlock textBlock = (TextBlock)e.OriginalSource;

            string tagLink = (string)textBlock.Tag;

            //MessageBox.Show("this is -----------" + tagLink);


            NavigationService.Navigate(new Uri("/Views/Pasta.xaml?titleLinkValue=" + tagLink + "", UriKind.Relative));
        }
    }
}