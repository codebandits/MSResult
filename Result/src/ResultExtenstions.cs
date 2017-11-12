using System;
using System.Linq;
using System.Collections.Generic;

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

        public static Result<List<S>, F> Traverse<S, F>(this List<Result<S, F>> resultList)
        {
            return resultList.Aggregate(
                new Success<List<S>, F>(new List<S>()),
                (Result<List<S>, F> acc, Result<S, F> result) =>
                {
                    if (acc is Success<List<S>, F>)
                    {
                        if (result is Success<S, F>)
                        {
                            var list = ((Success<List<S>, F>)acc).content;
                            list.Add(((Success<S, F>)result).content);
                            return new Success<List<S>, F>(list);
                        }
                        else
                        {
                            var failure = ((Failure<S, F>)result).content;
                            return new Failure<List<S>, F>(failure);
                        }
                    }
                    else
                    {
                        return acc;
                    }
                }
            );
        }
    }
}
