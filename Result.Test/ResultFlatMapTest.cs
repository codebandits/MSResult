using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Result.Test
{
    [TestClass]
    public class ResultFlatMapTest
    {
        [TestMethod]
        public void FlatMap_transformSuccessTypesWithALambda()
        {
            var oldResult = new Result<double, string>(42.5);

            Result<int, string> newResult = oldResult.FlatMap(Convert.ToInt32);

            Assert.AreEqual(newResult.Success, 42);
            
        }
    }
}
