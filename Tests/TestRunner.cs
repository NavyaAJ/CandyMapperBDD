using NUnit.Framework;
using CandyMapperBDD.Helpers;
using CandyMapperBDD.Config;
using TestContext = CandyMapperBDD.Helpers.TestContext;

namespace CandyMapperBDD.Tests
{
    [TestFixture]
    public class TestRunner
    {
        private TestContext? _testContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            LogHelper.LogInfo("Starting CandyMapper BDD Test Suite");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            LogHelper.LogInfo("Completed CandyMapper BDD Test Suite");
        }

        [SetUp]
        public void SetUp()
        {
            _testContext = new TestContext();
            LogHelper.LogTestStart(NUnit.Framework.TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            LogHelper.LogTestEnd(NUnit.Framework.TestContext.CurrentContext.Test.Name);
            _testContext?.CleanUp();
            _testContext = null;
        }

        [Test]
        [Category("smoke")]
        public void VerifyFrameworkSetup()
        {
            // This test verifies that the framework is properly set up
            Assert.That(_testContext, Is.Not.Null, "Test context should be initialized");
            Assert.That(_testContext!.WebDriverHelper, Is.Not.Null, "WebDriver helper should be initialized");
            Assert.That(_testContext.HomePage, Is.Not.Null, "Home page should be initialized");
            
            LogHelper.LogInfo("Framework setup verification completed successfully");
        }

        [Test]
        [Category("configuration")]
        public void VerifyConfiguration()
        {
            // This test verifies that configuration is loaded correctly
            var appSettings = ConfigurationManager.GetAppSettings();
            
            Assert.That(appSettings, Is.Not.Null, "App settings should be loaded");
            Assert.That(appSettings.ApplicationSettings.BaseUrl, Is.Not.Empty, "Base URL should be configured");
            Assert.That(appSettings.BrowserSettings.BrowserType, Is.Not.Empty, "Browser type should be configured");
            
            LogHelper.LogInfo($"Base URL: {appSettings.ApplicationSettings.BaseUrl}");
            LogHelper.LogInfo($"Browser: {appSettings.BrowserSettings.BrowserType}");
            LogHelper.LogInfo("Configuration verification completed successfully");
        }
    }
}