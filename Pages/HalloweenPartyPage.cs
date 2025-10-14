using Microsoft.Playwright;
using CandyMapperBDD.Helpers;

namespace CandyMapperBDD.Pages
{
    public class HalloweenPartyPage : BasePage
    {
        public HalloweenPartyPage(WebDriverHelper webDriverHelper) : base(webDriverHelper)
        {
        }

                // Page Elements
        private string PageTitle => "h1:has-text('Halloween Party')";
        private string IAmHostingAPartyLink => "text=I Am Hosting A Party";
        private string IAmAttendingAPartyLink => "text=I Am Attending A Party";
        private string IAmHostingAPartyLinkAlternative => "a[href='/host-a-party-1']";
        private string PartyOptionSelectionError => ".error:has-text('option'), .validation-error:has-text('option')";
        private string ContinueButton => "button:has-text('Continue'), button:has-text('Next')";

        // Page Actions
        public void ClickIAmHostingAParty()
        {
            if (IsElementDisplayed(IAmHostingAPartyLink))
            {
                ClickElement(IAmHostingAPartyLink);
            }
            else if (IsElementDisplayed(IAmHostingAPartyLinkAlternative))
            {
                ClickElement(IAmHostingAPartyLinkAlternative);
            }
            else
            {
                throw new Exception("'I Am Hosting A Party' link not found on the page");
            }
            WaitForPageToLoad();
        }

        public void ClickIAmAttendingAParty()
        {
            ClickElement(IAmAttendingAPartyLink);
            WaitForPageToLoad();
        }

        public async Task<bool> IsOnHalloweenPartyPageAsync()
        {
            var url = Page.Url;
            return url.Contains("/halloween-party") && 
                   await IsElementDisplayedAsync(PageTitle);
        }

        public bool IsOnHalloweenPartyPage()
        {
            return IsOnHalloweenPartyPageAsync().GetAwaiter().GetResult();
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

        public bool ArePartyOptionsDisplayed()
        {
            return IsElementDisplayed(IAmHostingAPartyLink) && 
                   IsElementDisplayed(IAmAttendingAPartyLink);
        }

        // Negative Testing Methods
        public void AttemptToProceedWithoutPartyOptionSelection()
        {
            if (IsElementDisplayed(ContinueButton))
            {
                ClickElement(ContinueButton);
            }
        }

        public bool IsPartyOptionSelectionErrorDisplayed()
        {
            return IsElementDisplayed(PartyOptionSelectionError);
        }

        public string GetPartyOptionSelectionErrorMessage()
        {
            return IsElementDisplayed(PartyOptionSelectionError) ? GetText(PartyOptionSelectionError) : string.Empty;
        }

        // Keyboard Navigation Methods
        public void ClickIAmHostingAPartyUsingKeyboard()
        {
            var hostingLink = Page.Locator(IAmHostingAPartyLink);
            hostingLink.FocusAsync().GetAwaiter().GetResult();
            hostingLink.PressAsync("Enter").GetAwaiter().GetResult();
        }

        // Helper methods for async operations
        private async Task<bool> IsElementDisplayedAsync(string selector)
        {
            return await WebDriverHelper.IsElementDisplayedAsync(selector);
        }

        private async Task<string> GetTextAsync(string selector)
        {
            return await WebDriverHelper.GetTextAsync(selector);
        }


    }
}