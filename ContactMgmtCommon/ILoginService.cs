using System;
using System.ServiceModel;

namespace ContactMgmtCommon
{
    /// <summary>
    /// </summary>
    [ServiceContract]
    public interface ILoginService : IDisposable
    {
        /// <summary>
        /// </summary>
        /// <param name="loginCreditials"></param>
        [OperationContract]
        [FaultContract(typeof(CustomException))]
        bool ValidateLogin(LoginInfo loginCreditials);
    }
}