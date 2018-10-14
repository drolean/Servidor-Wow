using System.Collections.Generic;
using Common.Database;
using Common.Database.Tables;
using Common.Globals;
using MongoDB.Driver;

namespace AuthServer
{
    public class AuthServerDatabase
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

        /// <summary>
        ///     Sets the authenticated user's sessionkey.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public UpdateResult SetSessionKey(string username, byte[] key)
        {
            return DatabaseModel.UserCollection.UpdateOne(
                Builders<Users>.Filter.Eq(x => x.Username, username),
                Builders<Users>.Update.Set(x => x.SessionKey, key));
        }

        /// <summary>
        ///     Get Realm list.
        /// </summary>
        /// <returns></returns>
        public List<Realms> GetRealms()
        {
            return DatabaseModel.RealmCollection.Find(_ => true).ToList();
        }

        /// <summary>
        ///     Update realm status flag.
        /// </summary>
        /// <param name="realm"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public UpdateResult UpdateRealmStatus(Realms realm, RealmFlag flag)
        {
            return DatabaseModel.RealmCollection.UpdateOne(
                Builders<Realms>.Filter.Eq(x => x.Name, realm.Name),
                Builders<Realms>.Update.Set(x => x.Flag, flag));
        }

        /// <summary>
        ///     Get Characters by Realm.
        /// </summary>
        /// <param name="realm"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public long GetCharactersByUser(Realms realm, string username)
        {
            return DatabaseModel.CharacterCollection.Find(a => a.Realm.Id == realm.Id && a.Name == username).Count();
        }
    }
}