using System.Collections.Generic;

namespace Touragency.Model
{
    public interface IPermissionsGranted
    {
        public List<Permissions> AllPermissions { get; }
        public void GrantPermissions(IEnumerable<Permissions> permissions); // Fixed typo in method name
        public void RevokePermissions(IEnumerable<Permissions> permissions);
        public void SetPermissions(IEnumerable<Permissions> permissions);
    }
}
