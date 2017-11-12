using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Result.Test
{
    [TestClass]
    public class ResultFlatMapTest
    {
        [TestMethod]
        public void ResultType_FlatMap_TransformAndFlatResult()
        {
            var result = new Success<double, string>(43.6);
            var newResult = (Success<int, string>)result.FlatMap((value) => new Success<int, string>(Convert.ToInt32(value)));

            Assert.AreEqual(44, newResult.content);
        }

        [TestMethod]
        public void ResultType_FlatMap_DoesNotTransform_WhenError()
        {
            var result = new Failure<double, string>("haha! I hate doubles!");
            var newResult = (Failure<int, string>)result.FlatMap((value) => new Success<int, string>(Convert.ToInt32(value)));

            Assert.AreEqual("haha! I hate doubles!", newResult.content);
        }

        [TestMethod]
        public void ResultType_FlatMapError_TransformAndFlatResultFailure()
        {
            var result = new Failure<double, string>("error1,error2");
            var newResult = (Failure<double, string[]>)result.FlatMapError((value) => new Failure<double, string[]>(value.Split(",")));

            Assert.IsTrue(Enumerable.SequenceEqual(new string[] { "error1", "error2" }, newResult.content));
        }

        [TestMethod]
        public void ResultType_FlatMapError_ReturnsNewResultWithNewFailureType_WhenSucess()
        {
            var result = new Success<double, string>(3.4);
            var newResult = (Success<double, string[]>)result.FlatMapError((value) => new Failure<double, string[]>(value.Split(",")));

            Assert.AreEqual(3.4, newResult.content);
        }
    }
}
