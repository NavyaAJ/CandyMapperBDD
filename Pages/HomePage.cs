using Microsoft.Playwright;
using CandyMapperBDD.Helpers;

namespace CandyMapperBDD.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(WebDriverHelper webDriverHelper) : base(webDriverHelper)
        {
        }

        // Page Elements
        private string HalloweenPartyLink => "text=Halloween Party";
        private string HalloweenPartyLinkAlternative => "a[href='/halloween-party']";
        private string PageTitle => "h1";

        // Page Actions
        public void NavigateToHomePage()
        {
            WebDriverHelper.NavigateTo(AppSettings.ApplicationSettings.BaseUrl);
            WaitForPageToLoad();
        }

        public void ClickHalloweenPartyLink()
        {
            // Try both possible selectors for the Halloween Party link
            if (IsElementDisplayed(HalloweenPartyLink))
            {
                ClickElement(HalloweenPartyLink);
            }
            else if (IsElementDisplayed(HalloweenPartyLinkAlternative))
            {
                ClickElement(HalloweenPartyLinkAlternative);
            }
            else
            {
                throw new Exception("Halloween Party link not found on the page");
            }
        }

        public async Task<bool> IsOnHomePageAsync()
        {
            var url = Page.Url;
            return url.Contains("candymapper.com") && 
                   (url == AppSettings.ApplicationSettings.BaseUrl || 
                    url == AppSettings.ApplicationSettings.BaseUrl.TrimEnd('/'));
        }

        public bool IsOnHomePage()
        {
            return IsOnHomePageAsync().GetAwaiter().GetResult();
        }

        public async Task<string> GetPageTitleAsync()
        {
            if (await IsElementDisplayedAsync(PageTitle))
            {
                return await GetTextAsync(PageTitle);
            }
            return await Page.TitleAsync();
        }

        public string GetPageTitle()
        {
            return GetPageTitleAsync().GetAwaiter().GetResult();
        }

        private async Task<bool> IsElementDisplayedAsync(string selector)
        {
            return await WebDriverHelper.IsElementDisplayedAsync(selector);
        }

        private async Task<string> GetTextAsync(string selector)
        {
            return await WebDriverHelper.GetTextAsync(selector);
        }

        // Keyboard Navigation Methods
        public void ClickHalloweenPartyLinkUsingKeyboard()
        {
            var partyLink = Page.Locator(HalloweenPartyLink);
            partyLink.FocusAsync().GetAwaiter().GetResult();
            partyLink.PressAsync("Enter").GetAwaiter().GetResult();
        }
    }
}