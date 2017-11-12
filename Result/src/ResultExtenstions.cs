using System;
namespace Result
{
    public static class ResultExtenstions
    {
        public static Result<NewSucess, F> Map<S, F, NewSucess>(this Result<S,F> result, Func<S, NewSucess> transform)
        {
            if(result is Success<S,F>)
            {
                return new Success<NewSucess, F>(transform(((Success<S, F>)result).content));
            } 
            else
            {
                return new Failure<NewSucess, F>(((Failure<S,F>)result).content);
            }
        }

        public static Result<S, NewFailureType> MapError<S, F, NewFailureType>(this Result<S, F> result, Func<F, NewFailureType> transform)
        {
            if (result is Failure<S, F>)
            {
                return new Failure<S, NewFailureType>(transform(((Failure<S, F>)result).content));
            }
            else
            {
                return new Success<S, NewFailureType>(((Success<S, F>)result).content);
            }
        }

        public static Result<NewSucess, F> FlatMap<S, F, NewSucess>(this Result<S, F> result, Func<S, Result<NewSucess, F>> transform)
        {
            if(result is Success<S,F>)
            {
                return transform(((Success<S,F>)result).content);
            }
            else
            {
                return new Failure<NewSucess, F>(((Failure<S,F>)result).content);
            }
        }

        public static Result<S, NewFailureType> FlatMapError<S, F, NewFailureType>(this Result<S, F> result, Func<F, Result<S, NewFailureType>> transform)
        {
            if( result is Failure<S,F>)
            {
                return transform(((Failure<S,F>)result).content);
            }
            else
            {
                return new Success<S, NewFailureType>(((Success<S,F>)result).content);
            
            }
        }
    }
}
