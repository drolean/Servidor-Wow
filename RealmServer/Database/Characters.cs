using System;
using System.Collections.Generic;
using Common.Database;
using Common.Database.Tables;
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

        internal static void Create(CMSG_CHAR_CREATE handler, Users user)
        {
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
                SubMap = new SubMap
                {
                    MapId = 0,
                    MapZone = 12,
                    MapX = -8949.95f,
                    MapY = -132.493f,
                    MapZ = 83.5312f,
                    MapO = 1.0f
                },
                SubSkin = new SubSkin
                {
                    Skin = handler.Skin,
                    Face = handler.Face,
                    HairStyle = handler.HairStyle,
                    HairColor = handler.HairColor,
                    FacialHair = handler.FacialHair
                },
                //

                //
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            DatabaseModel.CharacterCollection.InsertOne(character);
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

        public static Common.Database.Tables.Characters GetCharacter(CMSG_PLAYER_LOGIN handler)
        {
            return DatabaseModel.CharacterCollection.Find(x => x.Uid == handler.Id && x.DeletedAt == null).First();
        }
    }
}