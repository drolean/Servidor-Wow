using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Database;
using Common.Database.Tables;
using Common.Database.Xml;
using Common.Globals;
using Common.Helpers;
using MongoDB.Driver;
using RealmServer.PacketReader;

namespace RealmServer.Database
{
    public class Characters : RealmServerDatabase
    {
        /// <summary>
        ///     Retrieve character by char name
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public Common.Database.Tables.Characters FindCharacaterByName(string character)
        {
            return DatabaseModel.CharacterCollection.Find(x => x.Name == character).First();
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
                WatchFaction = 255,
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

        private static void CreateCharacterInventorie(Common.Database.Tables.Characters character)
        {
            uint countBag = 0;
            var startItems = MainProgram.CharacterOutfitReader.Get(character.Classe, character.Race, character.Gender);


            var result = "";
            for (var i = 0; i < 12; i++)
            {
                result += startItems.Items[i];
                if (i != 11) result += ",";
            }

            Console.WriteLine(result);
            /*
            foreach (var i in startItems.Items)
            {
                if (i <= 0)
                {
                    continue;
                }

                var item = XmlReader.GetItem(i);


                if (PrefInvSlot(item) == 23)
                    countBag++;
            }
            */
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
                        Action = actionBase.action,
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
            return DatabaseModel.CharacterCollection.FindSync(x => x.User == user.Id && x.DeletedAt == null).ToList();
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
            return DatabaseModel.CharacterCollection.Find(x => x.Uid == handler.Id && x.DeletedAt == null).First();
        }
    }
}