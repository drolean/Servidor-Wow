using System;
using System.Collections.Generic;
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
        public static IMongoCollection<Items> ItemsCollection = Database.GetCollection<Items>("Items");

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

            // Create initial Items (NightElf Warrior Male)
            var itemWornShortsword = new Items
            {
                Entry = 25,
                Class = 2,
                SubClass = 7,
                Name = "Worn Shortsword",
                DisplayId = 1542,
                Quality = 1,
                Flags = 0,
                InventoryType = 21,
                ItemLevel = 2,
                Stackable = 1,
                Delay = 1900,
                Material = 1,
                Sheath = 3,
                MaxDurability = 20,
                SubDamages = new List<SubDamage>
                {
                    new SubDamage
                    {
                        Type = 0,
                        Min = 1,
                        Max = 3
                    }
                }
            };
            var itemToughJerky = new Items
            {
                Entry = 117,
                Class = 0,
                SubClass = 0,
                Name = "Tough Jerky",
                DisplayId = 2473,
                Quality = 1,
                Flags = 64,
                BuyCount = 5,
                InventoryType = 0,
                ItemLevel = 5,
                Stackable = 20,
                SubSpells = new List<SubItemSpell>
                {
                    new SubItemSpell
                    {
                        Id = 433,
                        Trigger = 0,
                        Charges = -1,
                        PpmRate = 0,
                        CoolDown = 0,
                        Category = 11,
                        CategoryCooldown = 1000
                    }
                },
                Material = 0,
                Sheath = 0,
                MaxDurability = 0,
                FoodType = 1
            };
            var itemWornWoodenShield = new Items
            {
                Entry = 2362,
                Class = 4,
                SubClass = 6,
                Name = "Worn Wooden Shield",
                DisplayId = 18730,
                Quality = 0,
                Flags = 0,
                InventoryType = 14,
                ItemLevel = 1,
                SubResistences = new SubResistence
                {
                    Armor = 5
                },
                Stackable = 1,
                Delay = 1900,
                Material = 1,
                Sheath = 4,
                Block = 1,
                MaxDurability = 20
            };
            var itemRecruitsShirt = new Items
            {
                Entry = 6120,
                Class = 4,
                SubClass = 0,
                Name = "Recruit's Shirt",
                DisplayId = 9983,
                Quality = 1,
                Flags = 0,
                InventoryType = 4,
                ItemLevel = 1,
                Stackable = 1,
                Material = 7,
                Sheath = 0
            };
            var itemRecruitsPants = new Items
            {
                Entry = 6121,
                Class = 4,
                SubClass = 1,
                Name = "Recruit's Pants",
                DisplayId = 9984,
                InventoryType = 7,
                ItemLevel = 1,
                Stackable = 1,
                Material = 7,
                Sheath = 0,
                MaxDurability = 25,
                SubResistences = new SubResistence
                {
                    Armor = 2
                }
            };
            var itemRecruitsBoots = new Items
            {
                Entry = 6122,
                Class = 4,
                SubClass = 0,
                Name = "Recruit's Boots",
                DisplayId = 9985,
                InventoryType = 8,
                ItemLevel = 1,
                Stackable = 1,
                Material = 7,
                Sheath = 0,
                MaxDurability = 0
            };
            var itemHearthstone = new Items
            {
                Entry = 6948,
                Class = 15,
                SubClass = 0,
                Name = "Hearthstone",
                DisplayId = 6418,
                Flags = 64,
                InventoryType = 0,
                ItemLevel = 1,
                MaxCount = 1,
                Stackable = 1,
                Material = 0,
                Sheath = 0,
                MaxDurability = 0,
                Bonding = 1,
                SubSpells = new List<SubItemSpell>
                {
                    new SubItemSpell
                    {
                        Id = 8690,
                        Trigger = 0,
                        Charges = 0,
                        PpmRate = 0,
                        CoolDown = -1,
                        Category = 0,
                        CategoryCooldown = -1
                    }
                }
            };

            ItemsCollection.InsertMany(new[]
            {
                itemWornShortsword, itemToughJerky, itemWornWoodenShield,
                itemRecruitsShirt, itemRecruitsPants, itemRecruitsBoots,
                itemHearthstone
            });
        }
    }
}