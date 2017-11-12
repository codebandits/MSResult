using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Result.Test
{
    [TestClass]
    public class ResultTraverseTest
    {
        [TestMethod]
        public void Should_Traverse_AllSuccess_IntoList()
        {
            var listOfResults = new List<Result<string, string>>() {
                new Success<string, string>("1"),
                new Success<string, string>("2"),
                new Success<string, string>("3"),
                new Success<string, string>("4")
            };

            var resulOfList = (Success<List<string>, string>)listOfResults.Traverse();

            Assert.IsTrue(Enumerable.SequenceEqual(new string[] {"1", "2", "3", "4"}, resulOfList.content));
        }

        [TestMethod]
        public void Should_Return_FailureTypeWithError_When_ListContainsAFailure()
        {
            var listOfResults = new List<Result<string, string>>() {
                new Success<string, string>("1"),
                new Failure<string, string>("NaN"),
                new Success<string, string>("3"),
                new Success<string, string>("4")
            };

            var resulOfList = (Failure<List<string>, string>)listOfResults.Traverse();

            Assert.AreEqual("NaN", resulOfList.content);
        }
    }
}