using Microsoft.AspNetCore.Http;
using Petfinder.Models;
using System.Linq;
using System.Security.Claims;


namespace Petfinder.Helpers
{
    public class UserHelper
    {
        public static User GetCurrentUser(HttpContext httpContext, PetfinderContext _context)
        {
            var userName = httpContext.User.FindFirst(ClaimTypes.Name).Value;
            User currentUser = _context.Users.First(m => m.UserName == userName);
            return currentUser;
        }
    }
}
