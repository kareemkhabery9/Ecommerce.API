using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.CommenResponses
{

    // with methods return void
    public class Result
    {
        private readonly List<Error> _errors = [];
        public bool IsSuccess => _errors.Count == 0;
        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors => _errors;


        //success
        protected Result()
        {
            
        }

        //failure with single error
        protected Result(Error error)
        {
            _errors.Add(error);
        }

        //failure with some errors
        protected Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }


        //Factory Methods :

        public static Result Ok() => new Result();

        public static Result Fail(Error error) => new Result(error);

        public static Result Fail(List<Error> errors) => new Result(errors);


    }


    //with methods return value
    public class Result<TValue> : Result
    {
        private readonly TValue _value;


        public TValue Value => (IsSuccess) ? _value : throw new InvalidOperationException("you cannot access the value in case of failue scenario");


        //success
        private Result(TValue value) : base()
        {
            _value = value;
        }


        //failure with single error
        private Result(Error error) : base(error)
        {
            _value = default!;
        }


        //failure with single error
        private Result(List<Error> errors) : base(errors)
        {
            _value = default!;
        }


        // Factory Methods : 

        public static Result<TValue> Ok(TValue value) => new(value);

        public static new Result<TValue> Fail(Error error) => new(error);

        public static new Result<TValue> Fail(List<Error> errors) => new(errors);




    }



}
