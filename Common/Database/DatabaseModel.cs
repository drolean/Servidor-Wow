using System;
using Common.Database.Tables;
using Common.Globals;
using MongoDB.Driver;

namespace Common.Database
{
    public class DatabaseModel
    {
        public static MongoClient Client = new MongoClient();
        public static IMongoDatabase Database = Client.GetDatabase("wow-vanilla");

        public static IMongoCollection<Users> UserCollection = Database.GetCollection<Users>("Users");
        public static IMongoCollection<Realms> RealmCollection = Database.GetCollection<Realms>("Realms");

        public static IMongoCollection<Characters> CharacterCollection =
            Database.GetCollection<Characters>("Characters");

        public DatabaseModel()
        {
            // Drop Database
            Client.DropDatabase("wow-vanilla");

            // Create Unique Index
            UserCollection.Indexes.CreateOneAsync(
                Builders<Users>.IndexKeys.Ascending(i => i.Email),
                new CreateIndexOptions<Users>
                {
                    Unique = true
                });

            // Create Unique Index
            UserCollection.Indexes.CreateOneAsync(
                Builders<Users>.IndexKeys.Ascending(i => i.Username),
                new CreateIndexOptions<Users>
                {
                    Unique = true
                });

            // Create Unique Index
            CharacterCollection.Indexes.CreateOneAsync(
                Builders<Characters>.IndexKeys.Ascending(i => i.Name),
                new CreateIndexOptions<Users>
                {
                    Unique = true
                });

            var userJohn = new Users("John Doe", "john@doe.com", "john", "doe");
            var userJoao = new Users("Joao Doe", "joao@doe.com", "joao", "doe");
            var userBan = new Users("Ban Doe", "ban@doe.com", "ban", "doe");

            var userTest = new Users
            {
                Name = "Test Doe",
                Username = "test",
                Password = "doe",
                Email = "test1@doe.com"
            };

            UserCollection.InsertOne(userTest);
            UserCollection.InsertMany(new[] {userJohn, userJoao, userBan});


            var realmTest = new Realms
            {
                Name = "Firetree",
                Address = "127.0.0.1:1001",
                Type = RealmType.Normal,
                Flag = RealmFlag.Recommended,
                Timezone = RealmTimezone.AnyLocale,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            RealmCollection.InsertOne(realmTest);
        }
    }
}