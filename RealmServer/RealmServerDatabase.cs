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
        internal static CharacterInitializator Helper { get; set; }

        // Pega conta do usuario baseado no login
        internal Users GetAccount(string username) => !Model.Users.Any() ? null : Model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());

        // Retorna lista de chars do usuario
        internal List<Characters> GetCharacters(string username)
        {
            Users account = GetAccount(username);
            return Model.Characters.Where(a => a.user == account).ToList();
        }

        // Pega Char pelo nome
        internal Characters GetCharacaterByName(string username) => !Model.Characters.Any() ? null : Model.Characters.FirstOrDefault(a => a.name.ToLower() == username.ToLower());

        internal void CreateChar(CmsgCharCreate handler, Users users)
        {
            Helper = new CharacterInitializator();

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

            var Character = MainForm.Database.GetCharacaterByName(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(handler.Name));

            Helper.GenerateActionBar(Character);      // DONE: Generate Action Bar
            Helper.GenerateFactions(Character);       // DONE: Generate Reputation Factions
            Helper.GenerateSkills(Character);         // DONE: Generate Skills
            Helper.GenerateSpells(Character);         // DONE: Generate Spells
            Helper.GenerateInventory(Character);      // Generate Inventory

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

            return;
        }

        internal async void DeleteCharacter(int id)
        {
            // Check if char is own of player
                // if not BAN account and IP

            Characters Char = Model.Characters.FirstOrDefault(a => a.Id == id);

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
    }
}
