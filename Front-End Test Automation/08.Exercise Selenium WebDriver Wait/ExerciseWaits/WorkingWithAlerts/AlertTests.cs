using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace WorkingWithAlerts
{
        [TestFixture]
        public class AlertTests
        {
            IWebDriver driver;

        [SetUp]
        public void Setup()
        {
        
            var options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
        }

        [Test, Order(1)]
            public void HandleBasicAlert()
            {
                driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";

                driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Alert')]")).Click();

                IAlert alert = driver.SwitchTo().Alert();

                Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "Alert text is not as expected.");

                alert.Accept();

                IWebElement resultElement = driver.FindElement(By.Id("result"));
                Assert.That(resultElement.Text, Is.EqualTo("You successfully clicked an alert"), 
                    "Result message is not as expected.");
            }

            [Test, Order(2)]
            public void HandleConfirmAlert()
            {
                driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";
                driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Confirm')]")).Click();

                IAlert alert = driver.SwitchTo().Alert();
                Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected.");
                alert.Accept();

                IWebElement resultElement = driver.FindElement(By.Id("result"));
                Assert.That(resultElement.Text, Is.EqualTo("You clicked: Ok"), 
                    "Result message is not as expected after accepting the alert.");
                driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Confirm')]")).Click();

                alert = driver.SwitchTo().Alert();
                alert.Dismiss();

                resultElement = driver.FindElement(By.Id("result"));
                Assert.That(resultElement.Text, Is.EqualTo("You clicked: Cancel"), 
                    "Result message is not as expected after dismissing the alert.");
            }

            [Test, Order(3)]
            public void HandlePromptAlert()
            {
                driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";
                driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Prompt')]")).Click();

                IAlert alert = driver.SwitchTo().Alert();

                Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert text is not as expected.");

                string inputText = "Hello there!";
                alert.SendKeys(inputText);
                alert.Accept();

                IWebElement resultElement = driver.FindElement(By.Id("result"));
                Assert.That(resultElement.Text, Is.EqualTo("You entered: " + inputText), 
                    "Result message is not as expected after entering text in the prompt.");
            }

            [TearDown]
            public void TearDown()
            {
                driver.Dispose();
            }
        }
    }
