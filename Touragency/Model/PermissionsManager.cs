using System.Collections.Generic;

namespace Touragency.Model
{
    internal class PermissionsManager : IPermissionsGranted
    {
        public List<Permissions> AllPermissions { get; private set; } = new List<Permissions>();

        public void GrantPermissions(IEnumerable<Permissions> permissions) // Fixed typo in method name
        {
            foreach (Permissions permission in permissions)
            {
                if (AllPermissions.Contains(permission)) continue;
                AllPermissions.Add(permission);
            }
        }

        public void RevokePermissions(IEnumerable<Permissions> permissions)
        {
            foreach (Permissions permission in permissions)
            {
                if (AllPermissions.Contains(permission)) AllPermissions.Remove(permission);
            }
        }

        public void SetPermissions(IEnumerable<Permissions> permissions)
        {
            AllPermissions = new List<Permissions>(permissions);
        }
    }
}
