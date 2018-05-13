using ContactMgmtCommon;
using ContactMgmtSerivce;
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

            if (_loginCreditials == null)
                return false; //GetInvalidCreditalException();
            else
            {
                var errorMessages = _loginCreditials.Validate();
                if (!string.IsNullOrEmpty(errorMessages))
                    return false;

                DataAccessLayerBase dataAccessLayer =
                    DataAccessLayerBase.GetDataAccessLayer("logins.xml", ConnectionType);

                if (dataAccessLayer == null)
                    return false;

                string filterExpression = string.Concat("Name = '", CreditialsLoginInfo.LoginName,
                    "' and Password ='" + CreditialsLoginInfo.Password, "'");
                DataRow table = dataAccessLayer.GetData(filterExpression);
                if (table != null)
                    return true;
                else
                    return false; //RaiseCustomFaultException("Provided user/password do not match, please try again.");
            }

        }

        //private DataAccessLayerBase GetDataAccessLayer()
        //{
        //    DataAccessLayerBase dataAccessLayer = null;

        //    // ReSharper disable once StringCompareIsCultureSpecific.1
        //    if (String.Compare(ConnectionType.Trim().ToUpper(), "TEXT") == 0)
        //    {
        //        dataAccessLayer = new FileSystemDataMgr("logins.xml");
        //    }
        //    // ReSharper disable once StringCompareIsCultureSpecific.1
        //    else if (String.Compare(ConnectionType.Trim().ToUpper(), "SQL") == 0)
        //    {
        //        dataAccessLayer = new SqlDataMgr();
        //    }
        //    return dataAccessLayer;
        //}
        
        //CustomException GetInvalidCreditalException()
        //{
        //    CustomException exception = new CustomException
        //    {
        //        ExceptionMessage = "Input Creditials are missing",
        //        InnerException = "Object supplied is null",
        //        Title = "Invalid Creditials"
        //    };
        //    return exception;
        //}

        public void Dispose()
        {
            
        }
    }
}
