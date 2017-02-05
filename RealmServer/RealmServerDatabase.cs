using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common.Database;
using Common.Database.Dbc;
using Common.Database.Tables;
using Common.Globals;
using RealmServer.Handlers;
using Shaolinq;

namespace RealmServer
{
    public class RealmServerDatabase : DatabaseModel<Models>
    {
        // Pega conta do usuario baseado no login
        public Users GetAccount(string username) => !Model.Users.Any() ? null : Model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());

        // Retorna lista de chars do usuario
        public List<Characters> GetCharacters(string username)
        {
            Users account = GetAccount(username);
            return Model.Characters.Where(a => a.user == account).ToList();
        }

        // Pega Char pelo nome
        public Characters GetCharacaterByName(string username) => !Model.Characters.Any() ? null : Model.Characters.FirstOrDefault(a => a.name.ToLower() == username.ToLower());

        internal void CreateChar(CmsgCharCreate handler, Users users)
        {
            // Factions 
            var InitiFactions = MainForm.FactionReader.GenerateFactions((Races)handler.Race);

            // Character Variables
            var InitCharacter = MainForm.CharacterOutfitReader.Get((Classes)handler.Classe, (Races)handler.Race, (Genders)handler.Gender);

            var InitRace = XmlReader.GetRace((Races)handler.Race);

            var InitRaceClass = XmlReader.GetRaceClass((Races)handler.Race, (Classes)handler.Classe);

            // Set Character Create Information

            // Set Player Create Action Buttons

            // Set Player Create Skills

            // Set Player Taxi Zones 0-31

            // Set Tutorial Flags

            // Set Player Create Spells

            // Set Player Create Items
            CharStartOutfit startItems = MainForm.CharacterOutfitReader.Get((Classes)handler.Classe, (Races)handler.Race, (Genders)handler.Gender);
            /*
            for (int j = 0; j < 12; ++j)
            {
                if (startItems.Items[j] <= 0)
                    continue;

                Console.WriteLine(XmlReader.GetItem(startItems.Items[j]));
            }
            */
            // First add bags
            // Then add the rest of the items

            /*
            // Selecting Starter Itens Equipament
            CharStartOutfit startItems = CharStartOutfit.Values.FirstOrDefault(x => x.Match(handler.Race, handler.Class, handler.Gender));

            // Selecting char data creation
            CharacterCreationInfo charStarter = GetCharStarter((RaceID)handler.Race);
            */
            using (var scope = new DataAccessScope())
            {
                // Salva Char
                var Char = Model.Characters.Create();
                    Char.user       = users;
                    Char.name       = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(handler.Name);
                    Char.race       = (Races)handler.Race;
                    Char.classe     = (Classes)handler.Classe;
                    Char.gender     = (Genders)handler.Gender;
                    Char.level      = 1;
                    Char.money      = 0;
                    Char.MapId      = InitRace.init.MapId;
                    Char.MapZone    = InitRace.init.ZoneId;
                    Char.MapX       = InitRace.init.MapX;
                    Char.MapY       = InitRace.init.MapY;
                    Char.MapZ       = InitRace.init.MapZ;
                    Char.MapO       = InitRace.init.MapR;
                    Char.char_skin       = handler.Skin;
                    Char.char_face       = handler.Face;
                    Char.char_hairStyle  = handler.HairStyle;
                    Char.char_hairColor  = handler.HairColor;
                    Char.char_facialHair = handler.FacialHair;
                    Char.created_at = DateTime.Now;

                scope.Complete();
            }
        }
    }
}
