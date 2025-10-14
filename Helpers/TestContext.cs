using CandyMapperBDD.Helpers;
using CandyMapperBDD.Pages;

namespace CandyMapperBDD.Helpers
{
    public class TestContext
    {
        public WebDriverHelper WebDriverHelper { get; private set; }
        public HomePage HomePage { get; private set; }
        public HalloweenPartyPage HalloweenPartyPage { get; private set; }
        public PartyThemePage PartyThemePage { get; private set; }
        public PartyLocationPage PartyLocationPage { get; private set; }

        public TestContext()
        {
            InitializePages();
        }

        private void InitializePages()
        {
            WebDriverHelper = new WebDriverHelper();
            HomePage = new HomePage(WebDriverHelper);
            HalloweenPartyPage = new HalloweenPartyPage(WebDriverHelper);
            PartyThemePage = new PartyThemePage(WebDriverHelper);
            PartyLocationPage = new PartyLocationPage(WebDriverHelper);
        }

        public void CleanUp()
        {
            WebDriverHelper?.QuitDriver();
        }
    }
}