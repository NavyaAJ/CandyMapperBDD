using Microsoft.Playwright;
using CandyMapperBDD.Helpers;
using CandyMapperBDD.Config;

namespace CandyMapperBDD.Pages
{
    public abstract class BasePage
    {
        protected WebDriverHelper WebDriverHelper;
        protected AppSettings AppSettings;

        protected BasePage(WebDriverHelper webDriverHelper)
        {
            WebDriverHelper = webDriverHelper;
            AppSettings = ConfigurationManager.GetAppSettings();
        }

        protected IPage Page => WebDriverHelper.Page;

        protected void ClickElement(string selector)
        {
            WebDriverHelper.ClickElement(selector);
        }

        protected void EnterText(string selector, string text)
        {
            WebDriverHelper.EnterText(selector, text);
        }

        protected void SelectFromDropdown(string selector, string optionText)
        {
            WebDriverHelper.SelectFromDropdown(selector, optionText);
        }

        protected void SelectFromDropdownByValue(string selector, string optionValue)
        {
            WebDriverHelper.SelectFromDropdownByValue(selector, optionValue);
        }

        protected string GetText(string selector)
        {
            return WebDriverHelper.GetText(selector);
        }

        protected bool IsElementDisplayed(string selector)
        {
            return WebDriverHelper.IsElementDisplayed(selector);
        }

        protected void WaitForElement(string selector)
        {
            WebDriverHelper.WaitForElement(selector);
        }

        protected void ScrollToElement(string selector)
        {
            WebDriverHelper.ScrollToElement(selector);
        }

        public virtual void WaitForPageToLoad()
        {
            WebDriverHelper.WaitForPageToLoad();
        }
    }
}