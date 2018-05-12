using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMgmtService
{
    /// <summary>
    /// 
    /// </summary>
    internal class FileSystemDataMgr : DataAccessLayer
    {
        private readonly string _fileName;
        
        /// <summary>
        /// 
        /// </summary>
        public FileSystemDataMgr(string fileName)
        {
            _fileName = fileName;
        }
        
        public override DataTable GetAllData()
        {
            var table = _fileName == "login.xml" ? new DataTable("Login") : new DataTable("Contacts");
            ReadFile(ref table);
            return table.Rows.Count > 0 ? table : null;
        }

        public override DataRow GetData(string filterExpression)
        {
            var table = _fileName == "login.xml" ? new DataTable("Login") : new DataTable("Contacts");
            ReadFile(ref table);
            DataRow[] dataRow = table.Select(filterExpression);
            return dataRow.Length > 0 ? dataRow[0] : null;
        }

        private DataTable ReadFile(ref DataTable table)
        {
            //create the DataTable that will hold the data            
            try
            {
                if (string.IsNullOrEmpty(_fileName)) return table;

                //open the file using a Stream
                using (Stream stream = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
                {
                    using (DataSet ds = new DataSet())
                    {
                        ds.ReadXml(stream);
                        table = ds.Tables[0];
                    }
                    //return the results
                    return table;
                }
            }
            catch (Exception ex)
            {
                return table;
            }
        }        

        public override DataTable InsertData()
        {
            throw new NotImplementedException();
        }

        public override DataTable UpdateData()
        {
            throw new NotImplementedException();
        }

        public override DataTable DeleteData()
        {
            throw new NotImplementedException();
        }
    }
}
