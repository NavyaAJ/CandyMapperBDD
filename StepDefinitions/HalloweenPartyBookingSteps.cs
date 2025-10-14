using TechTalk.SpecFlow;
using FluentAssertions;
using CandyMapperBDD.Helpers;
using TestContext = CandyMapperBDD.Helpers.TestContext;

namespace CandyMapperBDD.StepDefinitions
{
    [Binding]
    public class HalloweenPartyBookingSteps
    {
        private readonly TestContext _testContext;

        public HalloweenPartyBookingSteps()
        {
            _testContext = Hooks.GetTestContext();
        }

        [Given(@"I am on the CandyMapper home page")]
        public void GivenIAmOnTheCandyMapperHomePage()
        {
            _testContext.HomePage.NavigateToHomePage();
            _testContext.WebDriverHelper.MaximizeWindow();
            _testContext.HomePage.IsOnHomePage().Should().BeTrue("User should be on the home page");
        }

        [When(@"I click on the Halloween Party link")]
        public void WhenIClickOnTheHalloweenPartyLink()
        {
            _testContext.HomePage.ClickHalloweenPartyLink();
        }

        [Then(@"I should be on the Halloween Party page")]
        public void ThenIShouldBeOnTheHalloweenPartyPage()
        {
            _testContext.HalloweenPartyPage.IsOnHalloweenPartyPage().Should().BeTrue("User should be on the Halloween Party page");
            _testContext.HalloweenPartyPage.ArePartyOptionsDisplayed().Should().BeTrue("Party options should be displayed");
        }

        [When(@"I select ""(.*)"" option")]
        public void WhenISelectOption(string option)
        {
            switch (option.ToLowerInvariant())
            {
                case "i am hosting a party":
                    _testContext.HalloweenPartyPage.ClickIAmHostingAParty();
                    break;
                case "i am attending a party":
                    _testContext.HalloweenPartyPage.ClickIAmAttendingAParty();
                    break;
                default:
                    throw new ArgumentException($"Unknown party option: {option}");
            }
        }

        [Then(@"I should be on the party theme selection page")]
        public void ThenIShouldBeOnThePartyThemeSelectionPage()
        {
            _testContext.PartyThemePage.IsOnPartyThemePage().Should().BeTrue("User should be on the party theme page");
            _testContext.PartyThemePage.AreThemeOptionsDisplayed().Should().BeTrue("Theme options should be displayed");
        }

        [When(@"I select the ""(.*)"" theme")]
        public void WhenISelectTheTheme(string theme)
        {
            switch (theme.ToLowerInvariant())
            {
                case "zombies":
                    _testContext.PartyThemePage.SelectZombiesTheme();
                    break;
                case "ghosts":
                    _testContext.PartyThemePage.SelectGhostsTheme();
                    break;
                default:
                    throw new ArgumentException($"Unknown theme: {theme}");
            }
        }

        [Then(@"I should be on the party location page")]
        public void ThenIShouldBeOnThePartyLocationPage()
        {
            _testContext.PartyLocationPage.IsOnPartyLocationPage().Should().BeTrue("User should be on the party location page");
        }

        [When(@"I set the number of guests to ""(.*)""")]
        public void WhenISetTheNumberOfGuestsTo(string guestCount)
        {
            _testContext.PartyLocationPage.SetNumberOfGuests(guestCount);
        }

        [When(@"I enter ""(.*)"" as the email address")]
        public void WhenIEnterAsTheEmailAddress(string email)
        {
            _testContext.PartyLocationPage.EnterEmailAddress(email);
        }

        [When(@"I click the ""(.*)"" button")]
        public void WhenIClickTheButton(string buttonName)
        {
            switch (buttonName.ToLowerInvariant())
            {
                case "remind me":
                    _testContext.PartyLocationPage.ClickRemindMeButton();
                    break;
                default:
                    throw new ArgumentException($"Unknown button: {buttonName}");
            }
        }

        [Then(@"I should see a confirmation message")]
        public void ThenIShouldSeeAConfirmationMessage()
        {
            _testContext.PartyLocationPage.IsConfirmationMessageDisplayed().Should().BeTrue("Confirmation message should be displayed");
            
            var confirmationMessage = _testContext.PartyLocationPage.GetConfirmationMessage();
            confirmationMessage.Should().NotBeNullOrEmpty("Confirmation message should have content");
            confirmationMessage.Should().Contain("email", "Confirmation message should mention email");
        }

