using System.Collections.Generic;

namespace Touragency.Model
{
    public class Group : IPermissionsGranted
    {
        public List<Permissions> AllPermissions
        {
            get => _permissionsManager.AllPermissions;
            private set
            {
                _permissionsManager.SetPermissions(value);
            }
        }
        public string Name { get; set; }
        private PermissionsManager _permissionsManager;

        public Group()
        {
            _permissionsManager = new PermissionsManager();
        }

        public void GrantPermissions(IEnumerable<Permissions> permissions) // Fixed typo in method name
        {
            _permissionsManager.GrantPermissions(permissions);
        }

        public void RevokePermissions(IEnumerable<Permissions> permissions)
        {
            _permissionsManager.RevokePermissions(permissions);
        }

        public void SetPermissions(IEnumerable<Permissions> permissions)
        {
            _permissionsManager.SetPermissions(permissions);
        }
        public void SetPermissions(params Permissions[] permissions)
        {
            SetPermissions((IEnumerable<Permissions>)permissions);
        }
    }
}
