using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Database.Tables;

namespace RealmServer
{
    public class RealmServerDatabase : DatabaseModel<Models>
    {
        // Pega conta do usuario baseado no login
        public Users GetAccount(string username) => !model.Users.Any() ? null : model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());

        // Retorna lista de chars do usuario
        public List<Characters> GetCharacters(string username)
        {
            Users account = GetAccount(username);
            return model.Characters.Where(a => a.user == account).ToList();
        }
    }
}
