using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Database.Tables;
using Shaolinq;

namespace AuthServer
{
    public class AuthServerDatabase : DatabaseModel<Models>
    {
        /// <summary>
        /// Get user account based on login.
        /// </summary>
        /// <param name="username">string</param>
        /// <returns>Model.Users</returns>
        public Users GetAccount(string username) => !Model.Users.Any() ? null : Model.Users.FirstOrDefault(a => a.username == username);

        /// <summary>
        /// Sets the authenticated user's sessionkey.
        /// </summary>
        /// <param name="username">string</param>
        /// <param name="key">byte</param>
        /// <returns>null</returns>
        public async void SetSessionKey(string username, byte[] key)
        {
            Users account = GetAccount(username);
            using (var scope = new DataAccessScope())
            {
                var user = Model.Users.GetReference(account.Id);
                user.sessionkey = key;
                await scope.CompleteAsync();
            }
        }

        /// <summary>
        /// Get Realms list
        /// </summary>
        /// <returns>Model.Realms</returns>
        internal List<Realms> GetRealms() => Model.Realms.Select(row => row).ToList();

        /// <summary>
        /// Get Characters by Realm
        /// </summary>
        /// <param name="realmId">Set a realm ID (int)</param>
        /// <param name="accountName">Account Name (string)</param>
        /// <returns>int</returns>
        public int GetCharactersUsers(int realmId, string accountName)
        {
            return Model.Characters.Where(a => a.realm.Id == realmId && a.name == accountName).ToList().Count;
        }
    }
}
