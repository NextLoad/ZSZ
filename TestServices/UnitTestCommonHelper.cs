using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZSZ.Common;

namespace TestServices
{
    [TestClass]
    public class UnitTestCommonHelper
    {
        [TestMethod]
        public void TestToDBC()
        {
            Assert.AreEqual("admin",CommonHelper.ToDBC("admin"));
            Assert.AreEqual("admin",CommonHelper.ToDBC("ａｄｍｉn"));
            Assert.AreEqual("6226f7cbe59e99a90b5cef6f94f966fd",CommonHelper.CalcMD5("sd"));
            Assert.AreEqual("4297f44b13955235245b2497399d7a93", CommonHelper.CalcMD5("123123"));
        }
    }
}
