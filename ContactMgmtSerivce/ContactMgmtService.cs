using ContactMgmtCommon;
using ContactMgmtSerivce;
using System.Collections.Generic;
using System.Data;

namespace ContactMgmtService
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactMgmtService : ServiceMain, IContactService    
    {
        #region Properties

        private DataAccessLayerBase DataAccessLayer
        {
            get
            {
                var dataAccessLayerBase = DataAccessLayerBase.GetDataAccessLayer("contacts.xml", ConnectionType);
                return dataAccessLayerBase;
            }
        }

        #endregion

        #region IContactService implementation

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Contact> GetAllContacts()
        {
            if (DataAccessLayer == null) return new List<Contact>();

            DataTable table = DataAccessLayer.GetAllData();

            if (table == null) return new List<Contact>();
            
            List<Contact> contacts = new List<Contact>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                contacts.Add(new Contact() {
                                             ContactId = long.Parse(row[0].ToString()),
                                             FirstName = row[1].ToString(),
                                             LastName = row[2].ToString(),
                                             EmailAddress = row[3].ToString(),
                                             PhoneNumber = row[4].ToString(),
                                             Status = row[5].ToString()
                                            });
            }
            return contacts;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        public void GetMatchingContacts(Contact contactInfo)
        {
            //string filterExpression = string.Concat("Name = '", CreditialsLoginInfo.LoginName,
            //    "' and Password ='" + CreditialsLoginInfo.Password, "'");
            GetContactInfoDataTable(contactInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        public bool DeleteContact(ref List<Contact> contactInfo)
        {          
            var table = GetContactInfoDataTable(contactInfo);
            DataAccessLayer.DeleteData(ref table);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        public bool InsertContact(ref Contact contactInfo)
        {
            var table = GetContactInfoDataTable(contactInfo);
            DataAccessLayer.InsertData(ref table);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        public bool UpdateContact(ref Contact contactInfo)
        {
            var table = GetContactInfoDataTable(contactInfo);
            string filterExpression = string.Concat("ID = ", contactInfo.ContactId);
            DataRow[] rows = table.Select(filterExpression);
            foreach (DataRow dataRow in rows)
            {
                dataRow["FirstName"] = contactInfo.FirstName;
                dataRow["LastName"] = contactInfo.LastName;
                dataRow["EmailAddress"] = contactInfo.EmailAddress;
                dataRow["PhoneNumber"] = contactInfo.PhoneNumber;
                dataRow["Status"] = contactInfo.Status;
            }
            DataAccessLayer.UpdateData(ref table);
            return true;
        }

        #endregion

        #region Private Helper methods

        private DataTable GetContactDataTable()
        {
            var table = new DataTable("Contact");
            table.Columns.Add("ID");
            table.Columns.Add("FirstName");
            table.Columns.Add("LastName");
            table.Columns.Add("EmailAddress");
            table.Columns.Add("PhoneNumber");
            table.Columns.Add("Status");
            return table;
        }

        private DataTable GetContactInfoDataTable(Contact contactInfo)
        {
            List<Contact> contactInfolst = new List<Contact> {contactInfo};
            return GetContactInfoDataTable(contactInfolst);
        }

        private DataTable GetContactInfoDataTable(List<Contact> contactInfolst)
        {
            var table = GetContactDataTable(); 
            foreach (var contactInfo in contactInfolst)
            {
                AddNewRowInContactDataTable(table, contactInfo);
            }
            return table;
        }

        private void AddNewRowInContactDataTable(DataTable table, Contact contactInfo)
        {
            var row = table.NewRow();
            row["ID"] = contactInfo.ContactId;
            row["FirstName"] = contactInfo.FirstName;
            row["LastName"] = contactInfo.LastName;
            row["EmailAddress"] = contactInfo.EmailAddress;
            row["PhoneNumber"] = contactInfo.PhoneNumber;
            row["Status"] = contactInfo.Status;
            table.Rows.Add(row);
        }

        #endregion

        public void Dispose()
        {
            
        }
    }
}
