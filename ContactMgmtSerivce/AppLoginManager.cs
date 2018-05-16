using ContactMgmtCommon;
using ContactMgmtService;
using System;
using System.Data;

namespace ContactMgmtService
{
    /// <summary>
    /// 
    /// </summary>
    public class AppLoginManager : ServiceMain, ILoginService
    {
        private LoginInfo _loginCreditials;

        public LoginInfo CreditialsLoginInfo
        {
            get
            { return _loginCreditials; }
            set { _loginCreditials = value; }
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
                    return false; //GetInvalidCreditalException();
                else
                {
                    var errorMessages = _loginCreditials.Validate();
                    if (!string.IsNullOrEmpty(errorMessages))
                    {
                        LogHelper.Log(LogType.Error, errorMessages);
                        return false;
                    }
                    
                    IDataAccess dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(@"DataFiles\logins.xml", ConnectionType);

                    if (dataAccessLayer == null)
                    {
                        LogHelper.Log(LogType.Error, "Unable to find login data file.");
                        return false;
                    }
                    
                    string filterExpression = string.Concat("Name = '", CreditialsLoginInfo.LoginName,
                        "' and Password ='" + CreditialsLoginInfo.Password, "'");
                    DataRow authenticateDataRow = dataAccessLayer.GetData(filterExpression);
                    if (authenticateDataRow != null)
                    {
                        CreditialsLoginInfo.Loginrole = Convert.ToString(authenticateDataRow["type"]);
                        LogHelper.Log(LogType.Information, string.Concat(CreditialsLoginInfo.LoginName, " LoginName Successful"));
                        return true;
                    }
                    else
                    {
                        LogHelper.Log(LogType.Information, "Provided user/password do not match, please try again.");
                        return false; 
                    }
                }

            }
            catch (Exception e)
            {
                LogHelper.Log(LogType.Error, String.Concat(e.Message, Environment.NewLine, e.StackTrace));
                throw;
            }

        }


        public void Dispose()
        {
            
        }
    }
}
