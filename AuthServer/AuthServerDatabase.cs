﻿using System.Linq;
using System.Threading.Tasks;
using Common.Database;
using Common.Database.Tables;
using MongoDB.Driver;

namespace AuthServer
{
    public class AuthServerDatabase
    {
        public async Task<Users> GetAccount(string username)
        {
            var results = await DatabaseModel.UserCollection.Find(x => x.Username == username).Limit(1).ToListAsync();
            return results.FirstOrDefault();
        }
    }

    /*
    public class AuthServerDatabase : DatabaseModel<Models>
    {
        /// <summary>
        ///     Get user account based on login.
        /// </summary>
        /// <param name="username">string</param>
        /// <returns>Model.Users</returns>
        public Users GetAccount(string username)
        {
            return !Model.Users.Any() ? null : Model.Users.FirstOrDefault(a => a.username == username);
        }

        /// <summary>
        ///     Sets the authenticated user's sessionkey.
        /// </summary>
        /// <param name="username">string</param>
        /// <param name="key">byte</param>
        /// <returns>null</returns>
        public async void SetSessionKey(string username, byte[] key)
        {
            var account = GetAccount(username);
            using (var scope = new DataAccessScope())
            {
                var user = Model.Users.GetReference(account.Id);
                user.sessionkey = key;
                await scope.CompleteAsync();
            }
        }

        /// <summary>
        ///     Get Realms list.
        /// </summary>
        /// <returns>Model.Realms</returns>
        internal List<Realms> GetRealms()
        {
            return Model.Realms.Select(row => row).ToList();
        }

        /// <summary>
        ///     Get Characters by Realm.
        /// </summary>
        /// <param name="realmId">Set a realm ID (int)</param>
        /// <param name="accountName">Account Name (string)</param>
        /// <returns>int</returns>
        public int GetCharactersUsers(int realmId, string accountName)
        {
            return Model.Characters.Where(a => a.realm.Id == realmId && a.name == accountName).ToList().Count;
        }

        /// <summary>
        ///     Update realm flag status.
        /// </summary>
        /// <param name="realm">Realms model</param>
        /// <param name="flag">RealmFlag status</param>
        public void UpdateRealmStatus(Realms realm, RealmFlag flag)
        {
            using (var scope = new DataAccessScope())
            {
                var model = Model.Realms.GetReference(realm);
                model.flag = flag;

                scope.CompleteAsync();
            }
        }
    }
    */
}