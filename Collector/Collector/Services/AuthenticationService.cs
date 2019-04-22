using MongoDB.Driver;
using Collector.Helpers;
using Collector.Models;
using Collector.Models.ServiceResponse;
using Collector.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Collector.Models.Documents;

namespace Collector.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IRepositoryWrapper _wrapper;
        private readonly IMongoClient _mongoClient;
        private readonly Random _random;

        public AuthenticationService(IMongoClient client)
        {
            _mongoClient = client;
            _wrapper = new RepositoryWrapper(_mongoClient);
            _random = new Random();
        }

        public string HashPassword(string password)
        {
            // Generate the hash, with an automatic 32 byte salt
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 32);
            rfc2898DeriveBytes.IterationCount = 10000;
            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            byte[] salt = rfc2898DeriveBytes.Salt;
            //Return the salt and the hash
            return rfc2898DeriveBytes.IterationCount + "|" + Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            // Generate the hash, with an automatic 32 byte salt
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(storedHash.Split('|')[1]));
            rfc2898DeriveBytes.IterationCount = 10000;
            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            byte[] salt = rfc2898DeriveBytes.Salt;
            //Return the salt and the hash
            if (Convert.ToBase64String(hash) == storedHash.Split('|')[2])
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a new web session and return it for a given user.
        /// </summary>
        /// <param name="username">The username for which the session is for.</param>
        /// <returns>A WebSession</returns>
        public WebSession CreateWebSession(string username)
        {
            WebSession webSession = new WebSession();
            CollectorUser reportUser = _wrapper.UserRepository.GetOne<CollectorUser>(f => f.Username == username);
            if (reportUser != null)
            {
                webSession.Expiry = DateTime.UtcNow.AddDays(14);
                webSession.ReportUserId = reportUser.Id;
                webSession.ForReportUsername = username;
                webSession.SessionCookie = GenerationHelper.CreateRandomString(true, true, false, 32);
                _wrapper.WebSessionRepository.AddOne<WebSession>(webSession);
            }
            return webSession;
        }

        public GetUserByCookieResponse GetUserByWebCookie(string cookie)
        {
            GetUserByCookieResponse result = new GetUserByCookieResponse();
            WebSession webSession = _wrapper.WebSessionRepository.GetOne<WebSession>(f => f.SessionCookie == cookie);
            if (webSession != null && webSession.Expiry > DateTime.UtcNow)
            {
                result.User = _wrapper.UserRepository.GetOne<CollectorUser>(f => f.Id == webSession.ReportUserId);
                result.Success = true;
            }
            else
            {
                result.User = null;
                result.Success = false;
            }
            return result;
        }

        public bool TryLoginCredentials(string username, string password)
        {
            CollectorUser user = _wrapper.UserRepository.GetOne<CollectorUser>(f => f.Username == username);
            if (user == null)
            {
                long usercount = _wrapper.UserRepository.Count<CollectorUser>(c => c.Id != null);
                if (usercount == 0)
                {
                    CollectorUser newUser = new CollectorUser();
                    newUser.PasswordHash = HashPassword(password);
                    newUser.Username = username;
                    newUser.IsOrganizationAdmin = true;
                    newUser.OrganizationRoles = new List<string>();
                    newUser.OrganizationRoles.Add("Admin");
                    _wrapper.UserRepository.AddOne<CollectorUser>(newUser);
                    user = newUser;
                }
                else
                {
                    return false;
                }
            }
            if (VerifyPassword(password, user.PasswordHash))
            {
                return true;
            }
            return false;

        }

    }
}
