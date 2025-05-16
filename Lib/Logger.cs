namespace QQBot_Jump.Lib
{
    public static class Logger
    {
        private static ILoggerFactory? _loggerFactory;

        /// <summary>
        /// 初始化日志
        /// </summary>
        public static void Configure(ILoggerFactory factory)
        {
            _loggerFactory = factory;
        }

        public static ILogger<T> GetLogger<T>()
        {
            if (_loggerFactory == null)
                throw new InvalidOperationException("LoggerFactory 未初始化，请在 Program.cs 中调用 AppLogger.Configure()");
            
            return _loggerFactory.CreateLogger<T>();
        }

        public static ILogger GetLogger(Type type)
        {
            if (_loggerFactory == null)
                throw new InvalidOperationException("LoggerFactory 未初始化");
            
            return _loggerFactory.CreateLogger(type);
        }

        public static void Info<T>(string message)
        {
            GetLogger<T>().LogInformation(message);
        }

        public static void Warn<T>(string message)
        {
            GetLogger<T>().LogWarning(message);
        }

        public static void Error<T>(string message, Exception? ex = null)
        {
            GetLogger<T>().LogError(ex, message);
        }
    }
}