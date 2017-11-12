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
            var oldResult = new Success<double, string>(42.5);

            var newResult = oldResult.Map(Convert.ToInt32);

            int actualValue = ((Success<int, string>)newResult).content; 
            Assert.AreEqual(actualValue, 42);
        }

        [TestMethod]
        public void ResultType_Map_ReturnFailure_WhenFailsNoMattertheMappingFunction()
        {
            var failureResult = new Failure<int, string>("go home please");
            var newFailureResult = (Failure<double, string>) failureResult.Map((value) => (value / 10.0));

            Assert.AreEqual("go home please", newFailureResult.content);
        }

        [TestMethod]
        public void ResultType_MapError_TransformTheFailureType()
        {
            var result = new Failure<double, string>("false");
            var newResultl = (Failure<double, Boolean>)result.MapError((value) => Convert.ToBoolean(value));

            Assert.AreEqual(false, newResultl.content);
        }

        [TestMethod]
        public void ResultType_MapError_ReturnANewResultWithSucessAndNewFailure_WhenItIsSucess()
        {
            var result = new Success<int, string>(42);
            var newResult = (Success<int, string[]>) result.MapError<int, string, string[]>((value) => value.Split(","));

            Assert.AreEqual(42, newResult.content);
        }
    }
}
