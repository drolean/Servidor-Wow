using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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
        internal static CharacterHelper Helper { get; set; }

        // Pega conta do usuario baseado no login
        internal Users GetAccount(string username) => !Model.Users.Any() ? null : Model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());

        // Retorna lista de chars do usuario
        internal List<Characters> GetCharacters(string username)
        {
            var account = GetAccount(username);
            return Model.Characters.Where(a => a.user == account).ToList();
        }

        // Pega Char pelo nome
        internal Characters GetCharacaterByName(string username) => !Model.Characters.Any() ? null : Model.Characters.FirstOrDefault(a => a.name.ToLower() == username.ToLower());

        internal void CreateChar(CmsgCharCreate handler, Users users)
        {
            Helper = new CharacterHelper();

            using (var scope = new DataAccessScope())
            {
                var initRace = XmlReader.GetRace((Races)handler.Race);

                // Check Name in Use

                // Can't create character named as the bot

                // Check for disabled class/race, only for non GM/Admin

                // Check for both horde and alliance (Only if it's a pvp realm)

                // Check for max characters in total on all realms

                // DONE: Save Char 
                var Char = Model.Characters.Create();
                    Char.user       = users;
                    // DONE: Make name capitalized as on official
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

                    // Set Another status 

                    // Set Taxi Zones

                    // tutorial Flags

                    // Map Explored

                    // Set Honor

                    // Query Access Level and Account ID
                    // Char.level 

                scope.Complete();
            }

            var character = MainForm.Database.GetCharacaterByName(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(handler.Name));

            Helper.GenerateActionBar(character);      // DONE: Generate Action Bar
            Helper.GenerateFactions(character);       // DONE: Generate Reputation Factions
            Helper.GenerateSkills(character);         // DONE: Generate Skills
            Helper.GenerateSpells(character);         // DONE: Generate Spells
            Helper.GenerateInventory(character);      // Generate Inventory

        }

        internal async Task UpdateMovement(Characters character)
        {
            using (var scope = new DataAccessScope())
            {
                var update = Model.Characters.GetReference(character.Id);
                update.MapX = character.MapX;
                update.MapY = character.MapY;
                update.MapZ = character.MapZ;
                update.MapO = character.MapO;

                await scope.CompleteAsync();
            }
        }

        internal List<CharactersInventorys> GetInventory(Characters character)
        {
            return Model.CharactersInventorys.Where(a => a.character == character).ToList();
        }

        internal async void UpdateName(CmsgCharRename handler)
        {
            using (var scope = new DataAccessScope())
            {
                var character = Model.Characters.GetByPrimaryKey(handler.Id);
                    character.name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(handler.Name);

                await scope.CompleteAsync();
            }
        }

        internal async void DeleteCharacter(int id)
        {
            // Check if char is own of player
                // if not BAN account and IP

            var Char = Model.Characters.FirstOrDefault(a => a.Id == id);

            using (var scope = new DataAccessScope())
            {
                // DONE: Delete Action Bar
                await Model.CharactersActionBars.Where(a => a.character == Char).DeleteAsync();
                
                // DONE: Delete Skills
                await Model.CharactersSkills.Where(a => a.character == Char).DeleteAsync();
                
                // DONE: Delete Spells
                await Model.CharactersSpells.Where(a => a.character == Char).DeleteAsync();
                
                // DONE: Delete Inventory
                await Model.CharactersInventorys.Where(a => a.character == Char).DeleteAsync();
                
                // DONE: Delete Reputation
                await Model.CharactersFactions.Where(a => a.character == Char).DeleteAsync();

                // Delete mails +  Delete mail items + Return mails
                // Delete ActionHouse
                // Delete Petitions
                // Delete GM Tickets
                // Delete Corpse
                // Delete Social
                // Delete Quests
                // Delete Honor
                
                // DONE: Delete Character
                await Model.Characters.Where(a => a.Id == id).DeleteAsync();

                await scope.CompleteAsync();
            }
        }

        internal Characters GetCharacter(uint guid)
        {
            return Model.Characters.FirstOrDefault(a => a.Id == guid);
        }

        internal List<CharactersSpells> GetSpells(Characters character)
        {
            return Model.CharactersSpells.Where(a => a.character == character).ToList();
        }

        internal List<CharactersFactions> GetFactions(Characters character)
        {
            return Model.CharactersFactions.Where(a => a.character == character).ToList();
        }

        internal List<CharactersActionBars> GetActionBar(Characters character)
        {
            return Model.CharactersActionBars.Where(a => a.character == character).ToList();
        }

        internal List<CharactersSkills> GetSkills(Characters character)
        {
            return Model.CharactersSkills.Where(a => a.character == character).ToList();
        }
    }
}
