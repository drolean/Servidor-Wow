using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Database.Tables;
using RealmServer.Handlers;

namespace RealmServer
{
    public class RealmServerDatabase : DatabaseModel<Models>
    {
        // Pega conta do usuario baseado no login
        public Users GetAccount(string username) => !model.Users.Any() ? null : model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());

        // Retorna lista de chars do usuario
        public List<Characters> GetCharacters(string username)
        {
            Users account = GetAccount(username);
            return model.Characters.Where(a => a.user == account).ToList();
        }

        // Pega Char pelo nome
        public Characters GetCharacaterByName(string username) => !model.Characters.Any() ? null : model.Characters.FirstOrDefault(a => a.name.ToLower() == username.ToLower());

        internal void CreateChar(CmsgCharCreate handler, Users users)
        {
            for (int i = 0; i <= 63; i++)
            {
                //var faction = MainForm.FactionReader.Get(i);

                /*

            if factionFlag == i
                For(0 to 3)

                endFor
            endif

public bool HaveFlag(uint value, byte flagPos)
{
	value = value >> Convert.ToUInt32(flagPos);
	value = value % 2;

	if (value == 1) {
		return true;
	} else {
		return false;
	}
}
                */
            }

            /*
            // Selecting Starter Itens Equipament
            CharStartOutfit startItems = CharStartOutfit.Values.FirstOrDefault(x => x.Match(handler.Race, handler.Class, handler.Gender));

            // Selecting char data creation
            CharacterCreationInfo charStarter = GetCharStarter((RaceID)handler.Race);

            using (var scope = new DataAccessScope())
            {
                // Salva Char
                var Char = model.Characters.Create();
                    Char.user       = users;
                    Char.name       = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(handler.Name);
                    Char.race       = (Races)handler.Race;
                    Char.classe     = (Classes)handler.Classe;
                    Char.gender     = (Genders)handler.Gender;
                    Char.level      = 1;
                    Char.money      = 0;
                    Char.MapId      = charStarter.MapID;
                    Char.MapZone    = charStarter.MapZone;
                    Char.MapX       = charStarter.MapX;
                    Char.MapY       = charStarter.MapY;
                    Char.MapZ       = charStarter.MapZ;
                    Char.MapO       = charStarter.MapRotation;
                    Char.char_skin
                    Char.char_face
                    Char.char_hairStyle
                    Char.char_hairColor
                    Char.char_facialHair
                    Char.created_at = DateTime.Now;

                scope.Complete();
            }
            */
        }
    }
}
