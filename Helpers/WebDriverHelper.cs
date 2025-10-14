using Microsoft.Playwright;
using CandyMapperBDD.Config;

namespace CandyMapperBDD.Helpers
{
    public class WebDriverHelper
    {
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IPage? _page;
        private readonly AppSettings _appSettings;

        public WebDriverHelper()
        {
            _appSettings = ConfigurationManager.GetAppSettings();
        }

        public IPage Page
        {
            get
            {
                if (_page == null)
                {
                    InitializePlaywright().GetAwaiter().GetResult();
                }
                return _page!;
            }
        }

        private async Task InitializePlaywright()
        {
            _playwright = await Playwright.CreateAsync();
            
            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = _appSettings.BrowserSettings.HeadlessMode,
                Args = new[]
                {
                    "--no-sandbox",
                    "--disable-dev-shm-usage",
                    "--disable-gpu"
                }
            };

            _browser = await _playwright.Chromium.LaunchAsync(launchOptions);
            
            var contextOptions = new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = _appSettings.BrowserSettings.WindowSize.Width,
                    Height = _appSettings.BrowserSettings.WindowSize.Height
                }
            };

            var context = await _browser.NewContextAsync(contextOptions);
            context.SetDefaultTimeout(_appSettings.BrowserSettings.PageLoadTimeout * 1000); // Convert to milliseconds
            
            _page = await context.NewPageAsync();
        }

        public async Task NavigateToAsync(string url)
        {
            await Page.GotoAsync(url);
        }

        public void NavigateTo(string url)
        {
            NavigateToAsync(url).GetAwaiter().GetResult();
        }

        public void MaximizeWindow()
        {
            // Playwright handles viewport size in context options, this is for compatibility
        }

        public async Task<ILocator> WaitForElementAsync(string selector, int timeoutInSeconds = 10)
        {
            var locator = Page.Locator(selector);
            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeoutInSeconds * 1000
            });
            return locator;
        }

        public ILocator WaitForElement(string selector, int timeoutInSeconds = 10)
        {
            return WaitForElementAsync(selector, timeoutInSeconds).GetAwaiter().GetResult();
        }

        public async Task<ILocator> WaitForClickableElementAsync(string selector, int timeoutInSeconds = 10)
        {
            var locator = Page.Locator(selector);
            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeoutInSeconds * 1000
            });
            return locator;
        }

        public ILocator WaitForClickableElement(string selector, int timeoutInSeconds = 10)
        {
            return WaitForClickableElementAsync(selector, timeoutInSeconds).GetAwaiter().GetResult();
        }

        public async Task ClickElementAsync(string selector)
        {
            var locator = await WaitForClickableElementAsync(selector);
            await locator.ClickAsync();
        }

        public void ClickElement(string selector)
        {
            ClickElementAsync(selector).GetAwaiter().GetResult();
        }

        public async Task EnterTextAsync(string selector, string text)
        {
            var locator = await WaitForElementAsync(selector);
            await locator.ClearAsync();
            await locator.FillAsync(text);
        }

        public void EnterText(string selector, string text)
        {
            EnterTextAsync(selector, text).GetAwaiter().GetResult();
        }

        public async Task SelectFromDropdownAsync(string selector, string optionText)
        {
            var locator = await WaitForElementAsync(selector);
            await locator.SelectOptionAsync(new SelectOptionValue { Label = optionText });
        }

        public void SelectFromDropdown(string selector, string optionText)
        {
            SelectFromDropdownAsync(selector, optionText).GetAwaiter().GetResult();
        }

        public async Task SelectFromDropdownByValueAsync(string selector, string optionValue)
        {
            var locator = await WaitForElementAsync(selector);
            await locator.SelectOptionAsync(new SelectOptionValue { Value = optionValue });
        }

        public void SelectFromDropdownByValue(string selector, string optionValue)
        {
            SelectFromDropdownByValueAsync(selector, optionValue).GetAwaiter().GetResult();
        }

        public async Task<string> GetTextAsync(string selector)
        {
            var locator = await WaitForElementAsync(selector);
            return await locator.TextContentAsync() ?? string.Empty;
        }

        public string GetText(string selector)
        {
            return GetTextAsync(selector).GetAwaiter().GetResult();
        }

        public async Task<bool> IsElementDisplayedAsync(string selector, int timeoutInSeconds = 5)
        {
            try
            {
                var locator = Page.Locator(selector);
                await locator.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = timeoutInSeconds * 1000
                });
                return true;
            }
            catch (TimeoutException)
            {
                return false;
            }
        }

        public bool IsElementDisplayed(string selector, int timeoutInSeconds = 5)
        {
            return IsElementDisplayedAsync(selector, timeoutInSeconds).GetAwaiter().GetResult();
        }

        public async Task WaitForPageToLoadAsync(int timeoutInSeconds = 30)
        {
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions
            {
                Timeout = timeoutInSeconds * 1000
            });
        }

        public void WaitForPageToLoad(int timeoutInSeconds = 30)
        {
            WaitForPageToLoadAsync(timeoutInSeconds).GetAwaiter().GetResult();
        }

        public async Task ScrollToElementAsync(string selector)
        {
            var locator = Page.Locator(selector);
            await locator.ScrollIntoViewIfNeededAsync();
        }

        public void ScrollToElement(string selector)
        {
            ScrollToElementAsync(selector).GetAwaiter().GetResult();
        }

        public async Task QuitDriverAsync()
        {
            if (_page != null)
            {
                await _page.CloseAsync();
                _page = null;
            }
            
            if (_browser != null)
            {
                await _browser.CloseAsync();
                _browser = null;
            }
            
            if (_playwright != null)
            {
                _playwright.Dispose();
                _playwright = null;
            }
        }

        public void QuitDriver()
        {
            QuitDriverAsync().GetAwaiter().GetResult();
        }

        // Additional methods for negative testing
        public void SetKeyboardOnlyMode(bool enabled)
        {
            // Enable keyboard-only navigation mode
            // This is a conceptual method - actual implementation would depend on specific requirements
        }

        public async Task DisableJavaScriptAsync()
        {
            if (_browser != null)
            {
                var contextOptions = new BrowserNewContextOptions
                {
                    JavaScriptEnabled = false,
                    ViewportSize = new ViewportSize
                    {
                        Width = _appSettings.BrowserSettings.WindowSize.Width,
                        Height = _appSettings.BrowserSettings.WindowSize.Height
                    }
                };

                var context = await _browser.NewContextAsync(contextOptions);
                _page = await context.NewPageAsync();
            }
        }

        public void DisableJavaScript()
        {
            DisableJavaScriptAsync().GetAwaiter().GetResult();
        }
    }
}