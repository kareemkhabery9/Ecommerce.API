using Ecommerce.Shared.CommenResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {


        //Handle result without value
        //if result is success => return with NoContent 204
        //if result is failure => return problem details with along with desc, status code
        
        protected IActionResult HandleResult (Result result)
        {
            if(result.IsSuccess)
                return NoContent();

            else
                return HandleProblem(result.Errors);
        }



        //Handle result with value
        //if result is success => return Ok 200 with value
        //if result is failure => return problem details with along with desc, status code

        protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result)
        {

            if (result.IsSuccess)
                return Ok(result.Value);

            else
                return HandleProblem(result.Errors);

        }


        private ActionResult HandleProblem (IReadOnlyList<Error> errors)
        {
            //If No Errors are proveded, return a generic problem details response (500 Error )
            if (errors.Count == 0)
            {
                return Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "An Error Occured"
                    );
            }


            //If All Errors are Validation Errors, Handle them as a validation problem 
            if (errors.All(E => E.ErrorType == ErrorType.Validation))
                return HandleValidationErrors(errors);


            //If there is only one error, Handle is as a single error problem

            return HandleSingleError(errors[0]);


        }


        private ActionResult HandleSingleError (Error error)
        {
            return Problem(
                title: error.Code,
                detail: error.Description,
                type: error.ErrorType.ToString(),
                statusCode: MapErrorTypeIntoStutesCode(error.ErrorType)
                );
        }

        private ActionResult HandleValidationErrors (IReadOnlyList<Error> errors)
        {
            var modelState = new ModelStateDictionary();

            foreach(var error in errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem(modelState);

        }


        private static int MapErrorTypeIntoStutesCode(ErrorType errorType)

    => errorType switch
    {
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.UnAuthorized => StatusCodes.Status401Unauthorized,
        ErrorType.Forbidden => StatusCodes.Status403Forbidden,
        ErrorType.InvalidCredintals => StatusCodes.Status401Unauthorized,
        _ => StatusCodes.Status500InternalServerError,
    };

    }
}
