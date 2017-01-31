using Common.Database;
using Common.Database.Tables;
using System.Linq;
using Shaolinq;
using System.Collections.Generic;

namespace AuthServer
{
    public class AuthServerDatabase : DatabaseModel<Models>
    {
        // Pega conta do usuario baseado no login
        public Users GetAccount(string username) => !model.Users.Any() ? null : model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());

        // Define a sessionkey do usuario autenticado
        public async void SetSessionKey(string username, byte[] key)
        {
            Users account = GetAccount(username);
            using (var scope = new DataAccessScope())
            {
                var user = model.Users.GetReference(account.Id);
                user.sessionkey = key;
                await scope.CompleteAsync();
            }
        }

        // Pega lista de Realms
        internal List<Realms> GetRealms() => model.Realms.Select(row => row).ToList();
    }
}
