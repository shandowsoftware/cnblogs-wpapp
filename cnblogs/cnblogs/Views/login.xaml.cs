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
using WP7_WebLib.HttpPost;
using System.IO;

namespace cnblogs.Views
{
    public partial class login : PhoneApplicationPage
    {
        public login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            /**Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("__EVENTARGUMENT", "");
            parameters.Add("__EVENTTARGET", "");
            parameters.Add("__EVENTVALIDATION", "/wEdAAUyDI6H/s9f+ZALqNAA4PyUhI6Xi65hwcQ8/QoQCF8JIahXufbhIqPmwKf992GTkd0wq1PKp6+/1yNGng6H71Uxop4oRunf14dz2Zt2+QKDEIYpifFQj3yQiLk3eeHVQqcjiaAP");
            parameters.Add("__VIEWSTATE", "/wEPDwULLTE1MzYzODg2NzZkGAEFHl9fQ29udHJvbHNSZXF1aXJlUG9zdEJhY2tLZXlfXxYBBQtjaGtSZW1lbWJlcm1QYDyKKI9af4b67Mzq2xFaL9Bt");

            parameters.Add("btnLogin", "登 录");
            //parameters.Add("chkRemember", "on");
            parameters.Add("tbPassword", "woaini5201314..");
            parameters.Add("tbUserName", "shandowsoftware");
            parameters.Add("txtReturnUrl", "http://home.cnblogs.com/");*/

            //getResponseHtml(parameters);


            /**CookieContainer myCookie = new CookieContainer();
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://passport.cnblogs.com/login.aspx");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            
            myRequest.CookieContainer = myCookie;
            myRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), myRequest);*/
            string username=this.username.Text;
            string password = this.password.Password;
            
            NavigationService.Navigate(new Uri("/Views/flashMemory.xaml?username="+username+"&password="+password+"", UriKind.Relative));
        }


        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            System.IO.Stream postStream = request.EndGetRequestStream(asynchronousResult);

            // Prepare Parameters String
            string parametersString = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwULLTE1MzYzODg2NzZkGAEFHl9fQ29udHJvbHNSZXF1aXJlUG9zdEJhY2tLZXlfXxYBBQtjaGtSZW1lbWJlcm1QYDyKKI9af4b67Mzq2xFaL9Bt&__EVENTVALIDATION=%2FwEdAAUyDI6H%2Fs9f%2BZALqNAA4PyUhI6Xi65hwcQ8%2FQoQCF8JIahXufbhIqPmwKf992GTkd0wq1PKp6%2B%2F1yNGng6H71Uxop4oRunf14dz2Zt2%2BQKDEIYpifFQj3yQiLk3eeHVQqcjiaAP&tbUserName=shandowsoftware&tbPassword=woaini5201314..&chkRemember=on&btnLogin=%E7%99%BB++%E5%BD%95&txtReturnUrl=http://home.cnblogs.com/";
                
                
            //foreach (KeyValuePair<string, string> parameter in parameters)
            //{
            //    parametersString = parametersString + (parametersString != "" ? "&" : "") + string.Format("{0}={1}", parameter.Key, parameter.Value);
            //}

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
                CookieCollection cookieColl = response.Cookies;
                using (StreamReader streamRead = new StreamReader(streamResponse))
                {
                    responseString = streamRead.ReadToEnd();
                }
            }
            catch { }
            finally 
            {
                if(streamResponse!=null)
                {
                    streamResponse.Close();
                }
            }

            if(responseString!=null)
            {
                Dispatcher.BeginInvoke(() => 
                {
                    string result=responseString.Substring(400,1400);
                    textBlockResult.Text =result; 
                });
            }



            /**StreamReader streamRead = new StreamReader(streamResponse);
            string responseString = streamRead.ReadToEnd();

            Dispatcher.BeginInvoke(() => { textBlockResult.Text = responseString; });



            // Close the stream object
            streamResponse.Close();
            streamRead.Close();*/


        }




       /** private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            Stream streamResponse = response.GetResponseStream();
            CookieCollection cookieColl = response.Cookies;
            StreamReader streamRead = new StreamReader(streamResponse);
            string responseString = streamRead.ReadToEnd();
        
            Dispatcher.BeginInvoke(() => { textBlockResult.Text = responseString; });

            

            // Close the stream object
            streamResponse.Close();
            streamRead.Close();

        
        }*/






        /**public void getResponseHtml(Dictionary<string, object> parameters)
        {

            PostClient proxy = new PostClient(parameters);
            proxy.DownloadStringCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    //Process the result... 
                    string data = e.Result;

                    MessageBox.Show(data);
                }
            };
            proxy.DownloadStringAsync(new Uri("http://passport.cnblogs.com/login.aspx", UriKind.Absolute));
        
        }  */     

    }
}