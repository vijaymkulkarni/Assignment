using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ContactMgmtCommon
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class LoginInfo
    {
        private string _loginName;
        private string _password;

        /// <summary>
        /// 
        /// </summary>
        public LoginInfo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        public LoginInfo(string loginName, string password)
        {
            _loginName = loginName;
            _password = password;
        }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Login Name is missing")]
        [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Login Name is invalid")]
        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [RegularExpression("[A-Za-z0-9#$^&@!~%=_]*", ErrorMessage = "Password is invalid")]
        [Required(ErrorMessage = "Password is missing")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Validate()
        {
            var errorMessages = new StringBuilder();
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(this, context, results);

            if (isValid) return errorMessages.ToString();

            foreach (var validationResult in results)
            {
                errorMessages.AppendLine(validationResult.ErrorMessage);
            }
            return errorMessages.ToString();
        }
    }
}
