using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Core.Encryption;
using System;
using Simple.Core.Extensions;

namespace Simple.Utils.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string secret = "SEC7ce166f7bdbb93b793303612b4973045956475ab078696a384378c1ecd2243b5";
            string message = $"{DateTime.Now.GetTimestamp()}\n{secret}";
            string sign = SHA256Encryption.HMACSHA256(message, secret);
        }
    }
}