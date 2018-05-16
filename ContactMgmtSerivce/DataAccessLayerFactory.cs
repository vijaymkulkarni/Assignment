using ContactMgmtService;
using System;
using System.Configuration;
using System.Data;

namespace ContactMgmtService
{
    internal abstract class DataAccessLayerFactory 
    {
        private static string _connectionString;

        private const string ConnectionStringName = "defaultConnection";
        /// <summary>
        /// 
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
                }
                return _connectionString;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataFile"></param>
        /// <param name="connectionType"></param>
        /// <returns></returns>
        internal static IDataAccess GetDataAccessLayer(string dataFile, string connectionType)
        {
            IDataAccess dataAccessLayer = null;
            
            if (String.Compare(connectionType.Trim().ToUpper(), "TEXT") == 0)
            {
                dataAccessLayer = new FileSystemDataMgr(dataFile);
            }
            else if (String.Compare(connectionType.Trim().ToUpper(), "SQL") == 0)
            {
                dataAccessLayer = new SqlDataMgr();
            }
            return dataAccessLayer;
        }
        
    }
    
}
