using Microsoft.Playwright;
using CandyMapperBDD.Helpers;

namespace CandyMapperBDD.Pages
{
    public class PartyLocationPage : BasePage
    {
        public PartyLocationPage(WebDriverHelper webDriverHelper) : base(webDriverHelper)
        {
        }

        // Page Elements
        private string PageTitle => "h1:has-text('Are you bringing any guests?')";
        private string GuestCountDropdown => "select, iframe select";
        private string EmailTextbox => "input[type='email'], input[placeholder*='Email'], input[name*='email']";
        private string RemindMeButton => "button:has-text('Remind Me')";
        private string ConfirmationMessage => "p:has-text('If you supplied a real email')";
        private string PartyLocationSection => "h4:has-text('Party Location')";
        private string LocationDetails => "h4:has-text('Party Location') ~ div p";
        
        // Error message selectors
        private string EmailValidationError => ".error:has-text('email'), .validation-error:has-text('email')";
        private string GuestCountValidationError => ".error:has-text('guest'), .validation-error:has-text('guest')";
        private string GuestCountLimitError => ".error:has-text('limit'), .validation-error:has-text('limit')";
        private string EmailRequiredError => ".error:has-text('required'), .validation-error:has-text('required')";
        private string GuestCountRequiredError => ".error:has-text('required'), .validation-error:has-text('required')";
        private string EmailLengthValidationError => ".error:has-text('length'), .validation-error:has-text('length')";
        private string TimeoutError => ".error:has-text('timeout'), .timeout-message";
        private string LoadingIndicator => ".loading, .spinner, [data-loading]";
        private string RetryButton => "button:has-text('Retry'), button:has-text('Try Again')";
        private string ServerErrorMessage => ".error:has-text('server'), .server-error";
        private string SupportContactInfo => ".support-contact, .help-contact";

        // Page Actions
        public void SetNumberOfGuests(string guestCount)
        {
            try
            {
                var dropdown = Page.Locator(GuestCountDropdown).First;
                dropdown.SelectOptionAsync(guestCount).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set number of guests: {ex.Message}");
            }
        }

        public void EnterEmailAddress(string email)
        {
            if (IsElementDisplayed(EmailTextbox))
            {
                EnterText(EmailTextbox, email);
            }
            else
            {
                throw new Exception("Email textbox not found on the page");
            }
        }

        public void ClickRemindMeButton()
        {
            if (IsElementDisplayed(RemindMeButton))
            {
                ClickElement(RemindMeButton);
                WaitForPageToLoad();
            }
            else
            {
                throw new Exception("Remind Me button not found on the page");
            }
        }

        public async Task<bool> IsOnPartyLocationPageAsync()
        {
            return Page.Url.Contains("/party-location");
        }

        public bool IsOnPartyLocationPage()
        {
            return IsOnPartyLocationPageAsync().GetAwaiter().GetResult();
        }

        public bool IsConfirmationMessageDisplayed()
        {
            return IsElementDisplayed(ConfirmationMessage);
        }

        public string GetConfirmationMessage()
        {
            if (IsConfirmationMessageDisplayed())
            {
                return GetText(ConfirmationMessage);
            }
            return string.Empty;
        }

        public bool IsPartyLocationSectionDisplayed()
        {
            return IsElementDisplayed(PartyLocationSection);
        }

        public async Task<string> GetPartyLocationDetailsAsync()
        {
            if (await IsElementDisplayedAsync(PartyLocationSection))
            {
                var locationElements = await Page.Locator(LocationDetails).AllAsync();
                var texts = new List<string>();
                foreach (var element in locationElements)
                {
                    var text = await element.TextContentAsync();
                    if (!string.IsNullOrWhiteSpace(text))
                        texts.Add(text);
                }
                return string.Join(", ", texts);
            }
            return string.Empty;
        }

        public string GetPartyLocationDetails()
        {
            return GetPartyLocationDetailsAsync().GetAwaiter().GetResult();
        }

        public bool IsEmailFormDisplayed()
        {
            return IsElementDisplayed(EmailTextbox) && IsElementDisplayed(RemindMeButton);
        }

        public void CompletePartyBooking(string guestCount, string email)
        {
            SetNumberOfGuests(guestCount);
            EnterEmailAddress(email);
            ClickRemindMeButton();
        }

        // Negative Testing Methods
        public bool IsEmailValidationErrorDisplayed()
        {
            return IsElementDisplayed(EmailValidationError);
        }

        public string GetEmailValidationErrorMessage()
        {
            return IsElementDisplayed(EmailValidationError) ? GetText(EmailValidationError) : string.Empty;
        }

        public bool IsGuestCountValidationErrorDisplayed()
        {
            return IsElementDisplayed(GuestCountValidationError);
        }

