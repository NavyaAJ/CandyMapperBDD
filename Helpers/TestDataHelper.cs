using System.Text.Json;
using CandyMapperBDD.Config;

namespace CandyMapperBDD.Helpers
{
    public class TestDataHelper
    {
        private readonly string _testDataPath;

        public TestDataHelper()
        {
            var appSettings = ConfigurationManager.GetAppSettings();
            _testDataPath = Path.Combine(Directory.GetCurrentDirectory(), appSettings.ApplicationSettings.TestDataPath);
        }

        public T LoadTestData<T>(string fileName)
        {
            var filePath = Path.Combine(_testDataPath, fileName);
            
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Test data file not found: {filePath}");
            }

            var jsonContent = File.ReadAllText(filePath);
            var testData = JsonSerializer.Deserialize<T>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (testData == null)
            {
                throw new InvalidOperationException($"Failed to deserialize test data from file: {filePath}");
            }

            return testData;
        }

        public string GetRandomEmail()
        {
            var timestamp = DateTime.Now.Ticks;
            return $"test{timestamp}@example.com";
        }

        public string GetRandomGuestCount()
        {
            var random = new Random();
            return random.Next(0, 3).ToString(); // 0, 1, or 2 guests
        }
    }

    public class TestData
    {
        public List<TestUser> TestUsers { get; set; } = new();
        public List<string> PartyThemes { get; set; } = new();
        public List<string> EmailAddresses { get; set; } = new();
    }

    public class TestUser
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PartyTheme { get; set; } = string.Empty;
        public string GuestCount { get; set; } = string.Empty;
    }
}