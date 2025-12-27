using Microsoft.AspNetCore.Mvc.Filters;

namespace RentCar.Filters
{
    public class RedirectIfAuthenticate : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.User.Identity!.IsAuthenticated)
            {
                context.HttpContext.Response.Redirect("/Car");
            }
        }
    }
}
