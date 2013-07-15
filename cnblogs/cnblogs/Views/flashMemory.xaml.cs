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
using System.IO;

namespace cnblogs.Views
{
    public partial class flashMemory : PhoneApplicationPage
    {
        public flashMemory()
        {
            InitializeComponent();
        }

        public CookieContainer cookieContainer = new CookieContainer();
        public HttpWebRequest myRequest;
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
           
            //myRequest = (HttpWebRequest)WebRequest.Create("http://passport.cnblogs.com/login.aspx");
            myRequest = (HttpWebRequest)WebRequest.Create("http://m.cnblogs.com/mobileLoginPost.aspx");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            if (cookieContainer==null)
            {
                myRequest.CookieContainer = new CookieContainer();
            }
            myRequest.CookieContainer = cookieContainer;

            myRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), myRequest);



        }



        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            System.IO.Stream postStream = request.EndGetRequestStream(asynchronousResult);

            // Prepare Parameters String
            //string parametersString = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwULLTE1MzYzODg2NzZkGAEFHl9fQ29udHJvbHNSZXF1aXJlUG9zdEJhY2tLZXlfXxYBBQtjaGtSZW1lbWJlcm1QYDyKKI9af4b67Mzq2xFaL9Bt&__EVENTVALIDATION=%2FwEdAAUyDI6H%2Fs9f%2BZALqNAA4PyUhI6Xi65hwcQ8%2FQoQCF8JIahXufbhIqPmwKf992GTkd0wq1PKp6%2B%2F1yNGng6H71Uxop4oRunf14dz2Zt2%2BQKDEIYpifFQj3yQiLk3eeHVQqcjiaAP&tbUserName=shandowsoftware&tbPassword=woaini5201314..&chkRemember=on&btnLogin=%E7%99%BB++%E5%BD%95&txtReturnUrl=http://home.cnblogs.com/";
            string parametersString = "tbUserName=shandowsoftware&tbPassword=woaini5201314..&chkRemember=on&txtReturnUrl=";
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(parametersString);
            // Write to the request stream.
            postStream.Write(byteArray, 0, parametersString.Length);
            postStream.Close();
            // Start the asynchronous operation to get the response
            request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
        }



        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            Stream streamResponse = null;
            string responseString = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
                streamResponse = response.GetResponseStream();
                
                CookieCollection cookieCollection = response.Cookies;
                cookieContainer.Add(new Uri("http://space.cnblogs.com/mobile/ming_post.aspx"), cookieCollection);
                //cookieContainer.Add(new Uri("http://home.cnblogs.com/ajax/ing/Publish"), cookieCollection);
                using (StreamReader streamRead = new StreamReader(streamResponse))
                {
                    responseString = streamRead.ReadToEnd();
                }
            }
            catch { }
            finally
            {
                if (streamResponse != null)
                {
                    streamResponse.Close();
                }
            }

            if (responseString != null)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    string result = responseString;

                  
                    textBlockResult.Text = result;
                });
            }

        }

        private void submitData_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

           
            myRequest = (HttpWebRequest)WebRequest.Create("http://space.cnblogs.com/mobile/ming_post.aspx");
            //myRequest = (HttpWebRequest)WebRequest.Create("http://home.cnblogs.com/ajax/ing/Publish");
            myRequest.Method = "POST";
            
            myRequest.ContentType = "application/x-www-form-urlencoded";
            //myRequest.Accept = "application/json,text/javascript,*/*;q=0.01";
            //myRequest.ContentType = "application/json;chartset=utf-8";
            if (cookieContainer==null)
            {
                myRequest.CookieContainer = new CookieContainer();
                
            }
            myRequest.CookieContainer = cookieContainer;
            
            myRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallbackqq), myRequest);
            
        }

        private void GetRequestStreamCallbackqq(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            System.IO.Stream postStream = request.EndGetRequestStream(asynchronousResult);

            // Prepare Parameters String
            //string parametersString = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwULLTE1MzYzODg2NzZkGAEFHl9fQ29udHJvbHNSZXF1aXJlUG9zdEJhY2tLZXlfXxYBBQtjaGtSZW1lbWJlcm1QYDyKKI9af4b67Mzq2xFaL9Bt&__EVENTVALIDATION=%2FwEdAAUyDI6H%2Fs9f%2BZALqNAA4PyUhI6Xi65hwcQ8%2FQoQCF8JIahXufbhIqPmwKf992GTkd0wq1PKp6%2B%2F1yNGng6H71Uxop4oRunf14dz2Zt2%2BQKDEIYpifFQj3yQiLk3eeHVQqcjiaAP&tbUserName=shandowsoftware&tbPassword=woaini5201314..&chkRemember=on&btnLogin=%E7%99%BB++%E5%BD%95&txtReturnUrl=http://home.cnblogs.com/";
            
            //string jsonText="{\"content\":\"来自[windows phone]客户端\",\"publicFlag\":1}";
            string jsonText = "txbContent=[来自windows phone客户端]各位早上好!&ingSubmit=%E6%8F%90%E4%BA%A4";
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(jsonText);
            // Write to the request stream.
            postStream.Write(byteArray, 0, jsonText.Length);
            postStream.Close();
            // Start the asynchronous operation to get the response
            request.BeginGetResponse(new AsyncCallback(GetResponseCallbackqq), request);
        }



        private void GetResponseCallbackqq(IAsyncResult asynchronousResult)
        {
            Stream streamResponse = null;
            string responseString = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
                streamResponse = response.GetResponseStream();

                //cookieCollection = response.Cookies;

                using (StreamReader streamRead = new StreamReader(streamResponse))
                {
                    responseString = streamRead.ReadToEnd();
                }
            }
            catch { }
            finally
            {
                if (streamResponse != null)
                {
                    streamResponse.Close();
                }
            }

            if (responseString != null)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    string result = responseString;
                    textBlockResult.Text = "";

                    textBlockResult.Text = result;
                });
            }
            else
            {
                Dispatcher.BeginInvoke(() =>
                {
                    
                    textBlockResult.Text = "";

                    textBlockResult.Text = "error";
                });
            }

        }



    }
}