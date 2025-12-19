using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace WorkingWithWindows
{
    [TestFixture]
    public class SwitchingWindowsTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test, Order(1)]
        public void HandleMultipleWindows()
        {
            driver.Url = "https://the-internet.herokuapp.com/windows";
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
            Assert.That(windowHandles.Count, Is.EqualTo(2));

            driver.SwitchTo().Window(windowHandles[1]);
            string newWindowContent = driver.PageSource;
            Assert.That(newWindowContent, Does.Contain("New Window"));

            string path = Path.Combine(Directory.GetCurrentDirectory(), "windows.txt");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.AppendAllText(path, "Window handle for new window: "
                + driver.CurrentWindowHandle + "\n\n");
            File.AppendAllText(path, "The page content: " + newWindowContent + "\n\n");

            driver.Close();
            driver.SwitchTo().Window(windowHandles[0]);

            string originalWindowContent = driver.PageSource;
            Assert.That(originalWindowContent, Does.Contain("Opening a new window"));

            File.AppendAllText(path, "Window handle for original window: "
                + driver.CurrentWindowHandle + "\n\n");
            File.AppendAllText(path, "The page content: " + originalWindowContent + "\n\n");
        }

        [Test, Order(2)]
        public void HandleNoSuchWindowException()
        {
            driver.Url = "https://the-internet.herokuapp.com/windows";
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
            driver.SwitchTo().Window(windowHandles[1]);
            driver.Close();

            try
            {
                driver.SwitchTo().Window(windowHandles[1]);
            }
            catch (NoSuchWindowException ex)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "windows.txt");
                File.AppendAllText(path, "NoSuchWindowException caught: " + ex.Message + "\n\n");
                Assert.Pass("NoSuchWindowException was correctly handled.");
            }
            catch (Exception ex)
            {
                Assert.Fail("An unexpected exception was thrown: " + ex.Message);
            }
            finally
            {
                driver.SwitchTo().Window(windowHandles[0]);
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}