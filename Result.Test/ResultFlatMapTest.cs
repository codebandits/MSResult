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
            var result = new Result<double, string>(43.6);
            var newResult = result.FlatMap((value) => new Result<int, string>(Convert.ToInt32(value)));

            Assert.AreEqual(44, newResult.Success);
        }

        [TestMethod]
        public void ResultType_FlatMap_DoesNotTransform_WhenError()
        {
            var result = new Result<double, string>("haha! I hate doubles!");
            var newResult = result.FlatMap((value) => new Result<int, string>(Convert.ToInt32(value)));

            Assert.AreEqual("haha! I hate doubles!", newResult.Failure);

            TestHelper.ShouldThrow<InvalidOperationException>(() => { var value = newResult.Success; });
        }

        [TestMethod]
        public void ResultType_FlatMapError_TransformAndFlatResultFailure()
        {
            var result = new Result<double, string>("error1,error2");
            var newResult = result.FlatMapError((value) => new Result<double, string[]>(value.Split(",")));

            Assert.IsTrue(Enumerable.SequenceEqual(new string[] { "error1", "error2" }, newResult.Failure));
        }

        [TestMethod]
        public void ResultType_FlatMapError_ReturnsNewResultWithNewFailureType_WhenSucess()
        {
            var result = new Result<double, string>(3.4);
            var newResult = result.FlatMapError((value) => new Result<double, string[]>(value.Split(",")));

            Assert.AreEqual(3.4, newResult.Success);

            TestHelper.ShouldThrow<InvalidOperationException>(() => { var value = newResult.Failure; });
        }
    }
}
