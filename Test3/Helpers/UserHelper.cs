using Microsoft.AspNetCore.Http;
using Petfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Petfinder.Data;


namespace Petfinder.Helpers
{
    public class UserHelper
    {
        public static User GetCurrentUser (HttpContext httpContext, PetfinderContext _context)
        {
            var userName = httpContext.User.FindFirst(ClaimTypes.Name).Value;
            User currentUser = _context.Users.First(m => m.UserName == userName);
            return currentUser;
        }
    }
}
