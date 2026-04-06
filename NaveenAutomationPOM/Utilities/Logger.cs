using log4net;
using log4net.Config;

namespace NaveenAutomationPOM.Utilities
{
    public static class Logger
    {
        private static readonly ILog log;

        static Logger()
        {
            var configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            if (File.Exists(configFile))
            {
                // Load and watch config in case it changes during development runs
                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
            }

            log = LogManager.GetLogger(typeof(Logger));
        }

        public static void Info(string message) => log.Info(message);
        public static void Warn(string message) => log.Warn(message);
        public static void Error(string message) => log.Error(message);
        public static void Debug(string message) => log.Debug(message);
    }
}

