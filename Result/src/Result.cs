﻿using System;
using System.Diagnostics.Contracts;

namespace Result
{
    sealed public class Result<S,F>
    {
        private readonly S data;
        private readonly F failure;

        private readonly Boolean isSucess;

        public Result(S data) {
            this.data = data;
            this.isSucess = true;
        }

        public Result(F failure) {
            this.failure = failure;
            this.isSucess = false;
        }
       
        public S Success
        {
            get {
                if (!isSucess)
                    throw new InvalidOperationException("it is not sucess!");

                return data;
            }
        }

        public F Failure
        {
            get {
                if (isSucess)
                    throw new InvalidOperationException("it is a sucess");

                return failure;
            }  
        }
    }
}