        [Then(@"the party location details should be displayed")]
        public void ThenThePartyLocationDetailsShouldBeDisplayed()
        {
            _testContext.PartyLocationPage.IsPartyLocationSectionDisplayed().Should().BeTrue("Party location section should be displayed");
            
            var locationDetails = _testContext.PartyLocationPage.GetPartyLocationDetails();
            locationDetails.Should().NotBeNullOrEmpty("Party location details should be displayed");
        }

        [Given(@"I complete the Halloween party booking with ""(.*)"" guests and email ""(.*)""")]
        public void GivenICompleteTheHalloweenPartyBookingWithGuestsAndEmail(string guestCount, string email)
        {
            // Navigate to home page
            _testContext.HomePage.NavigateToHomePage();
            _testContext.WebDriverHelper.MaximizeWindow();

            // Navigate through the booking flow
            _testContext.HomePage.ClickHalloweenPartyLink();
            _testContext.HalloweenPartyPage.ClickIAmHostingAParty();
            _testContext.PartyThemePage.SelectZombiesTheme();
            
            // Complete the booking
            _testContext.PartyLocationPage.CompletePartyBooking(guestCount, email);
        }

        [Then(@"the Halloween party booking should be completed successfully")]
        public void ThenTheHalloweenPartyBookingShouldBeCompletedSuccessfully()
        {
            _testContext.PartyLocationPage.IsConfirmationMessageDisplayed().Should().BeTrue("Confirmation message should be displayed after booking");
            _testContext.PartyLocationPage.IsPartyLocationSectionDisplayed().Should().BeTrue("Party location should be confirmed");
        }

        // Negative Scenario Step Definitions

        [Then(@"I should see an email validation error message")]
        public void ThenIShouldSeeAnEmailValidationErrorMessage()
        {
            _testContext.PartyLocationPage.IsEmailValidationErrorDisplayed().Should().BeTrue("Email validation error should be displayed");
            var errorMessage = _testContext.PartyLocationPage.GetEmailValidationErrorMessage();
            errorMessage.Should().NotBeNullOrEmpty("Email validation error message should have content");
            errorMessage.Should().Contain("email", "Error message should mention email validation");
        }

        [Then(@"the party booking should not be completed")]
        public void ThenThePartyBookingShouldNotBeCompleted()
        {
            _testContext.PartyLocationPage.IsConfirmationMessageDisplayed().Should().BeFalse("Confirmation message should not be displayed for invalid input");
        }

        [Then(@"I should see a guest count validation error message")]
        public void ThenIShouldSeeAGuestCountValidationErrorMessage()
        {
            _testContext.PartyLocationPage.IsGuestCountValidationErrorDisplayed().Should().BeTrue("Guest count validation error should be displayed");
            var errorMessage = _testContext.PartyLocationPage.GetGuestCountValidationErrorMessage();
            errorMessage.Should().NotBeNullOrEmpty("Guest count validation error message should have content");
        }

        [Then(@"I should see a guest count limit error message")]
        public void ThenIShouldSeeAGuestCountLimitErrorMessage()
        {
            _testContext.PartyLocationPage.IsGuestCountLimitErrorDisplayed().Should().BeTrue("Guest count limit error should be displayed");
            var errorMessage = _testContext.PartyLocationPage.GetGuestCountLimitErrorMessage();
            errorMessage.Should().Contain("limit", "Error message should mention guest count limit");
        }

        [When(@"I leave the email address field empty")]
        public void WhenILeaveTheEmailAddressFieldEmpty()
        {
            _testContext.PartyLocationPage.ClearEmailAddress();
        }

        [When(@"I leave the guest count field empty")]
        public void WhenILeaveTheGuestCountFieldEmpty()
        {
            _testContext.PartyLocationPage.ClearGuestCount();
        }

        [Then(@"I should see a required field error message for email")]
        public void ThenIShouldSeeARequiredFieldErrorMessageForEmail()
        {
            _testContext.PartyLocationPage.IsEmailRequiredErrorDisplayed().Should().BeTrue("Email required error should be displayed");
            var errorMessage = _testContext.PartyLocationPage.GetEmailRequiredErrorMessage();
            errorMessage.Should().Contain("required", "Error message should indicate field is required");
        }

