using System.Configuration;

namespace ContactMgmtService
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceMain
    {

        private string _connectionType;
        private const string ConnectionTypeKey = "connectionType";

        public string ConnectionType
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionType))
                {
                    _connectionType = ConfigurationManager.AppSettings[ConnectionTypeKey];
                }
                return _connectionType;
            }
        }

    }
}
