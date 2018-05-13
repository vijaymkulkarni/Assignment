using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactMgmtService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactMgmtCommon;

namespace ContactMgmtService.Tests
{
    [TestClass()]
    public class ContactMgmtServiceTests
    {
        private static Random rng = new Random();

        [TestMethod()]
        public void GetAllContactsTest()
        {
            IContactService contactMgmt = new ContactMgmtService();
            List<Contact> contacts = contactMgmt.GetAllContacts();
            Assert.AreNotEqual(contacts, null);
        }

        [TestMethod()]
        public void InsertContactTest()
        {
            var contact = GetRandomContact();
            using (IContactService contactMgmt = new ContactMgmtService())
            {
                bool returnValue = contactMgmt.InsertContact(ref contact);
                Assert.AreEqual(returnValue, true);
            }
            contact = null;
        }


        [TestMethod()]
        public void UpdateContactTest()
        {
            var contact = GetRandomContact();
            using (IContactService contactMgmt = new ContactMgmtService())
            {
                bool returnValue = contactMgmt.InsertContact(ref contact);
                Assert.AreEqual(returnValue, true, "Insert not successful");
            }

            contact.EmailAddress = string.Concat(newText(), @"@", "outlook.com");
            using (IContactService contactMgmt = new ContactMgmtService())
            {
                bool returnValue = contactMgmt.UpdateContact(ref contact);
                Assert.AreEqual(returnValue, true);
            }
            contact = null;
        }

        [TestMethod()]
        public void DeleteContactTest()
        {
            var contact = GetRandomContact();
            var secondContact = GetRandomContact();
            using (IContactService contactMgmt = new ContactMgmtService())
            {
                bool returnValue = contactMgmt.InsertContact(ref contact);
                Assert.AreEqual(returnValue, true, "First Insert not successful");

                returnValue = contactMgmt.InsertContact(ref secondContact);
                Assert.AreEqual(returnValue, true, "Second Insert not successful");

                contact.EmailAddress = string.Concat(newText(), @"@", "outlook.com");

                returnValue = contactMgmt.UpdateContact(ref contact);
                Assert.AreEqual(returnValue, true);
            }
            contact = null;
        }
        
        private static Contact GetRandomContact()
        {
            Contact contact = new Contact
            {
                ContactId = rng.Next(),
                FirstName = newText(),
                LastName = newText(),
                EmailAddress = string.Concat(newText(), @"@", "gmail.com"),
                PhoneNumber = rng.Next().ToString(),
                Status = "Active"
            };
            return contact;
        }

        //Reference : https://bytes.com/topic/c-sharp/answers/671528-c-random-alphanumeric-strings
        public static string newText()
        {
            char[] valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVW XYZ1234567890".ToCharArray(); ;
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < 10; i++)
            {
                sb.Append(valid[rng.Next(valid.Length)]);
            }
            return sb.ToString();
        }
    }
}