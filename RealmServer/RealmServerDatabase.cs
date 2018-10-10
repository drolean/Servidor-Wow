using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Database.Tables;

namespace RealmServer
{
    public class RealmServerDatabase : DatabaseModel<Models>
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

        /// <summary>
        ///     Retrieve characters by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal List<Characters> GetCharacters(string username)
        {
            var account = GetAccount(username);
            return Model.Characters.Where(a => a.user == account).ToList();
        }

        /// <summary>
        ///     Retrieve character by char name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal Characters FindCharacaterByName(string username)
        {
            return !Model.Characters.Any()
                ? null
                : Model.Characters.FirstOrDefault(a => a.name.ToLower() == username.ToLower());
        }
    }
}