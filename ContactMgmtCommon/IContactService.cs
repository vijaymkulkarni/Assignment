using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ContactMgmtCommon
{
    /// <summary>
    /// </summary>
    [ServiceContract]
    public interface IContactService : IDisposable
    {
        [OperationContract]
        [FaultContract(typeof(CustomException))]
        bool InsertContact(ref Contact contactInfo);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        bool UpdateContact(ref Contact contactInfo);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        bool DeleteContact(ref List<Contact> contactInfo);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void GetMatchingContacts(Contact contactInfo);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        List<Contact> GetAllContacts();
    }
}