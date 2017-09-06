using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Login
{
    class SeleniumTest
    {
        public static void Run()
        {
            if (IsNetConnected)
            {
                Console.WriteLine(@"Connected!");
                return;
            }
            //var url = @"http://172.16.96.100:8080/am/page/portal/realm/2e0d1ff9-c070-4e05-a07e-23325461e9db/login/pc/index.html?stage=2&revisit=false&language=en-US";
            IWebDriver driver = null;
            if (Explore.ToLower() == "chrome")
            {
                driver= new ChromeDriver();
            }
            else
            {
                driver=new InternetExplorerDriver();
            }

            //Notice navigation is slightly different than the Java version
            //This is because 'get' is a keyword in C#

            driver.Navigate().GoToUrl(StartUrl);
            try
            {
                IWebElement name = driver.FindElement(By.Id("un-userName"));
                IWebElement pwd = driver.FindElement(By.Id("un-password"));
                IWebElement login = driver.FindElement(By.Id("un-login"));

                IWebElement revisit = driver.FindElement(By.Id("freeCertification"));

                if (revisit.Displayed)
                {
                    revisit.Click();
                }
                else
                {
                    name.SendKeys("zhenzhen.fu");
                    pwd.SendKeys("Gjk_123456");

                    login.Click();//.SendKeys("Cheese");                
                }
                //if (revisit.Displayed)
                //{
                //    revisit.Click();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"IsNetConnected:{IsNetConnected}");
            //Console.WriteLine(@"Page title is: " + driver.Title);
            driver.Quit();
        }

        public static string StartUrl => ConfigurationManager.AppSettings["start_url"] ?? "http://172.16.96.100:8080/am/page/portal/realm/2e0d1ff9-c070-4e05-a07e-23325461e9db/login/pc/index.html?stage=1&language=en-US";

        public static string UserName=> ConfigurationManager.AppSettings["name"] ?? "zhenzhen.fu";

        public static string Password => ConfigurationManager.AppSettings["password"] ?? "Gjk_123456";

        public static string Explore=> ConfigurationManager.AppSettings["explorer"] ?? "chrome";

        public static bool IsNetConnected => ConnectBaidu();

        private static bool Ping(string ip)
        {
            
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000; // Timeout 时间，单位：毫秒
            System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }

        private static bool ConnectBaidu()
        {

            try
            {
                var resp = HttpWebResponseUtility.CreateGetHttpResponse("https://www.baidu.com/", 30000, null, null);
                if (null != resp)
                {
                    var sr = new StreamReader(resp.GetResponseStream());
                    sr.ReadToEnd();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }           
        }
    }
}
