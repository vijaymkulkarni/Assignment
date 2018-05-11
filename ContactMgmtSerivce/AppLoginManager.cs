using ContactMgmtCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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

        public bool ValidateLogin(LoginInfo loginCreditials)
        {
            _loginCreditials = loginCreditials;
            return ValidateLogin();
        }
        
        public bool ValidateLogin()
        {
            try
            {
                if (_loginCreditials == null)
                {
                    var exception = GetInvalidCreditalException();
                    throw new FaultException<CustomException>(exception);
                }
                else
                {
                    var errorMessages = _loginCreditials.Validate();
                    if (!string.IsNullOrEmpty(errorMessages))
                    {
                        //var exception = GetInvalidCreditalException();
                        //throw new FaultException<CustomException>(exception);
                        return false;
                    }

                    DataAccessLayer dataAccessLayer = null;
                    dataAccessLayer = GetDataAccessLayer();

                    if (dataAccessLayer == null) return false;
                    string filterExpression = string.Concat("Name = '", CreditialsLoginInfo.LoginName, 
                                                            "' and Password ='" + CreditialsLoginInfo.Password, "'");
                    DataRow table = dataAccessLayer.GetData(filterExpression);
                    if (table != null) return true;
                }
            }
            catch (Exception ex)
            {
                RaiseFaultException(ex);
            }
            return false;
        }

        private static void RaiseFaultException(Exception ex)
        {
            var exception = new CustomException
            {
                ExceptionMessage = ex.Message,
                Title = "Exception"
            };
            if (ex.InnerException != null) exception.InnerException = ex.InnerException.ToString();
            exception.StackTrace = ex.StackTrace;
            throw new FaultException<CustomException>(exception);
        }
        
        private DataAccessLayer GetDataAccessLayer()
        {
            DataAccessLayer dataAccessLayer = null;

            // ReSharper disable once StringCompareIsCultureSpecific.1
            if (String.Compare(ConnectionType.Trim().ToUpper(), "TEXT") == 0)
            {
                dataAccessLayer = new FileSystemDataMgr("logins.xml");
            }
            // ReSharper disable once StringCompareIsCultureSpecific.1
            else if (String.Compare(ConnectionType.Trim().ToUpper(), "SQL") == 0)
            {
                dataAccessLayer = new SqlDataMgr();
            }
            return dataAccessLayer;
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
    }
}
