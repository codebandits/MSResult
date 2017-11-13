using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using Result;


namespace Result.Test
{
    [TestClass]
    public class ResultBindTest
    {

        [TestMethod]
        public void Bind_Should_ElevateFunctionFromTypeToResult_ToResultToResult()
        {
            Func<string, Result<string, string>> fun = (string a) => new Success<string, string>(a+", world");

            Func<Result<string, string>,Result<string, string>> newFunc = ResultExtenstions.Bind<string,string>(fun);

            var output = newFunc(new Success<string, string>("hello"));

            Assert.AreEqual("hello, world", ((Success<string, string>)output).content);
        }

        [TestMethod]
        public void Bind_Should_SkipFunctionWhenError()
        {
            // TODO learn how to mock a whole function

            Func<string, Result<string, int>> fun = (string a) => new Success<string, int>(a + ", no please");

            Func<Result<string, int>, Result<string, int>> newFunc = ResultExtenstions.Bind<string, int>(fun);

            var output = newFunc(new Failure<string, int>(0));

            Assert.AreEqual(0, ((Failure<string, int>)output).content);
        }
    }
}
