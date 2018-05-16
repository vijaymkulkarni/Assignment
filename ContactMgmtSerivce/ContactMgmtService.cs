using ContactMgmtCommon;
using ContactMgmtService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ContactMgmtSerivce;

namespace ContactMgmtService
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactMgmtService : ServiceMain, IContactService
    {
        #region Properties

        private IDataAccess DataAccessLayer
        {
            get
            {
                var dataAccessLayerBase = DataAccessLayerFactory.GetDataAccessLayer(@"DataFiles\contacts.xml", ConnectionType);
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
            try
            {
                if (DataAccessLayer == null) return new List<Contact>();

                DataTable table = DataAccessLayer.GetAllData();

                if (table == null) return new List<Contact>();

                List<Contact> contacts = new List<Contact>(table.Rows.Count);
                foreach (DataRow row in table.Rows)
                {
                    contacts.Add(new Contact()
                    {
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
            catch (Exception e)
            {
                LogHelper.Log(LogType.Error, String.Concat(e.Message, Environment.NewLine, e.StackTrace));
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        public DataTable GetMatchingContacts(Contact contactInfo)
        {
            try
            {

                List<Contact> contacts = GetAllContacts();
                string filterExpression = string.Concat("ID = ", contactInfo.ContactId);
                Contact existingContact = (from contact in contacts
                                           where contact.ContactId == contactInfo.ContactId
                                           select contact).FirstOrDefault();

                return GetContactInfoDataTable(existingContact);
            }
            catch (Exception e)
            {
                LogHelper.Log(LogType.Error, String.Concat(e.Message, Environment.NewLine, e.StackTrace));
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        public bool DeleteContact(ref List<Contact> contactInfo)
        {
            try
            {

                var table = GetContactInfoDataTable(contactInfo);


                AuditHelper auditHelper = new AuditHelper("Delete");
                foreach (DataRow dataRow in table.Rows)
                {
                    auditHelper.AuditColumn("ContactId", dataRow["ID"], null);
                    auditHelper.AuditColumn("FirstName", dataRow["FirstName"], null);
                    auditHelper.AuditColumn("LastName", dataRow["LastName"], null);
                    auditHelper.AuditColumn("EmailAddress", dataRow["EmailAddress"], null);
                    auditHelper.AuditColumn("PhoneNumber", dataRow["PhoneNumber"], null);
                    auditHelper.AuditColumn("Status", dataRow["Status"], null);

                }
                auditHelper.EndAuditing();
                auditHelper = null;
                DataAccessLayer.DeleteData(ref table);
                return true;
            }
            catch (Exception e)
            {
                LogHelper.Log(LogType.Error, String.Concat(e.Message, Environment.NewLine, e.StackTrace));
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        public bool InsertContact(ref Contact contactInfo)
        {
            try
            {
                string errorMessage = contactInfo.Validate();

                if (!string.IsNullOrEmpty(errorMessage))
                    throw new System.Exception(errorMessage);

                var table = GetContactInfoDataTable(contactInfo);
                DataAccessLayer.InsertData(ref table);

                var dataRow = table.Rows[0];
                var auditHelper = new AuditHelper("Insert", dataRow["ID"]);
                auditHelper.AuditColumn("FirstName", dataRow["FirstName"], null);
                auditHelper.AuditColumn("LastName", dataRow["LastName"], null);
                auditHelper.AuditColumn("EmailAddress", dataRow["EmailAddress"], null);
                auditHelper.AuditColumn("PhoneNumber", dataRow["PhoneNumber"], null);
                auditHelper.AuditColumn("Status", dataRow["Status"], null);
                auditHelper.EndAuditing();
                auditHelper = null;

                return true;
            }
            catch (Exception e)
            {
                LogHelper.Log(LogType.Error, String.Concat(e.Message, Environment.NewLine, e.StackTrace));
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        public bool UpdateContact(ref Contact contactInfo)
        {
            try
            {
                string errorMessage = contactInfo.Validate();
                if (!string.IsNullOrEmpty(errorMessage))
                    throw new System.Exception(errorMessage);

                var table = GetMatchingContacts(contactInfo);

                string filterExpression = string.Concat("ID = ", contactInfo.ContactId);
                DataRow[] rows = table.Select(filterExpression);

                AuditHelper auditHelper = new AuditHelper("Update", contactInfo.ContactId);
                foreach (DataRow dataRow in rows)
                {
                    auditHelper.AuditColumn("FirstName", dataRow["FirstName"], contactInfo.FirstName);
                    auditHelper.AuditColumn("LastName", dataRow["LastName"], contactInfo.LastName);
                    auditHelper.AuditColumn("EmailAddress", dataRow["EmailAddress"], contactInfo.EmailAddress);
                    auditHelper.AuditColumn("PhoneNumber", dataRow["PhoneNumber"], contactInfo.PhoneNumber);
                    auditHelper.AuditColumn("Status", dataRow["Status"], contactInfo.Status);
                    dataRow["FirstName"] = contactInfo.FirstName;
                    dataRow["LastName"] = contactInfo.LastName;
                    dataRow["EmailAddress"] = contactInfo.EmailAddress;
                    dataRow["PhoneNumber"] = contactInfo.PhoneNumber;
                    dataRow["Status"] = contactInfo.Status;
                }
                auditHelper.EndAuditing();
                auditHelper = null;
                DataAccessLayer.UpdateData(ref table);
                return true;
            }
            catch (Exception e)
            {
                LogHelper.Log(LogType.Error, String.Concat(e.Message,Environment.NewLine,e.StackTrace));
                throw;
            }
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
            List<Contact> contactInfolst = new List<Contact> { contactInfo };
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
