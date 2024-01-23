using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
       if (!context.HttpContext.Request.Query.TryGetValue(Configuration.apiKeyname, out var extractedApiKey))
       {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "ApiKey n~ao encontrada"
            };
            return;
       }

       if(!Configuration.apiKey.Equals(extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "acesso não autorizado"
            };
            return;
        }

       await next();
    }
}
