using ContactMgmtCommon;
using ContactMgmtService;
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

            if (_loginCreditials == null)
                return false; //GetInvalidCreditalException();
            else
            {
                var errorMessages = _loginCreditials.Validate();
                if (!string.IsNullOrEmpty(errorMessages))
                    return false;

                IDataAccess dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(@"DataFiles\logins.xml", ConnectionType);

                if (dataAccessLayer == null)
                    return false;

                string filterExpression = string.Concat("Name = '", CreditialsLoginInfo.LoginName, "' and Password ='" + CreditialsLoginInfo.Password, "'");
                DataRow table = dataAccessLayer.GetData(filterExpression);
                if (table != null)
                    return true;
                else
                    return false; //RaiseCustomFaultException("Provided user/password do not match, please try again.");
            }

        }
        

        public void Dispose()
        {
            
        }
    }
}
