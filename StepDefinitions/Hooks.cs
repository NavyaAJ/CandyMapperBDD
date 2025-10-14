using TechTalk.SpecFlow;
using CandyMapperBDD.Helpers;
using TestContext = CandyMapperBDD.Helpers.TestContext;

namespace CandyMapperBDD.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        private static TestContext? _testContext;

        [BeforeScenario]
        public static void BeforeScenario()
        {
            _testContext = new TestContext();
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            _testContext?.CleanUp();
            _testContext = null;
        }

        public static TestContext GetTestContext()
        {
            if (_testContext == null)
            {
                throw new InvalidOperationException("Test context is not initialized. Make sure BeforeScenario hook is executed.");
            }
            return _testContext;
        }
    }
}