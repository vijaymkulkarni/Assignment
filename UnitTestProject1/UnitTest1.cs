using System;
using ContactMgmtCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactMgmtSerivce;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InvalidLogin()
        {
            try
            {
                ILoginService contactMgmt = new AppLoginManager();
                contactMgmt.ValidateLogin(new LoginInfo("vijay", "vijaymk"));
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
    }
}
