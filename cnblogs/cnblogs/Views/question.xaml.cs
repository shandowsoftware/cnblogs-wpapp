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
    public partial class question : PhoneApplicationPage
    {
        public question()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            HtmlAgilityPack.HtmlWeb htmlDoc = new HtmlAgilityPack.HtmlWeb();
            htmlDoc.LoadCompleted += new EventHandler<HtmlDocumentLoadCompleted>(htmlDocComplete);

            htmlDoc.LoadAsync("http://q.cnblogs.com/");
        }


        private void htmlDocComplete(object sender, HtmlDocumentLoadCompleted e)
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
                                foreach (HtmlNode item in nodeList)
                                {
                                    title = item.InnerHtml.Trim();
                                    //MessageBox.Show("out--------" + item.InnerHtml.Trim());  //问题标题
                                }


                                HtmlNode questionIntroduce = hsn.SelectSingleNode("//*[@class=\"news_summary\"]");
                                HtmlNode questionAuthor = hsn.SelectSingleNode("//*[@class=\"news_contributor\"]");
                                HtmlNode questionPushDate = hsn.SelectSingleNode("//*[@class=\"date\"]");

                                IEnumerable<HtmlNode> imgAuthorList = hsn.SelectSingleNode("//*[@class=\"author\"]").Descendants("img");

                                foreach (HtmlNode imgAuthor in imgAuthorList)
                                {

                                    string userImage = imgAuthor.GetAttributeValue("src", "");
                                    if (userImage.IndexOf(".gif") > 0)
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

                    this.listBox1.ItemsSource = listContent;


                }

            }

        }
    }
}