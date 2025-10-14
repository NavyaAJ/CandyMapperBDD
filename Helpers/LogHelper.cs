using System.Runtime.CompilerServices;

namespace CandyMapperBDD.Helpers
{
    public static class LogHelper
    {
        private static readonly object _lock = new object();

        public static void LogInfo(string message, [CallerMemberName] string methodName = "")
        {
            WriteLog("INFO", message, methodName);
        }

        public static void LogError(string message, [CallerMemberName] string methodName = "")
        {
            WriteLog("ERROR", message, methodName);
        }

        public static void LogWarning(string message, [CallerMemberName] string methodName = "")
        {
            WriteLog("WARNING", message, methodName);
        }

        public static void LogDebug(string message, [CallerMemberName] string methodName = "")
        {
            WriteLog("DEBUG", message, methodName);
        }

        public static void LogStep(string step, [CallerMemberName] string methodName = "")
        {
            WriteLog("STEP", step, methodName);
        }

        private static void WriteLog(string level, string message, string methodName)
        {
            lock (_lock)
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var logMessage = $"[{timestamp}] [{level}] [{methodName}] {message}";
                
                Console.WriteLine(logMessage);
                
                // Optionally write to file
                try
                {
                    var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
                    if (!Directory.Exists(logDirectory))
                    {
                        Directory.CreateDirectory(logDirectory);
                    }
                    
                    var logFile = Path.Combine(logDirectory, $"TestLog_{DateTime.Now:yyyyMMdd}.txt");
                    File.AppendAllText(logFile, logMessage + Environment.NewLine);
                }
                catch
                {
                    // Ignore file logging errors
                }
            }
        }

        public static void LogTestStart(string testName)
        {
            LogInfo($"========== Starting Test: {testName} ==========");
        }

        public static void LogTestEnd(string testName)
        {
            LogInfo($"========== Completed Test: {testName} ==========");
        }

        public static void LogException(Exception ex, [CallerMemberName] string methodName = "")
        {
            LogError($"Exception in {methodName}: {ex.Message}");
            LogError($"Stack Trace: {ex.StackTrace}");
        }
    }
}