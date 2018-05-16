using System.Diagnostics;

namespace ContactMgmtCommon
{
    /// <summary>
    /// File logger
    /// </summary>
    internal class EventViewerLogger : ILogger
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
                EventLog eventLog = new EventLog("ContactManagement")
                {
                    Source = "Contact Management",
                };
                EventLogEntryType envEntryType;
                switch (logType)
                {
                    case LogType.Critical:
                    case LogType.Error:
                        envEntryType = EventLogEntryType.Error;
                        break;
                    case LogType.Information:
                        envEntryType = EventLogEntryType.Information;
                        break;
                    case LogType.Warning:
                        envEntryType = EventLogEntryType.Warning;
                        break;
                    default:
                        envEntryType = EventLogEntryType.Information;
                        break;
                }
                eventLog.WriteEntry(message, envEntryType);
            }
        }
    }

}
