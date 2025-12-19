using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace WorkingWithIFrames
{
    public class IFrameTests
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
        }

        [Test, Order(1)]
        public void TestFrameByIndex()
        {
            driver.Url = "https://codepen.io/pervillalva/full/abPoNLd";
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));

            var dropdownButton = wait.Until(ExpectedConditions
                .ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.That(link.Displayed, Is.True, "Link inside the dropdown is not displayed as expected.");
            }

            driver.SwitchTo().DefaultContent();
        }


        [Test, Order(2)]
        public void TestFrameById()
        {
            driver.Url = "https://codepen.io/pervillalva/full/abPoNLd";

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("result"));

            var dropdownButton = wait.Until(ExpectedConditions
                .ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.That(link.Displayed, Is.True, "Link inside the dropdown is not displayed as expected.");
            }

            driver.SwitchTo().DefaultContent();
        }

        [Test, Order(3)]
        public void TestFrameByWebElement()
        {
            driver.Url = "https://codepen.io/pervillalva/full/abPoNLd";

            var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result")));

            driver.SwitchTo().Frame(frameElement);

            var dropdownButton = wait.Until(ExpectedConditions
                .ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.That(link.Displayed, Is.True, "Link inside the dropdown is not displayed as expected.");
            }

            driver.SwitchTo().DefaultContent();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}