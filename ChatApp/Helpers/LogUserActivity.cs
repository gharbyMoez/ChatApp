using ChatApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ChatApp.Helpers;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

        var userId = resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var uow = resultContext.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
        /*var user = await uow.UserRepository.GetUserByIdAsync(userId);
        *//* user.LastActive = DateTime.UtcNow;*//*
        await uow.Complete();*/
    }
}
