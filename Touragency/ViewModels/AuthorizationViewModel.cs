using System;
using System.Linq;
using Touragency.Model;

namespace Touragency.ViewModels
{
    public class AuthorizationViewModel
    {
        public event EventHandler AuthenticationSucceeded;
        public event EventHandler AuthenticationFailed;

        public void Authenticate(string login, string password)
        {
            if ((string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password)))
            {
                AuthenticationFailed?.Invoke(this, EventArgs.Empty);
                return;
            }

            bool isAuthenticated = CheckCredentials(login, password);

            if (isAuthenticated)
            {
                AuthenticationSucceeded?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                AuthenticationFailed?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool CheckCredentials(string login, string password)
        {
            var users = UserGenerator.GenerateSampleUsers();
            User user = users.FirstOrDefault(u => u.Username == login && u.Password == password);
            if (user is null)
            {
                return false;
            }
            Session.User = user;

            return true;
        }
    }
}