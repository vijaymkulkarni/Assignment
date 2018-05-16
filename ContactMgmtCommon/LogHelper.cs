using System;
using System.Configuration;

namespace ContactMgmtCommon
{
    public static class LogHelper
    {        
        private static string _logType;


        private const string LogDestinationType = "LogDestinationType";

        /// <summary>
        /// 
        /// </summary>
        private static string LogDestination
        {
            get
            {
                if (string.IsNullOrEmpty(_logType))
                {
                    _logType = Convert.ToString(ConfigurationManager.AppSettings[LogDestinationType]);
                }
                
                return _logType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logCategory"></param>
        /// <param name="message"></param>
        public static void Log(LogType logCategory, string message)
        {
            ILogger loggingHelperCls;
            switch (LogDestination.ToUpper())
            {
                case "TEXT":
                    loggingHelperCls = new FileLogger();
                    break;

                case "EVENTVIEWER":
                    loggingHelperCls = new EventViewerLogger();
                    break;

                default:
                    loggingHelperCls = new FileLogger();
                    break;
            }

            loggingHelperCls.Log(logCategory, message);
        }
    }
}
