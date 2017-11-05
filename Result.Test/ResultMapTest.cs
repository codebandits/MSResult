using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Result.Test
{
    [TestClass]
    public class ResultMapTest
    {
        [TestMethod]
        public void FlatMap_transformSuccessTypesWithALambda()
        {
            var oldResult = new Result<double, string>(42.5);

            Result<int, string> newResult = oldResult.Map(Convert.ToInt32);

            Assert.AreEqual(newResult.Success, 42);
        }

        [TestMethod]
        public void ResultType_Map_ReturnFailure_WhenFailsNoMattertheMappingFunction()
        {
            var failureResult = new Result<int, string>("go home please");
            var newFailureResult = failureResult.Map((value) => (value / 10.0));

            Assert.AreEqual("go home please", newFailureResult.Failure);

            TestHelper.ShouldThrow<InvalidOperationException>(() => { var value = newFailureResult.Success; });
        }

        [TestMethod]
        public void ResultType_MapError_TransformTheFailureType()
        {
            var result = new Result<double, string>("false");
            var newResultl = result.MapError((value) => Convert.ToBoolean(value));

            Assert.AreEqual(false, newResultl.Failure);
        }

        [TestMethod]
        public void ResultType_MapError_ReturnANewResultWithSucessAndNewFailure_WhenItIsSucess()
        {
            var result = new Result<int, string>(42);
            var newResult = result.MapError<int, string, string[]>((value) => value.Split(","));

            Assert.AreEqual(42, newResult.Success);

            TestHelper.ShouldThrow<InvalidOperationException>(() => { var value = newResult.Failure; });
        }
    }
}
