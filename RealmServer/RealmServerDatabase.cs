using Common.Database;
using Common.Database.Tables;
using System.Linq;

namespace RealmServer
{
    public class RealmServerDatabase : DatabaseModel<Models>
    {
        // Pega conta do usuario baseado no login
        public Users GetAccount(string username) => !model.Users.Any() ? null : model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());
    }
}
