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
        /// Validate Login operation contract
        /// </summary>
        /// <param name="loginCreditials">Login information</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(CustomException))]
        bool ValidateLogin(LoginInfo loginCreditials);
    }
}