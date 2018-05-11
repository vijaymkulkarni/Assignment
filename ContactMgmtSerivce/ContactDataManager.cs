using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactMgmtCommon;

namespace ContactMgmtSerivce
{
    public class ContactDataManager : IContactService
    {
        private static string _connectionType;
        private const string ConnectionTypeKey = "connectionType";

        public static string ConnectionType
        {
            get
            {
                if (_connectionType == string.Empty)
                {
                    _connectionType = ConfigurationManager.ConnectionStrings[ConnectionTypeKey].ConnectionString;
                }
                return _connectionType;
            }
        }
        
        public void InsertContact(Contact contactInfo)
        {
            throw new NotImplementedException();
        }

        public void UpdateContact(Contact contactInfo)
        {
            throw new NotImplementedException();
        }

        public void DeleteContact(List<Contact> contactInfo)
        {
            throw new NotImplementedException();
        }

        public void GetMatchingContacts(Contact contactInfo)
        {
            throw new NotImplementedException();
        }

        public void GetAllContacts()
        {
            throw new NotImplementedException();
        }
    }
}
