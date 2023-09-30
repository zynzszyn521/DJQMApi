namespace zyqmapi
{
    public class MyLogger : ILogger
    {
        private readonly string className;
        private readonly string logFolderPath;

        public MyLogger(string name, string folderPath)
        {
            className = name;
            logFolderPath = folderPath;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null; // 如果需要支持作用域，请返回一个实现了IDisposable接口的对象
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true; // 指定日志级别，这里设置为所有级别都输出日志
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);

            // 根据类名创建日志文件名
            var logFileName = $"{className}_{DateTime.Now.ToString("yyyyMMdd")}.log";
            string finalLogFolderPath = Path.Combine(logFolderPath, "logs");
            if (!Directory.Exists(finalLogFolderPath))
            {
                Directory.CreateDirectory(finalLogFolderPath);
            };
            var logFilePath = System.IO.Path.Combine(finalLogFolderPath, logFileName);

            // 将日志写入文件
            System.IO.File.AppendAllText(logFilePath, message);
        }
    }
}
