using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Factories
{
    public static class ApiResponseFactory
    {

        public static IActionResult GenerateApiVaildationResponse(ActionContext actionContext)
        {

            var errors = actionContext.ModelState.Where(x => x.Value!.Errors.Count > 0)
                .ToDictionary(
                        x => x.Key,
                        x => x.Value!.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            var problem = new ProblemDetails()
            {
                Title = "validation Errors",
                Detail = "One or more validation errors ocurred",
                Status = StatusCodes.Status400BadRequest,
                Extensions = { { "Errors", errors } },
            };


            return new BadRequestObjectResult(problem);

        }

    }
}
