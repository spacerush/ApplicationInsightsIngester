using Collector.Models;
using Collector.Models.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collector.Services
{
    public interface IAuthenticationService
    {

        string HashPassword(string password);
        
        bool VerifyPassword(string password, string storedHash);

        WebSession CreateWebSession(string username);

        GetUserByCookieResponse GetUserByWebCookie(string cookie);

        bool TryLoginCredentials(string username, string password);
    }
}
