using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMgmtService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DataTable GetAllData();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        DataRow GetData(string filterExpression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        void InsertData(ref DataTable table);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        void UpdateData(ref DataTable table);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        void DeleteData(ref DataTable table);
    }
}
