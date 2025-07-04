using System;
using System.Collections.Generic;
using System.Text;
using Touragency.Views.Windows;

namespace Touragency.Model
{
    public static class Session
    {
        public static MainscreenWnd MainscreenWnd {  get; set; }
        private static User? _authorithatedUser;
        public static User User { 
            get 
            {
                if (_authorithatedUser == null) 
                {
                    throw new Exception("User is not authorithated");
                }
                return _authorithatedUser;
            } 
            set 
            {
                _authorithatedUser = value;  
            }
        }

        public static bool CheckTourUpdatePermission(Permissions permission)
        {
            try
            {
                if (User is null)
                {
                    return false;
                }
                if (User.AllPermissions.Contains(permission))
                {
                    return true;
                }
                foreach (var group in User.Groups)
                {
                    if (group.AllPermissions.Contains(permission))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
