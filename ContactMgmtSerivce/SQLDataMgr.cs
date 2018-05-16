using ContactMgmtService;
using System;
using System.Data;

namespace ContactMgmtService
{
    internal class SqlDataMgr : IDataAccess
    {

        #region SQL Command
       
        //public SqlCommand GetCommand(string sql)
        //{
        //    var conn = new SqlConnection(ConnectionString);
        //    var sqlCmd = new SqlCommand(sql, conn);
        //    return sqlCmd;
        //}
        
        //private DataTable Execute(string sql)
        //{
        //    DataTable dt = new DataTable();
        //    SqlCommand cmd = GetCommand(sql);
        //    cmd.Connection.Open();
        //    dt.Load(cmd.ExecuteReader());
        //    cmd.Connection.Close();
        //    return dt;
        //}

        ///// <summary>
        ///// Datatable Döndür
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //private DataTable Execute(SqlCommand command)
        //{
        //    DataTable dt = new DataTable();
        //    command.Connection.Open();
        //    //command.ExecuteNonQuery();
        //    dt.Load(command.ExecuteReader());
        //    command.Connection.Close();
        //    return dt;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        //private int ExecuteNonQuery(string sql)
        //{
        //    SqlCommand cmd = GetCommand(sql);
        //    cmd.Connection.Open();
        //    int result = cmd.ExecuteNonQuery();
        //    cmd.Connection.Close();
        //    return result;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //private int ExecuteNonQuery(SqlCommand command)
        //{
        //    command.Connection.Open();
        //    int result = command.ExecuteNonQuery();
        //    command.Connection.Close();
        //    return result;
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="spName"></param>
        ///// <returns></returns>
        //private int ExecuteStoredProcedure(string spName)
        //{
        //    SqlCommand cmd = GetCommand(spName);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection.Open();
        //    int result = cmd.ExecuteNonQuery();
        //    cmd.Connection.Close();
        //    return result;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //private int ExecuteStoredProcedure(SqlCommand command)
        //{
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Connection.Open();
        //    int result = command.ExecuteNonQuery();
        //    command.Connection.Close();
        //    return result;
        //}

        #endregion

        public DataTable GetAllData()
        {
            throw new NotImplementedException();
        }

        public DataRow GetData(string filterExpression)
        {
            throw new NotImplementedException();
        }

        public void InsertData(ref DataTable table)
        {
            throw new NotImplementedException();
        }

        public void UpdateData(ref DataTable table)
        {
            throw new NotImplementedException();
        }

        public void DeleteData(ref DataTable table)
        {
            throw new NotImplementedException();
        }
    }
}
