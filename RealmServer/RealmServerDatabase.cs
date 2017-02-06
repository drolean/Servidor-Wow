using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common.Database;
using Common.Database.Tables;
using Common.Globals;
using RealmServer.Handlers;
using Shaolinq;
using RealmServer.Helpers;

namespace RealmServer
{
    public class RealmServerDatabase : DatabaseModel<Models>
    {
        public static CharacterInitializator Helper { get; set; }

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
            Helper = new CharacterInitializator();
           
            // Set Player Taxi Zones 0-31

            // Set Tutorial Flags

            using (var scope = new DataAccessScope())
            {
                var initRace = XmlReader.GetRace((Races)handler.Race);

                // Salva Char
                var Char = Model.Characters.Create();
                    Char.user       = users;
                    Char.name       = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(handler.Name);
                    Char.race       = (Races)handler.Race;
                    Char.classe     = (Classes)handler.Classe;
                    Char.gender     = (Genders)handler.Gender;
                    Char.level      = 1;
                    Char.money      = 0;
                    Char.MapId      = initRace.init.MapId;
                    Char.MapZone    = initRace.init.ZoneId;
                    Char.MapX       = initRace.init.MapX;
                    Char.MapY       = initRace.init.MapY;
                    Char.MapZ       = initRace.init.MapZ;
                    Char.MapO       = initRace.init.MapR;
                    Char.char_skin       = handler.Skin;
                    Char.char_face       = handler.Face;
                    Char.char_hairStyle  = handler.HairStyle;
                    Char.char_hairColor  = handler.HairColor;
                    Char.char_facialHair = handler.FacialHair;
                    Char.created_at = DateTime.Now;
                
                scope.Complete();
            }

            var Character = MainForm.Database.GetCharacaterByName(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(handler.Name));

            Helper.GenerateActionBar(Character);      // DONE
            Helper.GenerateFactions(Character);       // DONE
            Helper.GenerateInventory(Character);      // DONE
            Helper.GenerateSkills(Character);         // DONE
            Helper.GenerateSpells(Character);         // DONE
        }

        internal List<CharactersInventorys> GetInventory(Characters character)
        {
            return Model.CharactersInventorys.Where(a => a.character == character).ToList();
        }
    }
}