        [Then(@"I should see a required field error message for guest count")]
        public void ThenIShouldSeeARequiredFieldErrorMessageForGuestCount()
        {
            _testContext.PartyLocationPage.IsGuestCountRequiredErrorDisplayed().Should().BeTrue("Guest count required error should be displayed");
            var errorMessage = _testContext.PartyLocationPage.GetGuestCountRequiredErrorMessage();
            errorMessage.Should().Contain("required", "Error message should indicate field is required");
        }

        [When(@"I attempt to proceed without selecting a theme")]
        public void WhenIAttemptToProceedWithoutSelectingATheme()
        {
            _testContext.PartyThemePage.AttemptToProceedWithoutThemeSelection();
        }

        [Then(@"I should see a theme selection error message")]
        public void ThenIShouldSeeAThemeSelectionErrorMessage()
        {
            _testContext.PartyThemePage.IsThemeSelectionErrorDisplayed().Should().BeTrue("Theme selection error should be displayed");
            var errorMessage = _testContext.PartyThemePage.GetThemeSelectionErrorMessage();
            errorMessage.Should().Contain("theme", "Error message should mention theme selection");
        }

        [Then(@"I should remain on the party theme selection page")]
        public void ThenIShouldRemainOnThePartyThemeSelectionPage()
        {
            _testContext.PartyThemePage.IsOnPartyThemePage().Should().BeTrue("User should remain on the party theme page");
        }

        [When(@"I attempt to proceed without selecting a party option")]
        public void WhenIAttemptToProceedWithoutSelectingAPartyOption()
        {
            _testContext.HalloweenPartyPage.AttemptToProceedWithoutPartyOptionSelection();
        }

        [Then(@"I should see a party option selection error message")]
        public void ThenIShouldSeeAPartyOptionSelectionErrorMessage()
        {
            _testContext.HalloweenPartyPage.IsPartyOptionSelectionErrorDisplayed().Should().BeTrue("Party option selection error should be displayed");
        }

        [Then(@"I should remain on the Halloween Party page")]
        public void ThenIShouldRemainOnTheHalloweenPartyPage()
        {
            _testContext.HalloweenPartyPage.IsOnHalloweenPartyPage().Should().BeTrue("User should remain on the Halloween Party page");
        }

        [Then(@"no script should be executed")]
        public void ThenNoScriptShouldBeExecuted()
        {
            // This step would verify that XSS attempts are blocked
            // Implementation would depend on the specific security measures in place
            _testContext.PartyLocationPage.VerifyNoScriptExecution().Should().BeTrue("No malicious scripts should be executed");
        }

        [When(@"I enter an email address longer than maximum allowed length")]
        public void WhenIEnterAnEmailAddressLongerThanMaximumAllowedLength()
        {
            var longEmail = new string('a', 250) + "@example.com"; // Create an overly long email
            _testContext.PartyLocationPage.EnterEmailAddress(longEmail);
        }

        [Then(@"I should see an email length validation error message")]
        public void ThenIShouldSeeAnEmailLengthValidationErrorMessage()
        {
            _testContext.PartyLocationPage.IsEmailLengthValidationErrorDisplayed().Should().BeTrue("Email length validation error should be displayed");
        }

        [Given(@"the network connection is slow")]
        public void GivenTheNetworkConnectionIsSlow()
        {
            // This would simulate slow network conditions
            _testContext.WebDriverHelper.Page.SetDefaultTimeout(100); // Very short timeout to simulate slow network
        }

        [Then(@"I should see a timeout error message or loading indicator")]
        public void ThenIShouldSeeATimeoutErrorMessageOrLoadingIndicator()
        {
            var hasTimeoutError = _testContext.PartyLocationPage.IsTimeoutErrorDisplayed();
            var hasLoadingIndicator = _testContext.PartyLocationPage.IsLoadingIndicatorDisplayed();
            
            (hasTimeoutError || hasLoadingIndicator).Should().BeTrue("Either timeout error or loading indicator should be displayed");
        }

        [Then(@"the user should be able to retry the operation")]
        public void ThenTheUserShouldBeAbleToRetryTheOperation()
        {
            _testContext.PartyLocationPage.IsRetryButtonDisplayed().Should().BeTrue("Retry button should be available");
        }

        [Given(@"the server returns a (.*) error")]
        public void GivenTheServerReturnsAnError(int errorCode)
        {
            // This would mock a server error response
            // Implementation would depend on how the application handles server errors
        }

        [Then(@"I should see a server error message")]
        public void ThenIShouldSeeAServerErrorMessage()
        {
            _testContext.PartyLocationPage.IsServerErrorMessageDisplayed().Should().BeTrue("Server error message should be displayed");
        }

