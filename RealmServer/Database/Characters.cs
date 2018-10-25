using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Database;
using Common.Database.Tables;
using Common.Database.Xml;
using Common.Globals;
using Common.Helpers;
using MongoDB.Driver;
using RealmServer.Enums;
using RealmServer.PacketReader;

namespace RealmServer.Database
{
    public class Characters : RealmServerDatabase
    {
        internal static Task<ReplaceOneResult> UpdateCharacter(Common.Database.Tables.Characters character)
        {
            return DatabaseModel.CharacterCollection.ReplaceOneAsync(doc => doc.Uid == character.Uid, character);
        }

        /// <summary>
        ///     Find character by UID.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        internal static Common.Database.Tables.Characters FindCharacaterByUid(ulong uid)
        {
            return DatabaseModel.CharacterCollection.Find(x => x.Uid == uid).First();
        }

        /// <summary>
        ///     Retrieve character by name.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static Common.Database.Tables.Characters FindCharacaterByName(string character)
        {
            return DatabaseModel.CharacterCollection.Find(x => x.Name.ToLower() == character.ToLower())
                .First();
        }

        /// <summary>
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="user"></param>
        internal static void Create(CMSG_CHAR_CREATE handler, Users user)
        {
            var initRace = XmlReader.GetRace((Races) handler.Race);
            var initClas = XmlReader.GetRaceClass((Races) handler.Race, (Classes) handler.Classe);

            var character = new Common.Database.Tables.Characters
            {
                Uid = Utils.GenerateRandUlong(),
                User = user.Id,
                //
                Name = handler.Name,
                Race = (Races) handler.Race,
                Classe = (Classes) handler.Classe,
                Gender = (Genders) handler.Gender,
                Level = 1,
                Money = 10,
                Xp = 0,
                //
                WatchFaction = -1, // 255??
                Cinematic = false,
                Flag = CharacterFlag.None,
                TutorialFlags = new byte[32],
                //
                SubMap = new SubMap
                {
                    MapId = initRace.init.MapId,
                    MapZone = initRace.init.ZoneId,
                    MapX = initRace.init.MapX,
                    MapY = initRace.init.MapY,
                    MapZ = initRace.init.MapZ,
                    MapO = initRace.init.MapR
                },
                SubSkin = new SubSkin
                {
                    Skin = handler.Skin,
                    Face = handler.Face,
                    HairStyle = handler.HairStyle,
                    HairColor = handler.HairColor,
                    FacialHair = handler.FacialHair
                },
                SubStats = new SubStats
                {
                    Agility = (uint) (initRace.stats.agi + initClas.stats.agi),
                    Intellect = (uint) (initRace.stats.@int + initClas.stats.@int),
                    Spirit = (uint) (initRace.stats.spi + initClas.stats.spi),
                    Stamina = (uint) (initRace.stats.sta + initClas.stats.sta),
                    Strength = (uint) (initRace.stats.str + initClas.stats.str),

                    Energy = initClas.power.energy,
                    Mana = initClas.power.mana,
                    ManaType = 0, // TODO
                    Life = (uint) (initRace.health + initClas.health),
                    Rage = initClas.power.rage
                },
                SubSpells = new List<SubSpell>(),
                SubActionBars = new List<SubActionBar>(),
                SubFactions = new List<SubFaction>(),
                SubSkills = new List<SubSkill>(),
                SubInventorie = new List<SubInventory>(),
                SubFriends = new List<SubCharacterFriend>(),
                SubIgnoreds = new List<SubCharacterIgnored>(),

                //
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            DatabaseModel.CharacterCollection.InsertOne(character);

            CreateCharacterSpells(character, initRace, initClas);
            CreateCharacterActions(character, initClas);
            CreateCharacterFactions(character);
            CreateCharacterSkills(character, initRace, initClas);
            CreateCharacterInventorie(character);
        }

        /// <summary>
        /// </summary>
        /// <param name="character"></param>
        private static void CreateCharacterInventorie(Common.Database.Tables.Characters character)
        {
            var rnd = new Random();

            try
            {
                var startItems =
                    MainProgram.CharacterOutfitReader.Get(character.Classe, character.Race, character.Gender);

                for (var i = 0; i < 12; i++)
                {
                    if (startItems.Items[i] <= 0)
                        continue;

                    var item = DatabaseModel.ItemsCollection.Find(x => x.Entry == startItems.Items[i]).First();

                    if (item == null)
                        continue;

                    DatabaseModel.CharacterCollection.UpdateOneAsync(
                        Builders<Common.Database.Tables.Characters>.Filter.Where(x => x.Uid == character.Uid),
                        Builders<Common.Database.Tables.Characters>.Update.Push("SubInventorie", new SubInventory
                        {
                            Item = item.Entry,
                            Slot = PrefInvSlot(item.InventoryType) == 23
                                ? rnd.Next(23, 27)
                                : PrefInvSlot(item.InventoryType),
                            Durability = item.MaxDurability,
                            StackCount = item.Stackable == 20 ? 5 : 1,
                            CreatedAt = DateTime.Now
                        })
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // TODO: move to Helper
        internal static int PrefInvSlot(int item)
        {
            int[] slotTypes =
            {
                (int) InventorySlots.SLOT_INBACKPACK, // NONE EQUIP
                (int) InventorySlots.SLOT_HEAD,
                (int) InventorySlots.SLOT_NECK,
                (int) InventorySlots.SLOT_SHOULDERS,
                (int) InventorySlots.SLOT_SHIRT,
                (int) InventorySlots.SLOT_CHEST,
                (int) InventorySlots.SLOT_WAIST,
                (int) InventorySlots.SLOT_LEGS,
                (int) InventorySlots.SLOT_FEET,
                (int) InventorySlots.SLOT_WRISTS,
                (int) InventorySlots.SLOT_HANDS,
                (int) InventorySlots.SLOT_FINGERL,
                (int) InventorySlots.SLOT_TRINKETL,
                (int) InventorySlots.SLOT_MAINHAND, // 1h
                (int) InventorySlots.SLOT_OFFHAND, // shield
                (int) InventorySlots.SLOT_RANGED,
                (int) InventorySlots.SLOT_BACK,
                (int) InventorySlots.SLOT_MAINHAND, // 2h
                (int) InventorySlots.SLOT_BAG1,
                (int) InventorySlots.SLOT_TABARD,
                (int) InventorySlots.SLOT_CHEST, // robe
                (int) InventorySlots.SLOT_MAINHAND, // mainhand
                (int) InventorySlots.SLOT_OFFHAND, // offhand
                (int) InventorySlots.SLOT_MAINHAND, // held
                (int) InventorySlots.SLOT_INBACKPACK, // ammo
                (int) InventorySlots.SLOT_RANGED, // thrown
                (int) InventorySlots.SLOT_RANGED // rangedright
            };

            return slotTypes[item];
        }

        /// <summary>
        /// </summary>
        /// <param name="character"></param>
        /// <param name="initRace"></param>
        /// <param name="initClas"></param>
        private static void CreateCharacterSkills(Common.Database.Tables.Characters character, racesRace initRace,
            racesRaceClass initClas)
        {
            // SKILLS
            foreach (var skillBase in initRace.skills)
                DatabaseModel.CharacterCollection.UpdateOneAsync(
                    Builders<Common.Database.Tables.Characters>.Filter.Where(x => x.Uid == character.Uid),
                    Builders<Common.Database.Tables.Characters>.Update.Push("SubSkills", new SubSkill
                    {
                        Skill = skillBase.id,
                        Value = skillBase.min,
                        Max = skillBase.max,
                        CreatedAt = DateTime.Now
                    })
                );

            // SKILLS
            foreach (var skillBase in initClas.skills)
                DatabaseModel.CharacterCollection.UpdateOneAsync(
                    Builders<Common.Database.Tables.Characters>.Filter.Where(x => x.Uid == character.Uid),
                    Builders<Common.Database.Tables.Characters>.Update.Push("SubSkills", new SubSkill
                    {
                        Skill = skillBase.id,
                        Value = skillBase.min,
                        Max = skillBase.max,
                        CreatedAt = DateTime.Now
                    })
                );
        }

        /// <summary>
        /// </summary>
        /// <param name="character"></param>
        /// <param name="initRace"></param>
        /// <param name="initClas"></param>
        private static void CreateCharacterSpells(Common.Database.Tables.Characters character, racesRace initRace,
            racesRaceClass initClas)
        {
            // SPELLS
            foreach (var spellBase in initRace.spells)
                DatabaseModel.CharacterCollection.UpdateOneAsync(
                    Builders<Common.Database.Tables.Characters>.Filter.Where(x => x.Uid == character.Uid),
                    Builders<Common.Database.Tables.Characters>.Update.Push("SubSpells", new SubSpell
                    {
                        Spell = spellBase.id,
                        Active = 0,
                        Cooldown = 0,
                        CreatedAt = DateTime.Now
                    })
                );

            // SPELLS
            foreach (var spellBase in initClas.spells)
                DatabaseModel.CharacterCollection.UpdateOneAsync(
                    Builders<Common.Database.Tables.Characters>.Filter.Where(x => x.Uid == character.Uid),
                    Builders<Common.Database.Tables.Characters>.Update.Push("SubSpells", new SubSpell
                    {
                        Spell = spellBase.id,
                        Active = 0,
                        Cooldown = 0,
                        CreatedAt = DateTime.Now
                    })
                );
        }

        /// <summary>
        /// </summary>
        /// <param name="character"></param>
        /// <param name="initClas"></param>
        private static void CreateCharacterActions(Common.Database.Tables.Characters character, racesRaceClass initClas)
        {
            // ACTIONBAR
            foreach (var actionBase in initClas.actions)
                DatabaseModel.CharacterCollection.UpdateOneAsync(
                    Builders<Common.Database.Tables.Characters>.Filter.Where(x => x.Uid == character.Uid),
                    Builders<Common.Database.Tables.Characters>.Update.Push("SubActionBars", new SubActionBar
                    {
                        Button = actionBase.button,
                        Action = actionBase.spell,
                        Type = actionBase.type,
                        CreatedAt = DateTime.Now
                    })
                );
        }

        /// <summary>
        /// </summary>
        /// <param name="character"></param>
        private static void CreateCharacterFactions(Common.Database.Tables.Characters character)
        {
            // FACTIONS
            var initiFactions = MainProgram.FactionReader.GenerateFactions(character.Race);

            foreach (var valFaction in initiFactions)
            {
                var faction = valFaction.Split(',');

                DatabaseModel.CharacterCollection.UpdateOneAsync(
                    Builders<Common.Database.Tables.Characters>.Filter.Where(x => x.Uid == character.Uid),
                    Builders<Common.Database.Tables.Characters>.Update.Push("SubFactions", new SubFaction
                    {
                        Faction = int.Parse(faction[0]),
                        Flags = int.Parse(faction[1]),
                        Standing = int.Parse(faction[2]),
                        CreatedAt = DateTime.Now
                    })
                );
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static Task UpdateMovement(Common.Database.Tables.Characters character)
        {
            return DatabaseModel.CharacterCollection.UpdateOneAsync(
                Builders<Common.Database.Tables.Characters>.Filter.Where(x => x.Uid == character.Uid),
                Builders<Common.Database.Tables.Characters>.Update.Set(x => x.SubMap, character.SubMap)
            );
        }

        /// <summary>
        ///     Retrieve characters by user.ID
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<Common.Database.Tables.Characters> GetCharacters(Users user)
        {
            return DatabaseModel.CharacterCollection.Find(x => x.User == user.Id && x.DeletedAt == null).ToList();
        }

        /// <summary>
        ///     Set flag deletedAt to character.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static UpdateResult DeleteCharacter(CMSG_CHAR_DELETE handler)
        {
            return DatabaseModel.CharacterCollection.UpdateOne(
                Builders<Common.Database.Tables.Characters>.Filter.Where(x => x.Uid == handler.Id),
                Builders<Common.Database.Tables.Characters>.Update.Set(x => x.DeletedAt, DateTime.Now));
        }

        /// <summary>
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static Common.Database.Tables.Characters GetCharacter(CMSG_PLAYER_LOGIN handler)
        {
            return DatabaseModel.CharacterCollection.Find(x => x.Uid == handler.PlayerUid && x.DeletedAt == null)
                .First();
        }
    }
}