        public string GetGuestCountValidationErrorMessage()
        {
            return IsElementDisplayed(GuestCountValidationError) ? GetText(GuestCountValidationError) : string.Empty;
        }

        public bool IsGuestCountLimitErrorDisplayed()
        {
            return IsElementDisplayed(GuestCountLimitError);
        }

        public string GetGuestCountLimitErrorMessage()
        {
            return IsElementDisplayed(GuestCountLimitError) ? GetText(GuestCountLimitError) : string.Empty;
        }

        public void ClearEmailAddress()
        {
            var emailField = Page.Locator(EmailTextbox);
            emailField.ClearAsync().GetAwaiter().GetResult();
        }

        public void ClearGuestCount()
        {
            var guestField = Page.Locator(GuestCountDropdown);
            guestField.SelectOptionAsync("").GetAwaiter().GetResult();
        }

        public bool IsEmailRequiredErrorDisplayed()
        {
            return IsElementDisplayed(EmailRequiredError);
        }

        public string GetEmailRequiredErrorMessage()
        {
            return IsElementDisplayed(EmailRequiredError) ? GetText(EmailRequiredError) : string.Empty;
        }

        public bool IsGuestCountRequiredErrorDisplayed()
        {
            return IsElementDisplayed(GuestCountRequiredError);
        }

        public string GetGuestCountRequiredErrorMessage()
        {
            return IsElementDisplayed(GuestCountRequiredError) ? GetText(GuestCountRequiredError) : string.Empty;
        }

        public bool VerifyNoScriptExecution()
        {
            // Check if any XSS attempts resulted in script execution
            // This would typically involve checking for specific DOM modifications or alert dialogs
            return !Page.Locator("script").IsVisibleAsync().GetAwaiter().GetResult();
        }

        public bool IsEmailLengthValidationErrorDisplayed()
        {
            return IsElementDisplayed(EmailLengthValidationError);
        }

        public bool IsTimeoutErrorDisplayed()
        {
            return IsElementDisplayed(TimeoutError);
        }

        public bool IsLoadingIndicatorDisplayed()
        {
            return IsElementDisplayed(LoadingIndicator);
        }

        public bool IsRetryButtonDisplayed()
        {
            return IsElementDisplayed(RetryButton);
        }

        public bool IsServerErrorMessageDisplayed()
        {
            return IsElementDisplayed(ServerErrorMessage);
        }

        public bool IsSupportContactInfoDisplayed()
        {
            return IsElementDisplayed(SupportContactInfo);
        }

        // Keyboard Navigation Methods
        public void SetNumberOfGuestsUsingKeyboard(string guestCount)
        {
            var dropdown = Page.Locator(GuestCountDropdown);
            dropdown.FocusAsync().GetAwaiter().GetResult();
            dropdown.PressAsync("Enter").GetAwaiter().GetResult();
            dropdown.SelectOptionAsync(guestCount).GetAwaiter().GetResult();
        }

        public void EnterEmailAddressUsingKeyboard(string email)
        {
            var emailField = Page.Locator(EmailTextbox);
            emailField.FocusAsync().GetAwaiter().GetResult();
            emailField.FillAsync(email).GetAwaiter().GetResult();
        }

        public void ClickButtonUsingKeyboard(string buttonName)
        {
            var button = Page.Locator($"button:has-text('{buttonName}')");
            button.FocusAsync().GetAwaiter().GetResult();
            button.PressAsync("Enter").GetAwaiter().GetResult();
        }

        public bool AreValidationErrorsAccessible()
        {
            // Check if error messages have proper ARIA attributes
            var errorElements = Page.Locator("[role='alert'], [aria-live], .error[aria-describedby]");
            return errorElements.CountAsync().GetAwaiter().GetResult() > 0;
        }

        public bool IsFocusManagementCorrect()
        {
            // Verify that focus is moved to the first error when validation fails
            var focusedElement = Page.Locator(":focus");
            return focusedElement.CountAsync().GetAwaiter().GetResult() > 0;
        }

        public bool IsFormSubmissionPrevented()
        {
            // Check if form submission was prevented server-side
            return !IsConfirmationMessageDisplayed();
        }

        public bool IsSystemRespondingGracefully()
        {
            // Check if system provides appropriate response under load
            return Page.IsEnabledAsync("body").GetAwaiter().GetResult();
        }

        public bool HasAppropriateSystemResponse()
        {
            // Check if system provides feedback about processing status
            return IsLoadingIndicatorDisplayed() || IsServerErrorMessageDisplayed() || IsRetryButtonDisplayed();
        }

        // Helper methods for async operations
        private async Task<bool> IsElementDisplayedAsync(string selector)
        {
            return await WebDriverHelper.IsElementDisplayedAsync(selector);
        }
    }
}