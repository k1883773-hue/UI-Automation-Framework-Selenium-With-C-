using log4net;
using log4net.Config;
using NaveenAutomationPOM.DataFactory.LoginData;
using NaveenAutomationPOM.PageObjects;
using NaveenAutomationPOM.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NaveenAutomationPOM.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected IWebDriver? driver;
        protected ILog? log;
        public required TestContext TestContext { get; set; }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            ReportManager.InitReport();
        }

        [TestInitialize]
        public void Setup()
        {
            log = LogManager.GetLogger(this.GetType());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            driver.Navigate().GoToUrl(Config.BaseUrl);
            ReportManager.CreateTest(TestContext.TestName);
        }

        protected void LoginToApplication()
        {
            //PO
            var home = new HomePO(driver);
            var login = new LoginPO(driver);

            //DTO
            var loginWithValidCredentials = LoginData.GetLoginWithValidCredentials();

            home.SelectAccountDropdownOption("Login");
            Assert.AreEqual("Login", home.GetCurrentPageText(), "Not navigate to login page");
            login.LoginWithCredentials(loginWithValidCredentials.Email, loginWithValidCredentials.Password);
            Assert.AreEqual("Account", home.GetCurrentPageText(), "Not navigate to home page after login");
        }

        [TestCleanup]
        public void TearDown()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                ReportManager.CaptureScreenshot(driver!, TestContext.TestName);
            }
            driver?.Quit();
        }

        [AssemblyCleanup]
        public static void EndReport()
        {
            ReportManager.FlushReport();
        }
    }
}