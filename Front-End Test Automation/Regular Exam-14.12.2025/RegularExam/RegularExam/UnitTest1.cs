using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace RegularExam
{
    public class Tests
    {
        private IWebDriver driver;
        private readonly string BaseUrl = "https://d1dzr3dh7g0qgk.cloudfront.net";
        private string lastCreatedTaskName;
        private string lastCreatedTaskDescription;
        private Random random;

        [OneTimeSetUp]
        public void Setup()
        {
            random = new Random();
            var firefoxOptions = new FirefoxOptions();

            firefoxOptions.SetPreference("signon.rememberSignons", false);
            firefoxOptions.SetPreference("signon.autofillForms", false);
            firefoxOptions.SetPreference("browser.formfill.enable", false);
            firefoxOptions.SetPreference("signon.management.page.breach-alerts.enabled", false);
            firefoxOptions.SetPreference("signon.formlessCapture.enabled", false);
            firefoxOptions.SetPreference("signon.rememberSignons.visibilityToggle", false);

            driver = new FirefoxDriver(firefoxOptions);

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);


            //Login to the system
            driver.Navigate().GoToUrl(BaseUrl + "/User/LoginRegister");
            driver.FindElement(By.Id("tab-login")).Click();
            driver.FindElement(By.Id("loginName")).SendKeys("rally@test.com");
            driver.FindElement(By.Id("loginPassword")).SendKeys("123456");

            driver.FindElement(By.XPath("//button[@type='submit' and text()='Sign in']")).Click();
        }

        [Test, Order(1)]
        public void AddTaskWithoutName()
        {
            string InvalidTaskName = "";
            driver.FindElement(By.PartialLinkText("To-do")).Click();
            driver.FindElement(By.ClassName("btn-info")).Click();

            driver.FindElement(By.Id("form4Example1")).SendKeys(InvalidTaskName);
            driver.FindElement(By.XPath("//button[@type='submit' and text()='Create']")).Click();

            Assert.That(driver.PageSource.Contains("The TaskName field is required."), Is.True,
            "Error message 'The TaskName field is required.' should be displayed");

            Assert.That(driver.Url, Is.EqualTo("https://d1dzr3dh7g0qgk.cloudfront.net/Task/Create"));

        }

        [Test, Order(2)]
        public void AddTaskWithRandomName()
        {

            lastCreatedTaskName = "Name_" + random.Next(999, 99999).ToString();
            lastCreatedTaskDescription = "Description_Lorem ipsum dolor sit amet, consecte" + random.Next(1000000000, 2147483647).ToString();
            driver.FindElement(By.PartialLinkText("To-do")).Click();
            driver.FindElement(By.ClassName("btn-info")).Click();

            driver.FindElement(By.Id("form4Example1")).SendKeys(lastCreatedTaskName);
            driver.FindElement(By.Id("form4Example3")).SendKeys(lastCreatedTaskDescription);
            driver.FindElement(By.Id("datetimepicker1Input")).SendKeys("15/12/2025 11:05");
            driver.FindElement(By.Id("datetimepicker2Input")).SendKeys("25/12/2025 11:05");
            var statusSelect = driver.FindElement(By.CssSelector("select[name='Status']"));
            var select = new SelectElement(statusSelect);
            select.SelectByValue("10");
            driver.FindElement(By.XPath("//button[@type='submit' and text()='Create']")).Click();

            string expectedUrl = $"{BaseUrl}/Task/ToDo";
            Assert.That(driver.Url, Is.EqualTo(expectedUrl));

            var taskCard = driver.FindElements(By.CssSelector(".card.text-center"));
            var lastTaskCard = taskCard.Last();
            var lastTaskTitle = lastTaskCard.FindElement(By.CssSelector("h5.card-title"));

            Assert.That(lastTaskTitle.Text, Is.EqualTo(lastCreatedTaskName));

        }

        [Test, Order(3)]
        public void EditLastAddedTask()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Task/ToDo");

            var allTasks = driver.FindElements(By.CssSelector(".card.text-center"));
            string lastTaskNameBeforeEdit = allTasks.Last()
                                             .FindElement(By.CssSelector("h5.card-title"))
                                             .Text;


            var lastTaskCard = driver.FindElements(By.CssSelector(".card.text-center")).Last();
            var lastTaskEditButton = lastTaskCard.FindElement(By.XPath(".//a[@class='btn btn-info' and text()='Edit']"));
            lastTaskEditButton.Click();

            string editedTaskName = "EDITED_" + lastTaskNameBeforeEdit;

            driver.FindElement(By.Id("form4Example1")).Clear();
            driver.FindElement(By.Id("form4Example1")).SendKeys(editedTaskName);
            driver.FindElement(By.XPath("//button[@type='submit' and text()='Edit']")).Click();

            string expectedUrl = $"{BaseUrl}/Task/ToDo";
            Assert.That(driver.Url, Is.EqualTo(expectedUrl));

            var allTasksAfterEdit = driver.FindElements(By.CssSelector(".card.text-center"));
            string lastTaskNameAfterEdit = allTasksAfterEdit.Last()
                                             .FindElement(By.CssSelector("h5.card-title"))
                                             .Text;

            Assert.That(lastTaskNameAfterEdit, Is.EqualTo(editedTaskName));

        }

        [Test, Order(4)]
        public void MoveLastAddedTask()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Task/ToDo");

            var allTasks = driver.FindElements(By.CssSelector(".card.text-center"));
            string lastTaskName = allTasks.Last()
                                             .FindElement(By.CssSelector("h5.card-title"))
                                             .Text;


            var lastTaskCard = driver.FindElements(By.CssSelector(".card.text-center")).Last();
            var lastTaskEditButton = lastTaskCard.FindElement(By.XPath(".//a[@class='btn btn-info' and text()='Edit']"));
            lastTaskEditButton.Click();

            var statusSelect = driver.FindElement(By.CssSelector("select[name='Status']"));
            var select = new SelectElement(statusSelect);
            select.SelectByValue("20");
            driver.FindElement(By.XPath("//button[@type='submit' and text()='Edit']")).Click();

            Assert.That(driver.PageSource.Contains(lastTaskName), Is.False);



        }

        [Test, Order(5)]
        public void DeleteLastAddedTask()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Task/InProgress");

            var initialCount = driver.FindElements(By.CssSelector(".card.text-center")).Count();


            var lastTaskCard = driver.FindElements(By.CssSelector(".card.text-center")).Last();
            var lastTaskDeleteButton = lastTaskCard.FindElement(By.XPath(".//a[text()='Delete']"));
            lastTaskDeleteButton.Click();

            driver.FindElement(By.XPath("//button[text()='Yes']")).Click();
            string expectedUrl = $"{BaseUrl}/Task/InProgress";
            Assert.That(driver.Url, Is.EqualTo(expectedUrl));

            var countAfterDeletion = driver.FindElements(By.CssSelector(".card.text-center")).Count();

            Assert.That(countAfterDeletion, Is.EqualTo(initialCount - 1));

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}