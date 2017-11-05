using System;
namespace Result
{
    public static class ResultExtenstions
    {
        public static Result<NewSucess, F> Map<S, F, NewSucess>(this Result<S,F> result, Func<S, NewSucess> transform)
        {
            try {
                return new Result<NewSucess, F>(transform(result.Success));
            } catch (InvalidOperationException) {
                return new Result<NewSucess, F>(result.Failure);
            }
        }

        public static Result<S, NewFailureType> MapError<S, F, NewFailureType>(this Result<S, F> result, Func<F, NewFailureType> transform)
        {
            try
            {
                return new Result<S, NewFailureType>(transform(result.Failure));
            }
            catch (InvalidOperationException)
            {
                return new Result<S, NewFailureType>(result.Success);
            
            }
        }

        public static Result<NewSucess, F> FlatMap<S, F, NewSucess>(this Result<S, F> result, Func<S, Result<NewSucess, F>> transform)
        {
            try
            {
                return transform(result.Success);
            }
            catch (InvalidOperationException)
            {
                return new Result<NewSucess, F>(result.Failure);
            }
        }

        public static Result<S, NewFailureType> FlatMapError<S, F, NewFailureType>(this Result<S, F> result, Func<F, Result<S, NewFailureType>> transform)
        {
            try
            {
                return transform(result.Failure);
            }
            catch (InvalidOperationException)
            {
                return new Result<S, NewFailureType>(result.Success);
            
            }
        }
    }
}
