using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Application.DTOs.Result
{
    public static class ResultExtensions
    {
        public static IActionResult ToResponse<T>(this Result<T> result, string? successMessage = null)
        {
            if (result.IsSuccess)
            {
                if (successMessage != null)
                {
                    return new OkObjectResult(successMessage);
                }
                else if (result.Body != null)
                {
                    return new OkObjectResult(result.Body);
                }else
                {
                    return new OkResult();
                }
            }

            return new BadRequestObjectResult(result.Error);
        }
    }
}