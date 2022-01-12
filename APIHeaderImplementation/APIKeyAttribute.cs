//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace APIHeaderImplementation
//{
//    public class APIKeyAttribute
//    {
//    }
//}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MobileInternetBankingWebAPI.BAL.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class APIKeyAttribute : Attribute, IAsyncActionFilter
    {
        public APIKeyAttribute()
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string UnauthMsg = $"Error 401: Unauthorzed.";
            try
            {
                //get below from your Database. But currently hard coded
                string getValueFromDatase = "TY^*53535HHU)";
                /*
                     Note:
                         if you are targetting multple header, use  below
                         var headerKeyFromPostMan = context.HttpContext.Request?.Headers["ApiKey"][0];
                        if you are targetting single header use below
                        context.HttpContext.Request.Headers.TryGetValue("ApiKey", out var token))
                 */
                if (context.HttpContext.Request.Headers.TryGetValue("ApiKey", out var token))
                {
                    //below means get the header that has "ApiKey" has it key name in PostMan
                    var headerKeyFromPostMan = context.HttpContext.Request?.Headers["ApiKey"][0];

                    if (!getValueFromDatase.Equals(headerKeyFromPostMan))
                    {
                        context.Result = new UnauthorizedResult();

                    }
                    await next();
                }
                else
                {
                    // Failure use your log here
                    // _logger.LogWarning('Header not found');

                    context.Result = new ContentResult()
                    {
                        StatusCode = 404,
                        Content = UnauthMsg
                    };
                }
            }
            catch (Exception ex)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 404,
                    Content = UnauthMsg
                };
            }
        }
    }
}

