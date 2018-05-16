using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactMgmtCommon
{
    /// <summary>
    /// </summary>
    [DataContract]
    public class LoginInfo
    {
        private string _loginName;
        private string _password;
        private string _loginRole;
        
        /// <summary>
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        public LoginInfo(string loginName, string password)
        {
            _loginName = loginName;
            _password = password;
        }


        /// <summary>
        /// login name / user name of logged-in user.
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Login Name is missing")]
        [RegularExpression("[^a-zA-Z0-9]*", ErrorMessage = "Login Name is invalid")]
        public string LoginName
        {
            get
            {
                return _loginName;
            }
            set
            {
                _loginName = value;
            }
        }

        /// <summary>
        /// Password of logged in user
        /// </summary>
        [DataMember]
        [RegularExpression("[^A-Za-z0-9#$^&@!~%=_]*", ErrorMessage = "Password is invalid")]
        [Required(ErrorMessage = "Password is missing")]
        public string Password
        {
            get
            { return _password; }
            set { _password = value; }
        }


        /// <summary>
        /// Various roles of given login user return back to calling routine such as admin / Guest.
        /// </summary>
        [DataMember]
        public string Loginrole
        {
            get
            {
                return _loginRole;
            }
            set
            {
                _loginRole = value;
            }
        }
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string Validate()
        {
            var errorMessages = new StringBuilder();
            var re = new Regex(@"[^a-zA-Z0-9]");
            var mc = re.Matches(LoginName);
            if (mc.Count > 0)
                errorMessages.AppendLine("Login Name is invalid");
            re = new Regex(@"[^^A-Za-z0-9#$^&@!~%=_]");
            mc = re.Matches(Password);
            if (mc.Count > 0)
                errorMessages.AppendLine("Password is invalid");

            var context = new ValidationContext(this, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(this, context, results);

            if (isValid) return errorMessages.ToString();

            foreach (var validationResult in results) errorMessages.AppendLine(validationResult.ErrorMessage);
            return errorMessages.ToString();
        }
    }
}