using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace ContactMgmtService
{
    /// <summary>
    /// 
    /// </summary>
    internal class FileSystemDataMgr : DataAccessLayerBase
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
        
        public override void InsertData(ref DataTable table)
        {
            if (table == null) return;

            DataTable existingTable = new DataTable();
            ReadFile(ref existingTable);
            if (existingTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in table.Rows)
                {

                    DataRow newRow = existingTable.NewRow();
                    Random rnd = new Random();
                    newRow[0] = rnd.Next();
                    for (int columnsIndex = 1; columnsIndex < table.Columns.Count; columnsIndex++)
                    {
                        newRow[columnsIndex] = dataRow[columnsIndex];
                    }

                    existingTable.Rows.Add(newRow);
                }
            }
            else
            {
                existingTable = table;
            }
            WriteToTable(existingTable);
            table = existingTable;
        }
        
        public override void UpdateData(ref DataTable table)
        {
            DataTable existingTable = new DataTable();
            ReadFile(ref existingTable);
            foreach (DataRow dataRow in table.Rows)
            {
                string filterExpression = string.Concat("ID = ", dataRow[0]);
                DataRow[] rows = existingTable.Select(filterExpression);

                foreach (DataRow existingTableDataRow in rows)
                {
                    for(int columnIndex = 0; columnIndex < existingTable.Columns.Count; columnIndex++)
                    {
                        existingTableDataRow[columnIndex] = dataRow[columnIndex];
                    }
                }
            }
            WriteToTable(existingTable);
            table = existingTable;
        }

        public override void DeleteData(ref DataTable table)
        {
            DataTable existingTable = new DataTable();
            ReadFile(ref existingTable);
            foreach (DataRow dataRow in table.Rows)
            {
                string filterExpression = string.Concat("ID = ", dataRow[0]);
                DataRow[] rows = existingTable.Select(filterExpression);
                foreach (DataRow existingTableDataRow in rows)
                {
                    existingTableDataRow.Delete();
                }
            }
            WriteToTable(existingTable);
            table = existingTable;
        }

        #region Private Helper Routines

        private void ReadFile(ref DataTable table)
        {
            //create the DataTable that will hold the data            

            if (string.IsNullOrEmpty(_fileName)) return;

            using (TextReader tr = new StreamReader(_fileName))
            {
                using (DataSet ds = new DataSet())
                {
                    ds.ReadXml(tr);
                    if (ds.Tables.Count == 1)
                        table = ds.Tables[0];
                }
                tr.Close();
            }
        }

        private void WriteToTable(DataTable existingTable)
        {
            if (string.IsNullOrEmpty(_fileName)) return;

            DataSet ds = existingTable.DataSet;
            if (ds == null)
            {
                ds = new DataSet();
                ds.Tables.Add(existingTable);
            }

            using (XmlTextWriter xmlWrite = new XmlTextWriter(_fileName, Encoding.UTF8))
                ds.WriteXml(xmlWrite);

            existingTable = new DataTable();
            ReadFile(ref existingTable);            
        }

        #endregion

    }
}
