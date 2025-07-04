using System.Collections.Generic;

namespace Touragency.Model
{
    public class UserGenerator
    {
        public static List<User> GenerateSampleUsers()
        {
            User administrator = new User(username: "admin", password: "admin");
            administrator.AddGroup(DefaultGroups.Administrator);
            User administratorA = new User(username: "a", password: "a");
            administratorA.AddGroup(DefaultGroups.Administrator);
            User managerIlya = new User(username: "Ilya", password: "123");
            managerIlya.AddGroup(DefaultGroups.Manager);
            User managerVasya = new User(username: "Vasya", password: "qwerty");
            managerIlya.AddGroup(DefaultGroups.Manager);

            return new List<User>
            {
                administrator,
                administratorA,
                managerIlya,
                managerVasya
            };
        }
    }
}