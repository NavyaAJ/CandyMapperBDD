using Microsoft.Extensions.Configuration;

namespace CandyMapperBDD.Config
{
    public class AppSettings
    {
        public BrowserSettings BrowserSettings { get; set; } = new();
        public ApplicationSettings ApplicationSettings { get; set; } = new();
    }

    public class BrowserSettings
    {
        public string BrowserType { get; set; } = "Chrome";
        public bool HeadlessMode { get; set; } = false;
        public int ImplicitWait { get; set; } = 10;
        public int PageLoadTimeout { get; set; } = 30;
        public WindowSize WindowSize { get; set; } = new();
    }

    public class WindowSize
    {
        public int Width { get; set; } = 1920;
        public int Height { get; set; } = 1080;
    }

    public class ApplicationSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string TestDataPath { get; set; } = string.Empty;
    }

    public static class ConfigurationManager
    {
        private static AppSettings? _appSettings;

        public static AppSettings GetAppSettings()
        {
            if (_appSettings == null)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                _appSettings = new AppSettings();
                config.Bind(_appSettings);
            }

            return _appSettings;
        }
    }
}