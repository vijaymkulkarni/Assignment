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
    [ServiceContract]
    public interface ILoginService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginCreditials"></param>
        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void ValidateLogin(LoginInfo loginCreditials);

    }
}
