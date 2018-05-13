using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

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
            get => _selected;
            set => _selected = value;
        }

        [DataMember]
        public long ContactId
        {
            get => _contactId;
            set => _contactId = value;
        }

        /// <summary>
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "First Name Required")]
        [RegularExpression("[A-Za-z\\s]*", ErrorMessage = "First Name is invalid")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters allowed for First Name")]
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        /// <summary>
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Last Name Required")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters allowed for Last Name")]
        [RegularExpression("[A-Za-z\\s]*", ErrorMessage = "Last Name is invalid")]
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        /// <summary>
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Email Address Required")]
        [RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$",
            ErrorMessage = "Email address is invalid")]
        public string EmailAddress
        {
            get => _emailaddress;
            set => _emailaddress = value;
        }

        /// <summary>
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Phone Number Required")]
        [RegularExpression(
            @"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$",
            ErrorMessage = "Phone Number is invalid")]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        /// <summary>
        ///     Status(Possible values: Active/Inactive)
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Status Required")]
        public string Status
        {
            get => _status;
            set => _status = value;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string Validate()
        {
            var errorMessages = new StringBuilder();
            var context = new ValidationContext(this, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(this, context, results);

            if (isValid) return errorMessages.ToString();
            foreach (var validationResult in results) errorMessages.AppendLine(validationResult.ErrorMessage);
            return errorMessages.ToString();
        }
    }
}