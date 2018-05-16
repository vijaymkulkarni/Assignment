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
    public class Contact
    {
        private long _contactId;
        private string _emailaddress;
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private bool _selected;
        private string _status;

        [DataMember]
        public bool IsSelected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        [DataMember]
        public long ContactId
        {
            get
            {
                return _contactId;
            }
            set { _contactId = value; }
        }

        /// <summary>
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "First Name Required")]
        [RegularExpression("[A-Za-z\\s]*", ErrorMessage = "First Name is invalid")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters allowed for First Name")]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value; }
        }

        /// <summary>
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Last Name Required")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters allowed for Last Name")]
        [RegularExpression("[A-Za-z\\s]*", ErrorMessage = "Last Name is invalid")]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        /// <summary>
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Email Address Required")]
        [RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$",
            ErrorMessage = "Email address is invalid")]
        public string EmailAddress
        {
            get { return _emailaddress; }
            set { _emailaddress = value; }
        }

        /// <summary>
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Phone Number Required")]
        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Phone Number is invalid")]
        public string PhoneNumber
        {
            get
            { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        /// <summary>
        ///     Status(Possible values: Active/Inactive)
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Status Required")]
        public string Status
        {
            get
            { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string Validate()
        {
            var errorMessages = new StringBuilder();
            
            if (!Regex.IsMatch(FirstName, @"^[A-Za-z.'\-\p{L}\p{Zs}\p{Lu}\p{Ll}\']+$", 
                    RegexOptions.IgnoreCase, System.TimeSpan.FromMilliseconds(250)))
                errorMessages.AppendLine("First Name is invalid");

            if (!Regex.IsMatch(LastName, @"^[A-Za-z.'\-\p{L}\p{Zs}\p{Lu}\p{Ll}\']+$", RegexOptions.IgnoreCase, System.TimeSpan.FromMilliseconds(250)))
                errorMessages.AppendLine("Last Name is invalid");

            if (!Regex.IsMatch(EmailAddress, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                                           RegexOptions.IgnoreCase, System.TimeSpan.FromMilliseconds(250)))
                errorMessages.AppendLine("Email Address is invalid");

            if (!Regex.IsMatch(PhoneNumber, @"\+[0-9]{1,3}\s+[0-9]{2,3}\s+[0-9]{1,9}|\+[0-9]{1,3}\s+[0-9]{10,11}",  RegexOptions.IgnoreCase, System.TimeSpan.FromMilliseconds(250)))
                errorMessages.AppendLine("Phone Number is invalid");

            var context = new ValidationContext(this, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(this, context, results);

            if (isValid) return errorMessages.ToString();
            foreach (var validationResult in results) errorMessages.AppendLine(validationResult.ErrorMessage);
            return errorMessages.ToString();
        }
    }    
}