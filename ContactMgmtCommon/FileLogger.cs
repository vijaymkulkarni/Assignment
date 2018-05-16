using System.IO;

namespace ContactMgmtCommon
{
    /// <summary>
    /// File logger
    /// </summary>
    internal class FileLogger : ILogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logType">Log message category</param>
        /// <param name="message">Log message</param>
        public void Log(LogType logType, string message)
        {
            object _lock = new object();
            lock (_lock)
            {
                using (FileStream file = new FileStream("DataFiles\\Log.txt", FileMode.Append, FileAccess.Write,
                    FileShare.None))
                {
                    // Write the string array to a new file named "WriteLines.txt".
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        writer.WriteLine("<LogType=\"{0}\" Message=\"{1}\"/>", logType.ToString(), message);
                        file.Flush();
                    }
                }
            }
        }
    }
}
