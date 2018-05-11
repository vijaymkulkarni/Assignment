using ContactMgmtCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ContactMgmtSerivce
{
    /// <summary>
    /// 
    /// </summary>
    public class AppLoginManager : ILoginService
    {
        private LoginInfo _loginCreditials;

        private static string _connectionType;
        private const string ConnectionTypeKey = "connectionType";

        public static string ConnectionType
        {
            get
            {
                if (_connectionType == string.Empty)
                {
                    _connectionType = ConfigurationManager.ConnectionStrings[ConnectionTypeKey].ConnectionString;
                }
                return _connectionType;
            }
        }

        public AppLoginManager(LoginInfo loginCreditials)
        {
            _loginCreditials = loginCreditials;
        }

        public AppLoginManager()
        {
        }

        public LoginInfo CreditialsLoginInfo
        {
            get => _loginCreditials;
            set => _loginCreditials = value;
        }

        public void ValidateLogin(LoginInfo loginCreditials)
        {
            _loginCreditials = loginCreditials;
        }
        
        CustomException GetInvalidCreditalException()
        {
            CustomException exception = new CustomException
            {
                ExceptionMessage = "Input Creditials are missing",
                InnerException = "Object supplied is null",
                Title = "Invalid Creditials"
            };
            return exception;
        }

        public bool ValidateLogin()
        {
            if (_loginCreditials == null)
            {
                var exception = GetInvalidCreditalException();
                throw new FaultException<CustomException>(exception); ;
            }
            else
            {
                DataAccessLayer dataAccessLayer = null;
                dataAccessLayer = GetDataAccessLayer();

                if (dataAccessLayer == null) return false;
                DataTable table = dataAccessLayer.GetAllData(); 
            }
            return false;
        }

        private static DataAccessLayer GetDataAccessLayer()
        {
            DataAccessLayer dataAccessLayer = null;
            // ReSharper disable once StringCompareIsCultureSpecific.1
            if (String.Compare(ConnectionType, "TEXT") == 0)
            {
                dataAccessLayer = new FileSystemDataMgr();
            }
            // ReSharper disable once StringCompareIsCultureSpecific.1
            else if (String.Compare(ConnectionType, "SQL") == 0)
            {
                dataAccessLayer = new SqlDataMgr();
            }

            return dataAccessLayer;
        }
    }
}
