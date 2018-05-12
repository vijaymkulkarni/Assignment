using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ContactMgmtCommon
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [ServiceContract]
    public interface IContactService : IDisposable
    {
    
        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void InsertContact(Contact contactInfo);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void UpdateContact(Contact contactInfo);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void DeleteContact(List<Contact> contactInfo);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void GetMatchingContacts(Contact contactInfo);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        List<Contact> GetAllContacts();
    }

}
