using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Database.Tables;
using Shaolinq;

namespace AuthServer
{
    public class AuthServerDatabase : DatabaseModel<Models>
    {
        // Get user account based on login
        public Users GetAccount(string username) => !Model.Users.Any() ? null : Model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());

        // Sets the authenticated user's sessionkey
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

        // Get Realms list
        internal List<Realms> GetRealms() => Model.Realms.Select(row => row).ToList();
    }
}
