using ContactMgmtCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactMgmtService;

namespace ContactMgmtService
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactMgmtService : IContactService    
    {
        private string _connectionType;
        private const string ConnectionTypeKey = "connectionType";

        public string ConnectionType
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionType))
                {
                    _connectionType = ConfigurationManager.AppSettings[ConnectionTypeKey];
                }
                return _connectionType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ContactMgmtService()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        public void DeleteContact(List<Contact> contactInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Contact> GetAllContacts()
        {
            DataAccessLayer dataAccessLayer = null;
            dataAccessLayer = GetDataAccessLayer();

            if (dataAccessLayer == null)
                return new List<Contact>();

            //string filterExpression = string.Concat("Name = '", CreditialsLoginInfo.LoginName,
            //    "' and Password ='" + CreditialsLoginInfo.Password, "'");
            DataTable table = dataAccessLayer.GetAllData();
            if (table != null)
            {
                List<Contact> contacts = new List<Contact>(table.Rows.Count);
                foreach (DataRow row in table.Rows)
                {
                    contacts.Add(new Contact() { FirstName = row[0].ToString(),
                                                 LastName = row[1].ToString(),
                                                 EmailAddress = row[2].ToString(),
                                                 PhoneNumber = row[3].ToString(),
                                                 Status = bool.Parse(row[4].ToString())
                                                });
                }
                return contacts;
            }
            else
                return new List<Contact>(); 
        }

        private DataAccessLayer GetDataAccessLayer()
        {
            DataAccessLayer dataAccessLayer = null;

            // ReSharper disable once StringCompareIsCultureSpecific.1
            if (String.Compare(ConnectionType.Trim().ToUpper(), "TEXT") == 0)
            {
                dataAccessLayer = new FileSystemDataMgr("Contacts.xml");
            }
            // ReSharper disable once StringCompareIsCultureSpecific.1
            else if (String.Compare(ConnectionType.Trim().ToUpper(), "SQL") == 0)
            {
                dataAccessLayer = new SqlDataMgr();
            }
            return dataAccessLayer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        public void GetMatchingContacts(Contact contactInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        public void InsertContact(Contact contactInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        public void UpdateContact(Contact contactInfo)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //this.Dispose();
        }
    }
}
