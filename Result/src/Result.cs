using System;
using System.Diagnostics.Contracts;

namespace Result
{

    public abstract class Result<S,F> { }

    sealed public class Success<S,F> : Result<S,F>
    {
        public readonly S content;

        public Success(S content)
        {
            this.content = content;
        }
    }

    sealed public class Failure<S,F>: Result<S,F>
    {
        public readonly F content;

        public Failure(F content)
        {
            this.content = content;
        }
    }
}
