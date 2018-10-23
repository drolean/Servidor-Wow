using System;
using System.IO;
using Common.Database.Tables;
using Common.Globals;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Common.Database
{
    public class DatabaseModel
    {
        public static MongoClient Client = new MongoClient();
        public static IMongoDatabase Database = Client.GetDatabase("wow-vanilla");

        public static IMongoCollection<Users> UserCollection = Database.GetCollection<Users>("Users");
        public static IMongoCollection<Realms> RealmCollection = Database.GetCollection<Realms>("Realms");
        public static IMongoCollection<Items> ItemsCollection = Database.GetCollection<Items>("Items");
        public static IMongoCollection<Creatures> CreaturesCollection = Database.GetCollection<Creatures>("Creatures");

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

            // Create initial Users
            var userJohn = new Users("John Doe", "john@doe.com", "john", "doe");
            var userJoao = new Users("Joao Doe", "joao@doe.com", "joao", "doe");
            var userBan = new Users("Ban Doe", "ban@doe.com", "ban", "doe");

            UserCollection.InsertMany(new[] {userJohn, userJoao, userBan});

            // Create initial Realm
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

            // Seeds Items
            //InsertItems();

            InsertCreatures();
        }

        public void InsertCreatures()
        {
            /**
             * - 13460 empty and FR item.
             * - Check name of items.
             * - Many items not converted to JSON.
             */
            foreach (string file in Directory.EnumerateFiles("../Seeds/creatures/", "*"))
            {
                string contents = File.ReadAllText(file);
                Creatures document = BsonSerializer.Deserialize<Creatures>(contents);
                CreaturesCollection.InsertOne(document);
            }
        }

        public void InsertItems()
        {
            /**
             * - 13460 empty and FR item.
             * - Check name of items.
             * - Many items not converted to JSON.
             */
            foreach (string file in Directory.EnumerateFiles("../Seeds/Items/", "*"))
            {
                string contents = File.ReadAllText(file);
                Items document = BsonSerializer.Deserialize<Items>(contents);
                ItemsCollection.InsertOne(document);
            }
        }
    }
}