        [Then(@"the user should be provided with support contact information")]
        public void ThenTheUserShouldBeProvidedWithSupportContactInformation()
        {
            _testContext.PartyLocationPage.IsSupportContactInfoDisplayed().Should().BeTrue("Support contact information should be displayed");
        }

        [When(@"I navigate using only keyboard controls")]
        public void WhenINavigateUsingOnlyKeyboardControls()
        {
            // Set keyboard-only navigation mode
            _testContext.WebDriverHelper.SetKeyboardOnlyMode(true);
        }

        [When(@"I click on the Halloween Party link using keyboard")]
        public void WhenIClickOnTheHalloweenPartyLinkUsingKeyboard()
        {
            _testContext.HomePage.ClickHalloweenPartyLinkUsingKeyboard();
        }

        [When(@"I select ""(.*)"" option using keyboard")]
        public void WhenISelectOptionUsingKeyboard(string option)
        {
            switch (option.ToLowerInvariant())
            {
                case "i am hosting a party":
                    _testContext.HalloweenPartyPage.ClickIAmHostingAPartyUsingKeyboard();
                    break;
                default:
                    throw new ArgumentException($"Unknown party option for keyboard navigation: {option}");
            }
        }

        [When(@"I select the ""(.*)"" theme using keyboard")]
        public void WhenISelectTheThemeUsingKeyboard(string theme)
        {
            _testContext.PartyThemePage.SelectThemeUsingKeyboard(theme);
        }

        [When(@"I set the number of guests to ""(.*)"" using keyboard")]
        public void WhenISetTheNumberOfGuestsToUsingKeyboard(string guestCount)
        {
            _testContext.PartyLocationPage.SetNumberOfGuestsUsingKeyboard(guestCount);
        }

        [When(@"I enter ""(.*)"" as the email address using keyboard")]
        public void WhenIEnterAsTheEmailAddressUsingKeyboard(string email)
        {
            _testContext.PartyLocationPage.EnterEmailAddressUsingKeyboard(email);
        }

        [When(@"I click the ""(.*)"" button using keyboard")]
        public void WhenIClickTheButtonUsingKeyboard(string buttonName)
        {
            _testContext.PartyLocationPage.ClickButtonUsingKeyboard(buttonName);
        }

        [Then(@"validation errors should be announced to screen readers")]
        public void ThenValidationErrorsShouldBeAnnouncedToScreenReaders()
        {
            _testContext.PartyLocationPage.AreValidationErrorsAccessible().Should().BeTrue("Validation errors should be accessible to screen readers");
        }

        [Then(@"focus should be maintained appropriately")]
        public void ThenFocusShouldBeMaintainedAppropriately()
        {
            _testContext.PartyLocationPage.IsFocusManagementCorrect().Should().BeTrue("Focus should be managed correctly for accessibility");
        }

        [Given(@"JavaScript is disabled in the browser")]
        public void GivenJavaScriptIsDisabledInTheBrowser()
        {
            _testContext.WebDriverHelper.DisableJavaScript();
        }

        [Then(@"server-side validation should prevent form submission")]
        public void ThenServerSideValidationShouldPreventFormSubmission()
        {
            _testContext.PartyLocationPage.IsFormSubmissionPrevented().Should().BeTrue("Server-side validation should prevent invalid form submission");
        }

        [Given(@"multiple users are attempting to book parties simultaneously")]
        public void GivenMultipleUsersAreAttemptingToBookPartiesSimultaneously()
        {
            // This would simulate concurrent user scenarios
            // Implementation would depend on load testing capabilities
        }

        [Then(@"the system should handle the concurrent requests gracefully")]
        public void ThenTheSystemShouldHandleTheConcurrentRequestsGracefully()
        {
            _testContext.PartyLocationPage.IsSystemRespondingGracefully().Should().BeTrue("System should handle concurrent requests gracefully");
        }

        [Then(@"my booking should be processed or I should receive appropriate feedback")]
        public void ThenMyBookingShouldBeProcessedOrIShouldReceiveAppropriateFeedback()
        {
            var isBookingProcessed = _testContext.PartyLocationPage.IsConfirmationMessageDisplayed();
            var hasAppropriateResponse = _testContext.PartyLocationPage.HasAppropriateSystemResponse();
            
            (isBookingProcessed || hasAppropriateResponse).Should().BeTrue("Booking should be processed or appropriate feedback should be provided");
        }
    }
}