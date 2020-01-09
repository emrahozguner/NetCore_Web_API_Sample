using API.Extensions;
using API.Resources;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Config
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorResource(errors);

            return new BadRequestObjectResult(response);
        }
    }
}