namespace zyqmapi
{
    public class MyLoggerProvider : ILoggerProvider
    {
        private readonly string logFolderPath; // 日志文件夹路径
        public MyLoggerProvider(string folderPath)
        {
            logFolderPath = folderPath;
        }

        public ILogger CreateLogger(string categoryName)
        {
            var className = categoryName.Substring(categoryName.LastIndexOf(".") + 1); // 获取类名
            var logger = new MyLogger(className, logFolderPath);
            return logger;
        }

        public void Dispose()
        {
            // 可以在这里释放资源
        }
    }
}
