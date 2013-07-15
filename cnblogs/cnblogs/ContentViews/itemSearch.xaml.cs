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

namespace cnblogs.ContentViews
{
    public partial class itemSearch : PhoneApplicationPage
    {

        private string searchValueHttpUri;

        private int pageValue = 1;

        public itemSearch()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

            if (NavigationContext.QueryString.Count > 0)
            {
                searchValueHttpUri = NavigationContext.QueryString["searchParam"];
                getSearchResult(searchValueHttpUri, pageValue);
            }

   
        }

        private  void getSearchResult(string searchValueHttpUri,int pageParam) {

            HtmlAgilityPack.HtmlWeb htmlDoc = new HtmlAgilityPack.HtmlWeb();
            htmlDoc.LoadCompleted += new EventHandler<HtmlDocumentLoadCompleted>(htmlDocCompleteSearch);

            htmlDoc.LoadAsync("http://zzk.cnblogs.com/s?w=" + searchValueHttpUri + "&t=b&p="+pageParam+"");
        
        }


        private void htmlDocCompleteSearch(object sender, HtmlDocumentLoadCompleted e)
        {

            if (e.Error == null)
            {
                HtmlDocument htmlDoc = e.Document;
                if (htmlDoc != null)
                {
                    List<SearchEntity> searchList = new List<SearchEntity>();
                    
                    HtmlNode node = htmlDoc.GetElementbyId("searchResult");
                    
                    HtmlNodeCollection noC = node.SelectNodes("//*[@class=\"searchItem\"]");

                    foreach (HtmlNode h in noC)
                    {
                        HtmlNode hn1 = HtmlNode.CreateNode(h.OuterHtml);
                        SearchEntity search = new SearchEntity();

                        foreach (HtmlNode child in hn1.ChildNodes)
                        {
  
                            if (child.Attributes["class"] != null && child.Attributes["class"].Value == "searchItemTitle")
                            {
                                HtmlNode hsn = HtmlNode.CreateNode(child.OuterHtml);
                                
                                string searchTitleValue = hsn.SelectSingleNode("//*[@class=\"searchItemTitle\"]").InnerText.Trim();
                                search.searchTitle = searchTitleValue;
                                //MessageBox.Show("searchTitle--------------" + searchTitle.Trim());
                            }
                            
                            else if (child.Attributes["class"] != null && child.Attributes["class"].Value == "searchCon")
                            {
                                HtmlNode hsn = HtmlNode.CreateNode(child.OuterHtml);

                                
                                string searchConValue = hsn.SelectSingleNode("//*[@class=\"searchCon\"]").InnerText.Trim();
                                search.searchCon = searchConValue;

                                //MessageBox.Show("searchCon--------------" + searchCon.Trim());
                            }
                            
                            else if (child.Attributes["class"] != null && child.Attributes["class"].Value == "searchItemInfo")
                            {
                                
                                
                                HtmlNode hsn = HtmlNode.CreateNode(child.OuterHtml);
                                foreach (HtmlNode searchItemInfoChild in hsn.ChildNodes)
                                {
                                    if (searchItemInfoChild.Attributes["class"] != null && searchItemInfoChild.Attributes["class"].Value == "searchItemInfo-userName")
                                    {
                                        string userName = searchItemInfoChild.SelectSingleNode("//*[@class=\"searchItemInfo-userName\"]").InnerText.Trim();
                                        string publishDate = searchItemInfoChild.SelectSingleNode("//*[@class=\"searchItemInfo-publishDate\"]").InnerText.Trim();
                                        string good = searchItemInfoChild.SelectSingleNode("//*[@class=\"searchItemInfo-good\"]").InnerText.Trim();
                                        string comments = searchItemInfoChild.SelectSingleNode("//*[@class=\"searchItemInfo-comments\"]").InnerText.Trim();
                                        string views = searchItemInfoChild.SelectSingleNode("//*[@class=\"searchItemInfo-views\"]").InnerText.Trim();
                                        string searchInfoValue = userName + publishDate + good + comments + views;
                                        search.searchInfo = searchInfoValue;
                                        //MessageBox.Show("searchInfo--------------" + userName+publishDate+good+comments+views);
                                    }

                                    if (searchItemInfoChild.Attributes["class"] != null && searchItemInfoChild.Attributes["class"].Value == "searchURL")
                                    {
                                        string searchURLValue = searchItemInfoChild.SelectSingleNode("//*[@class=\"searchURL\"]").InnerText.Trim();
                                        search.searchURL = searchURLValue;
                                        //MessageBox.Show("searchInfo--------------" + searchURL);
                                    }
                                }

                                
                                
                                //string searchInfo = child.SelectSingleNode("//*[@class=\"searchItemInfo\"]").InnerText;
                                //MessageBox.Show("searchInfo--------------" + searchInfo);
                            }

                            
                       }

                        searchList.Add(search);
                    }


                    this.listBox1.ItemsSource = searchList;

                }

            }

        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("button1");
            pageValue--;
            if(pageValue<1){
                pageValue = 1;
            }
            getSearchResult(searchValueHttpUri,pageValue);
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            pageValue++;
            if(pageValue>=100){
                pageValue = 100;
            }
            getSearchResult(searchValueHttpUri, pageValue);
        }
    }
}