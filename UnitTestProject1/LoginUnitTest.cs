using ContactMgmtCommon;
using ContactMgmtService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class LoginUnitTest
    {

        [TestMethod]
        public void InvalidLogin()
        {
            ILoginService contactMgmt = new AppLoginManager();
            bool returnValue = contactMgmt.ValidateLogin(new LoginInfo("vijay", "vijaymk"));
            Assert.AreEqual(returnValue, false);
        }

        [TestMethod]
        public void ValidLogin()
        {
            ILoginService contactMgmt = new AppLoginManager();
            bool returnValue = contactMgmt.ValidateLogin(new LoginInfo("vijay", "vijay#1975~"));
            Assert.AreEqual(returnValue, true);
        }
        
        [TestMethod]
        public void InValidLoginName()
        {
            ILoginService contactMgmt = new AppLoginManager();
            bool returnValue = contactMgmt.ValidateLogin(new LoginInfo("v#ijay", "vijay#1975~"));
            Assert.AreEqual(returnValue, false);
        }

        [TestMethod]
        public void InValidPassword()
        {
            ILoginService contactMgmt = new AppLoginManager();
            bool returnValue = contactMgmt.ValidateLogin(new LoginInfo("vijay", "vijay#*1975~"));
            Assert.AreEqual(returnValue, false);
        }
    }
}
