using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Touragency.Model
{
    public class User : IPermissionsGranted
    {
        public List<Permissions> AllPermissions { 
            get => _permissionsManager.AllPermissions;
            private set
            {
                _permissionsManager.SetPermissions(value);
            } 
        }
        public List<Group> Groups
        {
            get; private set;
        } = new List<Group>();
        public string Username { get; private set; }
        public string Password { get; private set; }

        private PermissionsManager _permissionsManager;

        public User(string username, string password)
        {
            Username = username;
            Password = password;
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
        public void AddGroup(Group group)
        {
            if (Groups.Contains(group)) return;
            Groups.Add(group);
        }
        public void AddGroup(DefaultGroups group)
        {
            var _group = new Group();
            switch (group)
            {
                case DefaultGroups.Administrator:
                    _group.Name = "Администратор";
                    _group.SetPermissions(Permissions.CreateTour, Permissions.UpdateTour, Permissions.DeleteTour,
                        Permissions.CreateHotel, Permissions.UpdateHotel, Permissions.DeleteHotel);
                    AddGroup(_group);
                    break;
                case DefaultGroups.Manager:
                    _group.Name = "Менеджер";
                    AddGroup(_group);
                    break;
                default:
                    throw new NotImplementedException("Для данной группы не установлен сценарий разграничения доступа");
            }
        }

        public void DeleteGroup(Group group)
        {
            if (Groups.Contains(group)) Groups.Remove(group);
        }
    }
}
