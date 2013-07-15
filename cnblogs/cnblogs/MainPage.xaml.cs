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
using PhoneApp1;
using HtmlAgilityPack;
using System.Xml.XPath;


namespace cnblogs
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 将 listbox 控件的数据上下文设置为示例数据
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // 为 ViewModel 项加载数据
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void PanoramaItem_Loaded(object sender, RoutedEventArgs e)
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
                    IEnumerable<HtmlNode> nodeList = hn.Descendants("a");
                    List<Recom> listContent = new List<Recom>();
                    
                    int index = 0;

                    foreach (HtmlNode item in nodeList)
                    {


                        index++;
    
                        string title = item.InnerText;

                        if (title == null || title.Length < 2)
                            continue;
                        
                        //MessageBox.Show("test-----------" + index);
                        string recomTitle = title;
                        //MessageBox.Show("out--------" + title);
                        string titlelnk = item.GetAttributeValue("href", "");
                        //MessageBox.Show("titlelnk--------" + titlelnk);
                        string viewsTitleLnk="";
                        Recom recom = new Recom();

                        if(index==1){
                            viewsTitleLnk=  titlelnk + "&&" + "/ContentViews/";
                        }else if(index==3||index==5){
                            viewsTitleLnk=  titlelnk + "&&" + "/ContentViews/indexContent.xaml";
                            recom.viweImages = "/images/cream.png";
                        }else if(index==6||index==8){
                            viewsTitleLnk = titlelnk + "&&" + "/ContentViews/nowNewsContent.xaml";
                            recom.viweImages = "/images/news.png";
                        }

                        
                        recom.recomTitle = recomTitle;
                        recom.titleLink = viewsTitleLnk;
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
            int index=tagLink.IndexOf("&&");
            

            string titleLink = tagLink.Substring(0,tagLink.IndexOf("&&"));
            string viewsLink = tagLink.Substring(tagLink.IndexOf("&&")+2);
            //MessageBox.Show("viewsLink--------" + viewsLink);
            //MessageBox.Show("this is -----------" + titleLink);


            NavigationService.Navigate(new Uri("" + viewsLink + "?titleLinkValue=" + titleLink + "", UriKind.Relative));
        }


        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPost1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/index.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 候选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPost2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/wait.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 推荐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPost5_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/login.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 精华
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPost4_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/cream.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 新闻
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPost3_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/nowNews.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPost6_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/about.xaml", UriKind.Relative));
        }

        /// <summary>
        /// 加载问答列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanoramaItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            HtmlAgilityPack.HtmlWeb htmlDoc = new HtmlAgilityPack.HtmlWeb();
            htmlDoc.LoadCompleted += new EventHandler<HtmlDocumentLoadCompleted>(htmlDocCompleteQuestion);

            htmlDoc.LoadAsync("http://q.cnblogs.com/");
        }


        private void htmlDocCompleteQuestion(object sender, HtmlDocumentLoadCompleted e)
        {
            if (e.Error == null)
            {
                HtmlDocument htmlDoc = e.Document;
                if (htmlDoc != null)
                {

                    List<Question> listContent = new List<Question>();

                    HtmlNode node = htmlDoc.GetElementbyId("main");
                    HtmlNode hn = HtmlNode.CreateNode(node.OuterHtml);
                    HtmlNodeCollection noC = hn.SelectNodes("//*[@class=\"one_entity\"]");



                    foreach (HtmlNode h in noC)
                    {

                        HtmlNode hn1 = HtmlNode.CreateNode(h.OuterHtml);


                        //MessageBox.Show("hn1------"+hn1.InnerHtml);
                        foreach (HtmlNode child in hn1.ChildNodes)
                        {

                            Question question = new Question();
                            /**if (child.Attributes["class"] == null || child.Attributes["class"].Value != "answercount")
                                continue;
                                
                                HtmlNode hsn = HtmlNode.CreateNode(child.OuterHtml);
                                string answerCount = hsn.InnerText.Trim();
                                MessageBox.Show(answerCount.Substring(0,answerCount.IndexOf("回答数")).Trim());*/


                            if (child.Attributes["class"] != null && child.Attributes["class"].Value == "answercount")
                            {
                                HtmlNode hsn = HtmlNode.CreateNode(child.OuterHtml);
                                string answerCount = hsn.InnerText.Trim();
                                //MessageBox.Show(answerCount.Substring(0, answerCount.IndexOf("回答数")).Trim());//回答人数


                            }
                            else if (child.Attributes["class"] != null && child.Attributes["class"].Value == "news_item")
                            {
                                HtmlNode hsn = HtmlNode.CreateNode(child.OuterHtml);
                                HtmlNode gold = hsn.SelectSingleNode("//*[@class=\"gold\"]");
                                string goldCount = "";
                                if (gold != null)
                                {
                                    goldCount = gold.InnerText.Trim();
                                    //MessageBox.Show("gold----------" + gold.InnerText.Trim());  //悬赏金币
                                }
                                else
                                {
                                    goldCount = "0";
                                    //MessageBox.Show("gold----------0");                   //悬赏金币 空则为0
                                }

                                IEnumerable<HtmlNode> nodeList = hsn.SelectSingleNode("//*[@class=\"news_entry\"]").Descendants("a");
                                string title = "";
                                string titlelink = "";
                                foreach (HtmlNode item in nodeList)
                                {
                                    title = item.InnerHtml.Trim();
                                    titlelink = item.GetAttributeValue("href", "");

                                    //MessageBox.Show("out--------" + item.InnerHtml.Trim());  //问题标题
                                }


                                HtmlNode questionIntroduce = hsn.SelectSingleNode("//*[@class=\"news_summary\"]");
                                HtmlNode questionAuthor = hsn.SelectSingleNode("//*[@class=\"news_contributor\"]");
                                HtmlNode questionPushDate = hsn.SelectSingleNode("//*[@class=\"date\"]");

                                IEnumerable<HtmlNode> imgAuthorList = hsn.SelectSingleNode("//*[@class=\"author\"]").Descendants("img");

                                foreach (HtmlNode imgAuthor in imgAuthorList)
                                {

                                    string userImage = imgAuthor.GetAttributeValue("src", "");
                                    if (userImage.IndexOf(".gif") > -1)
                                    {
                                        question.questionUserImage = "/images/skydrive.png";
                                    }
                                    else
                                    {
                                        question.questionUserImage = userImage;
                                    }

                                    //MessageBox.Show("out--------" + item.InnerHtml.Trim());  //问题标题
                                }



                                string introduce = questionIntroduce.InnerText.Trim();
                                string author = questionAuthor.InnerText.Trim();
                                string date = questionPushDate.InnerText.Trim();

                                question.questionTitle = title;
                                question.questionLink = titlelink;
                                question.questionIntroduce = introduce;
                                question.questionInfo = author + "  " + date;
                                question.questionGoldImage = "/images/gold.png";
                                question.questionGoldCount = goldCount;

                                listContent.Add(question);
                                //MessageBox.Show("questionIntroduce------------" + questionIntroduce.InnerText.Trim());   //问题简介
                                //MessageBox.Show("news_footer----------" + questionAuthor.InnerText.Trim());    //提问者
                                //MessageBox.Show("date----------" + questionPushDate.InnerText.Trim());         //发布时间

                                //string answerCount = hsn.InnerText.Trim();



                            }
                            else
                            {
                                continue;
                            }




                        }


                        //HtmlNode newnode = hn.SelectSingleNode("/div/div/div[3]");

                        /**IEnumerable<HtmlNode> nodeList = node.Ancestors();  //获取该元素所有的父节点的集合
                        foreach (HtmlNode item in nodeList)
                        {
                            Console.Write(item.Name + " ");   //输出 div div body html #document
                        }*/


                    }

                    this.listBox2.ItemsSource = listContent;


                }

            }

        }

        private void TextBlock_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlock textBlock = (TextBlock)e.OriginalSource;

            string tagLink = (string)textBlock.Tag;

            NavigationService.Navigate(new Uri("/ContentViews/QuestionContent.xaml?titleLinkValue=" + tagLink + "", UriKind.Relative));

        }

        private void btnSearch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string searchValue = this.searchValue.Text;
            NavigationService.Navigate(new Uri("/ContentViews/itemSearch.xaml?searchParam=" + searchValue + "", UriKind.Relative));
            //NavigationService.Navigate(new Uri("/ContentViews/itemSearch.xaml", UriKind.Relative));
            


        }




    }
}