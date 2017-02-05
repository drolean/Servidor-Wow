using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Database.Tables;
using Shaolinq;

namespace AuthServer
{
    public class AuthServerDatabase : DatabaseModel<Models>
    {
        // Pega conta do usuario baseado no login
        public Users GetAccount(string username) => !Model.Users.Any() ? null : Model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());

        // Define a sessionkey do usuario autenticado
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

        // Pega lista de Realms
        internal List<Realms> GetRealms() => Model.Realms.Select(row => row).ToList();
    }
}
