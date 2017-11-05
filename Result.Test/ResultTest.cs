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
            var result = new Result<int, string>(42);

            Assert.AreEqual(result.Success, 42);
        }

        [TestMethod]
        public void ResultType_SuccessDoesNotHaveFailure()
        {
            var result = new Result<int, string>(42);

            TestHelper.ShouldThrow<InvalidOperationException>(() => { var failure = result.Failure; } );
        }

        [TestMethod]
        public void ResultType_WhenFailure_RetursReason()
        {
            var result = new Result<Object, string>("It went so wrong!");

            Assert.AreEqual("It went so wrong!", result.Failure);
        }

        [TestMethod]
        public void ResultType_FailureDoesNotHaveSucess()
        {
            var result = new Result<Object, string>("RIP");

            TestHelper.ShouldThrow<InvalidOperationException>(() => { var failure = result.Success; });
        }
    }
}
