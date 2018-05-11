using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ContactMgmtCommon
{
    [DataContract]
    public class LoginInfo
    {
        string _loginName;
        string _password;

        public LoginInfo()
        {
        }

        public LoginInfo(string loginName, string password)
        {
            _loginName = loginName;
            _password = password;
        }


        //First Name
        [DataMember]
        [Required(ErrorMessage = "Login Name is missing")]
        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }

        //Last Name
        [DataMember]
        [Required(ErrorMessage = "Password is missing")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Validate()
        {
            StringBuilder errorMessages = new StringBuilder();
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(this, context, results);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    errorMessages.AppendLine(validationResult.ErrorMessage);
                }
            }
            return errorMessages.ToString();
        }
    }
}
