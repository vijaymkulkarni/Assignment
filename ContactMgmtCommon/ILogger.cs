namespace ContactMgmtCommon
{
    public interface ILogger
    {
        void Log(LogType logType, string message);
    }
}
