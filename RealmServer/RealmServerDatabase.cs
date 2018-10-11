using System.Linq;
using Common.Database;
using Common.Database.Tables;

namespace RealmServer
{
    public class RealmServerDatabase : DatabaseModel<Common.Database.Models>
    {
        /// <summary>
        ///     Get account based on login
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal Users GetAccount(string username)
        {
            return !Model.Users.Any()
                ? null
                : Model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());
        }
    }
}