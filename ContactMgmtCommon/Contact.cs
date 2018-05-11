using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ContactMgmtCommon
{
    [DataContract]
    public class Contact
    {

        string _firstName;
        string _lastName;
        string _emailaddress;
        string _phoneNumber;
        bool _status;

        public Contact()
        {

        }

        public Contact(string contactFirstName, string contactLastName, string emailaddress, string phoneNumber, bool status, string firstName, string lastName, string emailAddress, string phonenNumber, bool contactStatus)
        {
            _firstName = contactFirstName;
            _lastName = contactLastName;
            _emailaddress = emailaddress;
            _phoneNumber = phoneNumber;
            _status = contactStatus;
        }

        //First Name
        [DataMember]
        [Required(ErrorMessage = "First Name Required")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters allowed for First Name")]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        //Last Name
        [DataMember]
        [Required(ErrorMessage = "Last Name Required")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters allowed for Last Name")]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        //Email
        [DataMember]
        [Required(ErrorMessage = "Email Address Required")]
        [RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Email address is invalid")]
        public string EmailAddress
        {
            get { return _emailaddress; }
            set { _emailaddress = value; }
        }

        //Phone Number
        [DataMember]
        [Required(ErrorMessage = "Phonen Number Required")]
        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Phone Number is invalid")]
        public string PhonenNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        //Status(Possible values: Active/Inactive)
        [DataMember]
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }        

        public string Validate()
        {
            StringBuilder errorMessages = new StringBuilder();
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

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
