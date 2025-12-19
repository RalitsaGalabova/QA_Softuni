using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SearchWithImlicitWait
{
    public class ImplicitWaitSearchProduct
    {
        [TestFixture]
        public class ImplicitWaitTests
        {
            IWebDriver driver;

            [SetUp]
            public void Setup()
            {
                driver = new ChromeDriver();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Url = "http://practice.bpbonline.com/";
            }

            [Test, Order(1)]
            public void SearchProduct_Keyboard_ShouldAddToCart()
            {
                driver.FindElement(By.Name("keywords")).SendKeys("keyboard");
                driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

                try
                {

                    driver.FindElement(By.LinkText("Buy Now")).Click();
                    Assert.That(driver.PageSource, Does.Contain("keyboard"),
                                "The product 'keyboard' was not found in the cart page.");
                    Console.WriteLine("Scenario completed");
                }
                catch (Exception ex)
                {
                    Assert.Fail("Unexpected exception: " + ex.Message);
                }
            }

            [Test, Order(2)]
            public void SearchProduct_Junk_ShouldThrowNoSuchElementException()
            {
                driver.FindElement(By.Name("keywords")).SendKeys("junk");
                driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

                try
                {
                    driver.FindElement(By.LinkText("Buy Now")).Click();
                }
                catch (NoSuchElementException ex)
                {
                    Assert.Pass("Expected NoSuchElementException was thrown.");
                    Console.WriteLine("Timeout - " + ex.Message);
                }
                catch (Exception ex)
                {
                    Assert.Fail("Unexpected exception: " + ex.Message);
                }
            }

            [TearDown]
            public void TearDown()
            {
                driver.Dispose();
            }
        }
    }
}