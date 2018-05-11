using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMgmtSerivce
{
    internal abstract class DataAccessLayer
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

        public abstract DataTable GetAllData();

        public abstract DataTable GetData();

        public abstract DataTable InsertData();

        public abstract DataTable UpdateData();

        public abstract DataTable DeleteData();
    }


}
