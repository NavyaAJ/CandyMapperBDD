Feature: Halloween Party Booking
    As a user visiting CandyMapper.com
    I want to book a Halloween party
    So that I can host a themed party for my guests

Background:
    Given I am on the CandyMapper home page

@smoke @halloween-party
Scenario: Successfully book a Halloween party with Zombies theme
    When I click on the Halloween Party link
    Then I should be on the Halloween Party page
    When I select "I Am Hosting A Party" option
    Then I should be on the party theme selection page
    When I select the "Zombies" theme
    Then I should be on the party location page
    When I set the number of guests to "2"
    And I enter "test@example.com" as the email address
    And I click the "Remind Me" button
    Then I should see a confirmation message
    And the party location details should be displayed

@regression @halloween-party
Scenario: Successfully book a Halloween party with Ghosts theme
    When I click on the Halloween Party link
    Then I should be on the Halloween Party page
    When I select "I Am Hosting A Party" option
    Then I should be on the party theme selection page
    When I select the "Ghosts" theme
    Then I should be on the party location page
    When I set the number of guests to "1"
    And I enter "ghost@example.com" as the email address
    And I click the "Remind Me" button
    Then I should see a confirmation message
    And the party location details should be displayed

@data-driven @halloween-party
Scenario Outline: Book Halloween party with different guest counts
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "<GuestCount>"
    And I enter "<Email>" as the email address
    And I click the "Remind Me" button
    Then I should see a confirmation message
    And the party location details should be displayed

Examples:
    | GuestCount | Email                    |
    | 0          | solo@example.com         |
    | 1          | couple@example.com       |
    | 2          | friends@example.com      |

@integration
Scenario: Complete Halloween party booking flow
    Given I complete the Halloween party booking with "2" guests and email "automation@test.com"
    Then the Halloween party booking should be completed successfully

# Negative Test Scenarios
@negative @validation @email
Scenario: Cannot book Halloween party with invalid email format
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "2"
    And I enter "invalid-email" as the email address
    And I click the "Remind Me" button
    Then I should see an email validation error message
    And the party booking should not be completed

@negative @validation @email
Scenario Outline: Email validation with various invalid formats
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "2"
    And I enter "<InvalidEmail>" as the email address
    And I click the "Remind Me" button
    Then I should see an email validation error message
    And the party booking should not be completed

Examples:
    | InvalidEmail          |
    | plainaddress          |
    | @domain.com           |
    | username@             |
    | username@.com         |
    | username..@domain.com |
    | username@domain       |
    | username@domain..com  |

@negative @validation @guests
Scenario: Cannot book Halloween party with negative guest count
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "-1"
    And I enter "test@example.com" as the email address
    And I click the "Remind Me" button
    Then I should see a guest count validation error message
    And the party booking should not be completed

@negative @validation @guests
Scenario: Cannot book Halloween party with excessive guest count
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "1000"
    And I enter "test@example.com" as the email address
    And I click the "Remind Me" button
    Then I should see a guest count limit error message
    And the party booking should not be completed

@negative @validation @empty-fields
Scenario: Cannot book Halloween party with empty email field
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "2"
    And I leave the email address field empty
    And I click the "Remind Me" button
    Then I should see a required field error message for email
    And the party booking should not be completed

@negative @validation @empty-fields
Scenario: Cannot book Halloween party with empty guest count
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I leave the guest count field empty
    And I enter "test@example.com" as the email address
    And I click the "Remind Me" button
    Then I should see a required field error message for guest count
    And the party booking should not be completed

@negative @validation @special-characters
Scenario Outline: Guest count field should not accept special characters
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "<InvalidInput>"
    And I enter "test@example.com" as the email address
    And I click the "Remind Me" button
    Then I should see a guest count validation error message
    And the party booking should not be completed

Examples:
    | InvalidInput |
    | abc          |
    | 2.5          |
    | 2a           |
    | !@#          |
    | 2+3          |

@negative @ui @navigation
Scenario: Cannot proceed without selecting party theme
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I attempt to proceed without selecting a theme
    Then I should see a theme selection error message
    And I should remain on the party theme selection page

@negative @ui @navigation
Scenario: Cannot proceed without selecting party option
    When I click on the Halloween Party link
    And I attempt to proceed without selecting a party option
    Then I should see a party option selection error message
    And I should remain on the Halloween Party page

@negative @security @xss
Scenario: Email field should be protected against XSS attacks
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "2"
    And I enter "<script>alert('xss')</script>@example.com" as the email address
    And I click the "Remind Me" button
    Then I should see an email validation error message
    And no script should be executed
    And the party booking should not be completed

@negative @security @sql-injection
Scenario: Email field should be protected against SQL injection
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "2"
    And I enter "'; DROP TABLE users; --@example.com" as the email address
    And I click the "Remind Me" button
    Then I should see an email validation error message
    And the party booking should not be completed

@negative @boundary @email-length
Scenario: Email field should enforce maximum length limit
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "2"
    And I enter an email address longer than maximum allowed length
    And I click the "Remind Me" button
    Then I should see an email length validation error message
    And the party booking should not be completed

@negative @network @timeout
Scenario: Handle network timeout during party booking
    Given the network connection is slow
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "2"
    And I enter "test@example.com" as the email address
    And I click the "Remind Me" button
    Then I should see a timeout error message or loading indicator
    And the user should be able to retry the operation

@negative @server-error @500
Scenario: Handle server error during party booking
    Given the server returns a 500 error
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "2"
    And I enter "test@example.com" as the email address
    And I click the "Remind Me" button
    Then I should see a server error message
    And the user should be provided with support contact information

@negative @accessibility @keyboard-navigation
Scenario: All interactive elements should be keyboard accessible
    When I navigate using only keyboard controls
    And I click on the Halloween Party link using keyboard
    And I select "I Am Hosting A Party" option using keyboard
    And I select the "Zombies" theme using keyboard
    When I set the number of guests to "invalid" using keyboard
    And I enter "invalid-email" as the email address using keyboard
    And I click the "Remind Me" button using keyboard
    Then validation errors should be announced to screen readers
    And focus should be maintained appropriately

@negative @browser-compatibility @javascript-disabled
Scenario: Form should work with JavaScript disabled
    Given JavaScript is disabled in the browser
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "invalid"
    And I enter "invalid-email" as the email address
    And I click the "Remind Me" button
    Then server-side validation should prevent form submission
    And appropriate error messages should be displayed

@negative @performance @concurrent-users
Scenario: Handle concurrent booking attempts
    Given multiple users are attempting to book parties simultaneously
    When I click on the Halloween Party link
    And I select "I Am Hosting A Party" option
    And I select the "Zombies" theme
    When I set the number of guests to "2"
    And I enter "test@example.com" as the email address
    And I click the "Remind Me" button
    Then the system should handle the concurrent requests gracefully
    And my booking should be processed or I should receive appropriate feedback