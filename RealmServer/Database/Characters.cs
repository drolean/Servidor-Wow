using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common.Database.Tables;
using Common.Globals;
using RealmServer.PacketReader;
using Shaolinq;

namespace RealmServer.Database
{
    public class Characters : RealmServerDatabase
    {
        /// <summary>
        ///     Retrieve character by char name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal static Common.Database.Tables.Characters FindCharacaterByName(string username)
        {
            return !Model.Characters.Any()
                ? null
                : Model.Characters.FirstOrDefault(a => a.name.ToLower() == username.ToLower());
        }

        /// <summary>
        ///     Retrieve characters by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal static List<Common.Database.Tables.Characters> GetCharacters(string username)
        {
            var account = MainProgram.RealmServerDatabase.GetAccount(username);
            return Model.Characters.Where(a => a.user == account).ToList();
        }

        internal static void Create(CMSG_CHAR_CREATE handler, Users users)
        {
            using (var scope = new DataAccessScope())
            {

                // DONE: Save Char 
                var Char = Model.Characters.Create();
                Char.user = users;
                // DONE: Make name capitalized as on official
                Char.name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(handler.Name);
                Char.race = (Races)handler.Race;
                Char.classe = (Classes)handler.Classe;
                Char.gender = (Genders)handler.Gender;
                Char.level = 1;
                Char.money = 0;
                ///
                Char.MapId = 0;
                Char.MapZone = 12;
                Char.MapX = -8949.95f;
                Char.MapY = -132.493f;
                Char.MapZ = 83.5312f;
                Char.MapO = 1.0f;
                Char.char_skin = handler.Skin;
                Char.char_face = handler.Face;
                Char.char_hairStyle = handler.HairStyle;
                Char.char_hairColor = handler.HairColor;
                Char.char_facialHair = handler.FacialHair;
                Char.created_at = DateTime.Now;
                Char.watched_faction = 255;

                // Set Another status 

                // Set Taxi Zones

                // tutorial Flags

                // Map Explored

                // Set Honor

                // Query Access Level and Account ID
                // Char.level 

                scope.Complete();
            }
        }
    }
}