# CandyMapper BDD Test Framework 
Note: This entire framework is created using AI agents - Playwright MCP server and Builtin Agents from github Copilot
A comprehensive Behavior-Driven Development (BDD) test framework built with SpecFlow, NUnit, and Microsoft Playwright for automated testing of the CandyMapper.com Halloween Party booking functionality.

## 🎯 Features

- **BDD Testing**: Written in Gherkin language with SpecFlow
- **Playwright Integration**: Modern browser automation with Microsoft Playwright
- **Comprehensive Test Coverage**: 
  - ✅ Positive scenarios (Happy path testing)
  - ❌ Negative scenarios (Error handling and validation)
  - 🔒 Security testing (XSS, SQL injection protection)
  - ♿ Accessibility testing (Keyboard navigation, screen readers)
  - 🚀 Performance testing (Network timeouts, concurrent users)
- **Page Object Model**: Maintainable and reusable page objects
- **Configurable**: Environment-specific settings via JSON configuration
- **Rich Reporting**: Detailed test execution logs and reports

## 🏗️ Architecture

### Project Structure
```
CandyMapperBDD/
├── Config/                     # Configuration settings
├── Features/                   # BDD feature files (Gherkin)
├── Helpers/                    # Utility classes and test context
├── Pages/                      # Page Object Model classes
├── StepDefinitions/           # SpecFlow step definitions
├── TestData/                  # Test data and configuration
└── Tests/                     # Unit tests for framework validation
```

### Technology Stack
- **.NET 8.0**: Modern .NET framework
- **SpecFlow**: BDD framework for .NET
- **Microsoft Playwright**: Cross-browser web automation
- **NUnit**: Unit testing framework
- **FluentAssertions**: Fluent assertion library

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 or VS Code
- Git

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd CandyMapperBDD
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Install Playwright browsers**
   ```bash
   # Add .NET tools to PATH (if not already done)
   export PATH="$PATH:~/.dotnet/tools"
   
   # Install Playwright CLI
   dotnet tool install --global Microsoft.Playwright.CLI
   
   # Install browsers
   playwright install
   ```

## 🧪 Running Tests

### Run All Tests
```bash
dotnet test
```

### Run Tests by Category
```bash
# Smoke tests only
dotnet test --filter Category=smoke

# Negative tests only
dotnet test --filter Category=negative

# Security tests only
dotnet test --filter Category=security
```

### Run Specific Test Scenarios
```bash
# Run tests with specific tags
dotnet test --filter Category=email-validation
dotnet test --filter Category=accessibility
```

## 📋 Test Scenarios

### Positive Scenarios
- ✅ Successfully book Halloween party with Zombies theme
- ✅ Successfully book Halloween party with Ghosts theme
- ✅ Data-driven testing with different guest counts
- ✅ Complete end-to-end booking flow

### Negative Scenarios
- ❌ **Email Validation**: Invalid formats, XSS attempts, SQL injection
- ❌ **Guest Count Validation**: Negative numbers, excessive counts, special characters
- ❌ **Form Validation**: Empty required fields, invalid selections
- ❌ **Security Testing**: XSS protection, SQL injection prevention
- ❌ **Boundary Testing**: Maximum field lengths, limit validations
- ❌ **Error Handling**: Network timeouts, server errors
- ❌ **Accessibility**: Keyboard navigation, screen reader compatibility
- ❌ **Performance**: Concurrent user scenarios, system load testing

## ⚙️ Configuration

### Application Settings (`appsettings.json`)
```json
{
  "BrowserSettings": {
    "BrowserType": "Chrome",
    "HeadlessMode": false,
    "ImplicitWait": 10,
    "PageLoadTimeout": 30,
    "WindowSize": {
      "Width": 1920,
      "Height": 1080
    }
  },
  "ApplicationSettings": {
    "BaseUrl": "https://candymapper.com/",
    "TestDataPath": "TestData/"
  }
}
```

### Test Data Configuration
Test data is stored in `TestData/TestData.json` and includes:
- Valid test scenarios
- Invalid input patterns for negative testing
- Security test patterns
- Expected error messages

## 📊 Test Categories

| Category | Description | Examples |
|----------|-------------|----------|
| `@smoke` | Critical path tests | Basic booking functionality |
| `@regression` | Full regression suite | All positive scenarios |
| `@negative` | Error handling tests | Invalid inputs, validation |
| `@security` | Security vulnerability tests | XSS, SQL injection |
| `@accessibility` | Accessibility compliance | Keyboard navigation |
| `@performance` | Performance and load tests | Concurrent users |
| `@boundary` | Boundary value testing | Field limits |

## 🔧 Framework Features

### Page Object Model
- **Maintainable**: Centralized element definitions
- **Reusable**: Common actions across different tests
- **Playwright Integration**: Modern async/await patterns

### Error Handling
- **Comprehensive Validation**: Email, guest count, form validation
- **Security Testing**: XSS and SQL injection protection
- **User Experience**: Proper error messages and feedback

### Reporting
- **Detailed Logs**: Comprehensive test execution logging
- **SpecFlow Reports**: BDD-style test reports
- **Screenshots**: Failure screenshots (configurable)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-test-scenario`)
3. Commit your changes (`git commit -am 'Add new negative test scenario'`)
4. Push to the branch (`git push origin feature/new-test-scenario`)
5. Create a Pull Request

## 📝 Best Practices

### Writing Tests
- Use descriptive scenario names
- Follow Given-When-Then structure
- Keep scenarios focused and atomic
- Use data-driven testing for similar scenarios with different inputs

### Page Objects
- Keep selectors maintainable (prefer data attributes over complex CSS)
- Implement both sync and async methods
- Add proper error handling and meaningful error messages

### Test Data
- Separate test data from test logic
- Use realistic test data
- Include both valid and invalid test cases

## 🐛 Troubleshooting

### Common Issues

1. **Playwright Browser Not Found**
   ```bash
   # Install browsers for the specific Playwright version
   playwright install
   ```

2. **Test Configuration Issues**
   - Verify `appsettings.json` settings
   - Check test data file paths
   - Ensure proper base URL configuration

3. **Selector Issues**
   - Use Playwright Inspector for debugging
   - Prefer stable selectors (data attributes, IDs)
   - Add proper wait conditions

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🎃 About CandyMapper

This test framework is designed to ensure the quality and reliability of the CandyMapper.com Halloween Party booking system, providing comprehensive test coverage for both positive user journeys and edge cases.
