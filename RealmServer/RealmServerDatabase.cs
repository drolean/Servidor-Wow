using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common.Database;
using Common.Database.Dbc;
using Common.Database.Tables;
using Common.Database.Xml;
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
            var InitRaceClass = XmlReader.GetRaceClass((Races)handler.Race, (Classes)handler.Classe);

            // Set Character Create Information

            // Set Player Create Action Buttons

            // Set Player Create Skills

            // Set Player Taxi Zones 0-31

            // Set Tutorial Flags

            // Set Player Create Spells

            

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

                // Factions 
                var initiFactions = MainForm.FactionReader.GenerateFactions((Races)handler.Race);

                foreach (var valFaction in initiFactions)
                {
                    string[] fac = valFaction.Split(',');

                    var charFactions = Model.CharactersFactions.Create();
                        charFactions.character  = Char;
                        charFactions.faction    = Int32.Parse(fac[0]);
                        charFactions.flags      = Int32.Parse(fac[1]);
                        charFactions.standing   = Int32.Parse(fac[2]);    
                        charFactions.created_at = DateTime.Now;
                }
               
                // Set Player Create Items
                CharStartOutfit startItems = MainForm.CharacterOutfitReader.Get((Classes)handler.Classe, (Races)handler.Race, (Genders)handler.Gender);

                for (int j = 0; j < 12; ++j)
                {
                    if (startItems.Items[j] <= 0)
                        continue;

                    var item = XmlReader.GetItem(startItems.Items[j]);

                    if (item == null)
                        continue;

                    var charInventory = Model.CharactersInventorys.Create();
                        charInventory.character  = Char;
                        charInventory.item       = (ulong) item.id;
                        charInventory.stack      = 1;
                        charInventory.slot       = PrefInvSlot(item);
                        charInventory.created_at = DateTime.Now;
                }

                // Character Variables
                var initCharacter = MainForm.CharacterOutfitReader.Get((Classes)handler.Classe, (Races)handler.Race, (Genders)handler.Gender);

                scope.Complete();
            }
        }

        private uint PrefInvSlot(ItemsItem item)
        {
            int[] slotTypes = {
                (int)InventorySlots.SLOT_INBACKPACK, // NONE EQUIP
	            (int)InventorySlots.SLOT_HEAD,
                (int)InventorySlots.SLOT_NECK,
                (int)InventorySlots.SLOT_SHOULDERS,
                (int)InventorySlots.SLOT_SHIRT,
                (int)InventorySlots.SLOT_CHEST,
                (int)InventorySlots.SLOT_WAIST,
                (int)InventorySlots.SLOT_LEGS,
                (int)InventorySlots.SLOT_FEET,
                (int)InventorySlots.SLOT_WRISTS,
                (int)InventorySlots.SLOT_HANDS,
                (int)InventorySlots.SLOT_FINGERL,
                (int)InventorySlots.SLOT_TRINKETL,
                (int)InventorySlots.SLOT_MAINHAND, // 1h
	            (int)InventorySlots.SLOT_OFFHAND, // shield
	            (int)InventorySlots.SLOT_RANGED,
                (int)InventorySlots.SLOT_BACK,
                (int)InventorySlots.SLOT_MAINHAND, // 2h
	            (int)InventorySlots.SLOT_BAG1,
                (int)InventorySlots.SLOT_TABARD,
                (int)InventorySlots.SLOT_CHEST, // robe
	            (int)InventorySlots.SLOT_MAINHAND, // mainhand
	            (int)InventorySlots.SLOT_OFFHAND, // offhand
	            (int)InventorySlots.SLOT_MAINHAND, // held
	            (int)InventorySlots.SLOT_INBACKPACK, // ammo
	            (int)InventorySlots.SLOT_RANGED, // thrown
	            (int)InventorySlots.SLOT_RANGED // rangedright
            };

            return (uint)slotTypes[item.inventoryType];
        }
    }


    public enum InventorySlots
    {
        SLOT_HEAD = 0,
        SLOT_NECK = 1,
        SLOT_SHOULDERS = 2,
        SLOT_SHIRT = 3,
        SLOT_CHEST = 4,
        SLOT_WAIST = 5,
        SLOT_LEGS = 6,
        SLOT_FEET = 7,
        SLOT_WRISTS = 8,
        SLOT_HANDS = 9,
        SLOT_FINGERL = 10,
        SLOT_FINGERR = 11,
        SLOT_TRINKETL = 12,
        SLOT_TRINKETR = 13,
        SLOT_BACK = 14,
        SLOT_MAINHAND = 15,
        SLOT_OFFHAND = 16,
        SLOT_RANGED = 17,
        SLOT_TABARD = 18,

        //! Misc Types
        SLOT_BAG1 = 19,
        SLOT_BAG2 = 20,
        SLOT_BAG3 = 21,
        SLOT_BAG4 = 22,
        SLOT_INBACKPACK = 23,

        SLOT_ITEM_START = 23,
        SLOT_ITEM_END = 39,

        SLOT_BANK_ITEM_START = 39,
        SLOT_BANK_ITEM_END = 63,
        SLOT_BANK_BAG_1 = 63,
        SLOT_BANK_BAG_2 = 64,
        SLOT_BANK_BAG_3 = 65,
        SLOT_BANK_BAG_4 = 66,
        SLOT_BANK_BAG_5 = 67,
        SLOT_BANK_BAG_6 = 68,
        SLOT_BANK_END = 69
    }
}
