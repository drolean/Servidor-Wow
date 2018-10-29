using Common.Database;
using Common.Database.Tables;
using MongoDB.Driver;

namespace RealmServer
{
    public class RealmServerDatabase
    {
        /// <summary>
        ///     Get user account based on login.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Users GetAccount(string username)
        {
            return DatabaseModel.UserCollection.Find(x => x.Username == username).First();
        }
    }
}
