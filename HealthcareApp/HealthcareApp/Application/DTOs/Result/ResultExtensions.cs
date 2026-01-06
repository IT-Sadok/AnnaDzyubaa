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
                return successMessage != null
                    ? new OkObjectResult(successMessage) 
                    : new OkResult();
            }

            return new BadRequestObjectResult(result.Error);
        }
    }
}
