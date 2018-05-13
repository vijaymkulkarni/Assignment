using System;
using System.Configuration;
using System.Data;

namespace ContactMgmtService
{
    internal abstract class DataAccessLayerBase 
    {
        private static string _connectionString;

        private const string ConnectionStringName = "defaultConnection";

        public static string ConnectionString
        {
            get
            {
                if (_connectionString == string.Empty)
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
        internal static DataAccessLayerBase GetDataAccessLayer(string dataFile, string connectionType)
        {
            DataAccessLayerBase dataAccessLayer = null;

            // ReSharper disable once StringCompareIsCultureSpecific.1
            if (String.Compare(connectionType.Trim().ToUpper(), "TEXT") == 0)
            {
                dataAccessLayer = new FileSystemDataMgr(dataFile);
            }
            // ReSharper disable once StringCompareIsCultureSpecific.1
            else if (String.Compare(connectionType.Trim().ToUpper(), "SQL") == 0)
            {
                dataAccessLayer = new SqlDataMgr();
            }
            return dataAccessLayer;
        }

        public abstract DataTable GetAllData();

        public abstract DataRow GetData(string filterExpression);

        public abstract void InsertData(ref DataTable table);

        public abstract void UpdateData(ref DataTable table);

        public abstract void DeleteData(ref DataTable table);
    }
    
}
