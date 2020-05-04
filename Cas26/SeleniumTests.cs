using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

namespace Cas26
{
    class SeleniumTests
    {
        IWebDriver driverFirefox;
        IWebDriver driverChrome;

        string url = "https://www.google.com/";

        [Test]
        public void TestGoogleSearchInFirefox()
        {
            driverFirefox = new FirefoxDriver();
            Navigate(driverFirefox, url);

            By selector = By.Name("q");
            IWebElement search = FindElement(driverFirefox, selector);
            // *** Red ispod radi isto sto i dva reda iznad ***
            //IWebElement search = FindElement(driverFirefox, By.Name("q"));
            if (search != null)
            {
                SendKeys(search, "Selenium C#", sendEnter: false);
            }

            selector = By.XPath("//input[@name='btnK']");
            IWebElement button = FindElement(driverFirefox, selector);
            if (button != null)
            {
                button.Click();
            }
            Wait(2000);

            selector = By.PartialLinkText("to English");
            IWebElement changeToEnglish = FindElement(driverFirefox, selector);
            if (changeToEnglish != null)
            {
                changeToEnglish.Click();
                Wait(2000);
            }

            IWebElement body = FindElement(driverFirefox, By.TagName("body"));
            if (body.Text.Contains("Videos"))
            {
                Assert.Pass();
            } else
            {
                Assert.Fail("Test failed - no videos present.");
            }

            /*
            IWebElement nav = FindElement(driverFirefox, By.Id("top_nav_"));
            if (nav != null)
            {
                if (nav.Displayed && nav.Enabled)
                {
                    Assert.Pass("Test completed successfully.");
                } else
                {
                    Assert.Fail("Test failed - element is not visible.");
                }
            } else
            {
                Assert.Fail("Test failed - no element present.");
            }
            */
        }

        [Test]
        public void TestGoogleSearchInChrome()
        {
            driverChrome = new ChromeDriver();

            By selector = By.Name("q");
            IWebElement search = driverChrome.FindElement(selector);
            // *** Red ispod radi isto sto i dva reda iznad ***
            //IWebElement search = driverFirefox.FindElement(By.Name("q"));
            search.SendKeys("Selenium C#");
            search.SendKeys(Keys.Enter);
            Wait(4000);
        }

        [TearDown]
        public void TearDown()
        {
            driverFirefox.Close();
        }

        public IWebElement FindElement(IWebDriver driver, By selector)
        {
            IWebElement elReturn = null;
            try
            {
                elReturn = driver.FindElement(selector);
            } catch (NoSuchElementException)
            {
            } catch(Exception e)
            {
                throw e;
            }

            return elReturn;
        }

        private void SendKeys(IWebElement element, string keys, int wait = 1000, bool sendEnter = true)
        {
            element.SendKeys(keys);
            Wait(wait);
            if (sendEnter)
            {
                element.SendKeys(Keys.Enter);
            }
        }

        static private void Navigate(IWebDriver driver, string url, int wait = 1000)
        {
            driver.Manage().Window.Maximize();
            Wait(wait);
            driver.Navigate().GoToUrl(url);
            Wait(wait);
        }

        static private void Wait(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }
    }
}
