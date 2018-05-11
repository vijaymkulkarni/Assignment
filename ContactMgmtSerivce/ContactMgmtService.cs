using ContactMgmtCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactMgmtSerivce;

namespace ContactMgmtSerivce
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactMgmtService : IContactService    
    {
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
            throw new NotImplementedException();
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
    }
}
