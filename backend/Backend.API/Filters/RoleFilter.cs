namespace Backend.API.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Threading.Tasks;

    public class RoleFilter : Attribute, IAsyncActionFilter
    {
        //private readonly Role? _role;

        //public RoleFilter()
        //{
        //    _role = null;
        //}

        //public RoleFilter(Role role)
        //{
        //    _role = role;
        //}

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            //    if (_role is null)
            //    {
            //        await next();
            //        return;
            //    }

            //    var ct = context.HttpContext.RequestAborted;

            //    var authenticationService = context.HttpContext.RequestServices.GetRequiredService<IAuthenticationService>();

            //    User user = await authenticationService.GetCurrentUserAsync(ct);

            //    if (user is null)
            //        throw new BusinessLogicException(ExtensionCode.NoRights);


            //    if (_permissions.ContainsKey(_role.Value) && _permissions[_role.Value].Contains(user.Role))
            //    {
            //        await next();
            //        return;
            //    }

            //    throw new BusinessLogicException(ExtensionCode.NoRights);
            //    //await next();
            //}

            //private static readonly Dictionary<Role, Role[]> _permissions = new Dictionary<Role, Role[]>
            //{
            //    {   Role.User,     [ Role.User, Role.Admin ]   },
            //    {   Role.Admin,    [ Role.Admin ]              },
            //};
        }
    }
}
