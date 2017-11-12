using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Result.Test
{
    [TestClass]
    public class ResultTest
    {
        [TestMethod]
        public void ResultType_WhenItHasASucess_ReturnsSucessValue()
        {
            var result = new Success<int, string>(42);

            Assert.AreEqual(result.content, 42);
        }

        [TestMethod]
        public void ResultType_WhenFailure_RetursReason()
        {
            var result = new Failure<Object, string>("It went so wrong!");

            Assert.AreEqual("It went so wrong!", result.content);
        }

        [TestMethod]
        public void ResultType_WithSameTypes_ShouldBeAbleToSayIfItIsSuccessOrFailure()
        {
            var success = new Success<string, string>("my sucess");

            Assert.AreEqual(success.content, "my sucess");
        
            var failure = new Failure<string, string>("my failure");

            Assert.AreEqual(failure.content, "my failure");
        }
    }
}
