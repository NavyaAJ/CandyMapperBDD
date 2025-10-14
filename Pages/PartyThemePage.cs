using Microsoft.Playwright;
using CandyMapperBDD.Helpers;

namespace CandyMapperBDD.Pages
{
    public class PartyThemePage : BasePage
    {
        public PartyThemePage(WebDriverHelper webDriverHelper) : base(webDriverHelper)
        {
        }

        // Page Elements
        private string PageTitle => "h1:has-text('Party Theme')";
        private string ZombiesThemeLink => "text=Zombies";
        private string GhostsThemeLink => "text=Ghosts";
        private string ZombiesThemeLinkAlternative => "a[href='/party-location']";
        private string ThemeSelectionError => ".error:has-text('theme'), .validation-error:has-text('theme')";
        private string ContinueButton => "button:has-text('Continue'), button:has-text('Next')";

        // Page Actions
        public void SelectZombiesTheme()
        {
            if (IsElementDisplayed(ZombiesThemeLink))
            {
                ClickElement(ZombiesThemeLink);
            }
            else if (IsElementDisplayed(ZombiesThemeLinkAlternative))
            {
                ClickElement(ZombiesThemeLinkAlternative);
            }
            else
            {
                throw new Exception("Zombies theme option not found on the page");
            }
            WaitForPageToLoad();
        }

        public void SelectGhostsTheme()
        {
            ClickElement(GhostsThemeLink);
            WaitForPageToLoad();
        }

        public async Task<bool> IsOnPartyThemePageAsync()
        {
            return Page.Url.Contains("/host-a-party") && 
                   await IsElementDisplayedAsync(PageTitle);
        }

        public bool IsOnPartyThemePage()
        {
            return IsOnPartyThemePageAsync().GetAwaiter().GetResult();
        }

        public string GetPageTitle()
        {
            return GetText(PageTitle);
        }

        public bool AreThemeOptionsDisplayed()
        {
            return IsElementDisplayed(ZombiesThemeLink) && 
                   IsElementDisplayed(GhostsThemeLink);
        }

        public async Task<List<string>> GetAvailableThemesAsync()
        {
            var themes = new List<string>();
            
            if (await IsElementDisplayedAsync(ZombiesThemeLink))
            {
                themes.Add("Zombies");
            }
            
            if (await IsElementDisplayedAsync(GhostsThemeLink))
            {
                themes.Add("Ghosts");
            }
            
            return themes;
        }

        public List<string> GetAvailableThemes()
        {
            return GetAvailableThemesAsync().GetAwaiter().GetResult();
        }

        // Negative Testing Methods
        public void AttemptToProceedWithoutThemeSelection()
        {
            if (IsElementDisplayed(ContinueButton))
            {
                ClickElement(ContinueButton);
            }
        }

        public bool IsThemeSelectionErrorDisplayed()
        {
            return IsElementDisplayed(ThemeSelectionError);
        }

        public string GetThemeSelectionErrorMessage()
        {
            return IsElementDisplayed(ThemeSelectionError) ? GetText(ThemeSelectionError) : string.Empty;
        }

        // Keyboard Navigation Methods
        public void SelectThemeUsingKeyboard(string theme)
        {
            string selector = theme.ToLowerInvariant() switch
            {
                "zombies" => ZombiesThemeLink,
                "ghosts" => GhostsThemeLink,
                _ => throw new ArgumentException($"Unknown theme: {theme}")
            };

            var themeLink = Page.Locator(selector);
            themeLink.FocusAsync().GetAwaiter().GetResult();
            themeLink.PressAsync("Enter").GetAwaiter().GetResult();
        }

        // Helper methods for async operations
        private async Task<bool> IsElementDisplayedAsync(string selector)
        {
            return await WebDriverHelper.IsElementDisplayedAsync(selector);
        }
    }
}