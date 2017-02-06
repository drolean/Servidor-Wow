using Common.Database;
using Common.Database.Dbc;
using Common.Database.Tables;
using Common.Database.Xml;
using Common.Globals;
using Shaolinq;
using System;

namespace RealmServer.Helpers
{
    public class CharacterInitializator : DatabaseModel<Models>
    {

        #region ENUM's
        public enum InventorySlots
        {
            SLOT_START      = 0,
            SLOT_HEAD       = 0,
            SLOT_NECK       = 1,
            SLOT_SHOULDERS  = 2,
            SLOT_SHIRT      = 3,
            SLOT_CHEST      = 4,
            SLOT_WAIST      = 5,
            SLOT_LEGS       = 6,
            SLOT_FEET       = 7,
            SLOT_WRISTS     = 8,
            SLOT_HANDS      = 9,
            SLOT_FINGERL    = 10,
            SLOT_FINGERR    = 11,
            SLOT_TRINKETL   = 12,
            SLOT_TRINKETR   = 13,
            SLOT_BACK       = 14,
            SLOT_MAINHAND   = 15,
            SLOT_OFFHAND    = 16,
            SLOT_RANGED     = 17,
            SLOT_TABARD     = 18,
            SLOT_END        = 19,

            // Misc Types
            SLOT_BAG_START  = 19,
            SLOT_BAG1       = 19,
            SLOT_BAG2       = 20,
            SLOT_BAG3       = 21,
            SLOT_BAG4       = 22,
            SLOT_INBACKPACK = 23,
            SLOT_BAG_END    = 23,

            SLOT_ITEM_START = 23,
            SLOT_ITEM_END   = 39,

            SLOT_BANK_ITEM_START    = 39,
            SLOT_BANK_ITEM_END      = 63,
            SLOT_BANK_BAG_1         = 63,
            SLOT_BANK_BAG_2         = 64,
            SLOT_BANK_BAG_3         = 65,
            SLOT_BANK_BAG_4         = 66,
            SLOT_BANK_BAG_5         = 67,
            SLOT_BANK_BAG_6         = 68,
            SLOT_BANK_END           = 69
        }

        public enum ManaTypes : int
        {
            TYPE_MANA = 0,
            TYPE_RAGE = 1,
            TYPE_FOCUS = 2,
            TYPE_ENERGY = 3,
            TYPE_HAPPINESS = 4,
            TYPE_HEALTH = -2
        }
        #endregion

        public static uint PrefInvSlot(ItemsItem item)
        {
            int[] slotTypes = {
                (int)InventorySlots.SLOT_INBACKPACK,    // NONE EQUIP
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
                (int)InventorySlots.SLOT_MAINHAND,      // 1h
	            (int)InventorySlots.SLOT_OFFHAND,       // shield
	            (int)InventorySlots.SLOT_RANGED,
                (int)InventorySlots.SLOT_BACK,
                (int)InventorySlots.SLOT_MAINHAND,      // 2h
	            (int)InventorySlots.SLOT_BAG1,
                (int)InventorySlots.SLOT_TABARD,
                (int)InventorySlots.SLOT_CHEST,         // robe
	            (int)InventorySlots.SLOT_MAINHAND,      // mainhand
	            (int)InventorySlots.SLOT_OFFHAND,       // offhand
	            (int)InventorySlots.SLOT_MAINHAND,      // held
	            (int)InventorySlots.SLOT_INBACKPACK,    // ammo
	            (int)InventorySlots.SLOT_RANGED,        // thrown
	            (int)InventorySlots.SLOT_RANGED         // rangedright
            };

            return (uint)slotTypes[item.inventoryType];
        }
        
        public static ManaTypes GetClassManaType(Classes Classe)
        {
            switch (Classe)
            {
                case Classes.CLASS_DRUID:
                case Classes.CLASS_HUNTER:
                case Classes.CLASS_MAGE:
                case Classes.CLASS_PALADIN:
                case Classes.CLASS_PRIEST:
                case Classes.CLASS_SHAMAN:
                case Classes.CLASS_WARLOCK:
                    return ManaTypes.TYPE_MANA;
                case Classes.CLASS_ROGUE:
                    return ManaTypes.TYPE_ENERGY;
                case Classes.CLASS_WARRIOR:
                    return ManaTypes.TYPE_RAGE;
                default:
                    return ManaTypes.TYPE_MANA;
            }
        }

        public static Genders GetRaceModel(Races Race, Genders Gender)
        {
            switch (Race)
            {
                case Races.RACE_HUMAN:
                    return 49 + Gender;
                case Races.RACE_ORC:
                    return 51 + Gender;
                case Races.RACE_DWARF:
                    return 53 + Gender;
                case Races.RACE_NIGHT_ELF:
                    return 55 + Gender;
                case Races.RACE_UNDEAD:
                    return 57 + Gender;
                case Races.RACE_TAUREN:
                    return 59 + Gender;
                case Races.RACE_GNOME:
                    return 1563 + Gender;
                case Races.RACE_TROLL:
                    return 1478 + Gender;
            }

            return 16358 + Genders.GENDER_MALE;
        }

        // DONE: Generate Skills for Creation Char 
        public void GenerateSkills(Characters character)
        {
            var initRace = XmlReader.GetRace(character.race);
            var initRaceClass = XmlReader.GetRaceClass(character.race, character.classe);

            foreach (var skillBase in initRace.skills)
            {
                using (var scope = new DataAccessScope())
                {
                    var Skill = Model.CharactersSkills.Create();
                        Skill.character = character;
                        Skill.skill = skillBase.id;
                        Skill.value = skillBase.min;
                        Skill.max = skillBase.max;
                        Skill.created_at = DateTime.Now;

                    scope.Complete();
                }
            }

            foreach (var skillBase in initRaceClass.skills)
            {
                using (var scope = new DataAccessScope())
                {
                    var Skill = Model.CharactersSkills.Create();
                    Skill.character = character;
                    Skill.skill = skillBase.id;
                    Skill.value = skillBase.min;
                    Skill.max = skillBase.max;
                    Skill.created_at = DateTime.Now;

                    scope.Complete();
                }
            }
        }

        // DONE: Generate Factions(Reputation) for Creation Char
        public void GenerateFactions(Characters character)
        {
            var initiFactions = MainForm.FactionReader.GenerateFactions(character.race);

            foreach (var valFaction in initiFactions)
            {
                string[] faction = valFaction.Split(',');

                using (var scope = new DataAccessScope())
                {
                    var charFactions = Model.CharactersFactions.Create();
                    charFactions.character = character;
                    charFactions.faction = int.Parse(faction[0]);
                    charFactions.flags = int.Parse(faction[1]);
                    charFactions.standing = int.Parse(faction[2]);
                    charFactions.created_at = DateTime.Now;

                    scope.Complete();
                }
            }
        }

        // DONE: Generate ActionBar for Creation Char
        public void GenerateActionBar(Characters character)
        {
            var initRace = XmlReader.GetRace(character.race);
            var initRaceClass = XmlReader.GetRaceClass(character.race, character.classe);
            /*
            foreach (var actionBase in initRace.actions)
            {
                using (var scope = new DataAccessScope())
                {
                    var ActionBar = Model.CharactersActionBars.Create();
                    ActionBar.character = character;
                    ActionBar.button = actionBase.button;
                    ActionBar.action = actionBase.action;
                    ActionBar.type = actionBase.type;
                    ActionBar.created_at = DateTime.Now;

                    scope.Complete();
                }
            }
            */
            foreach (var actionBase in initRaceClass.actions)
            {
                using (var scope = new DataAccessScope())
                {
                    var ActionBar = Model.CharactersActionBars.Create();
                    ActionBar.character = character;
                    ActionBar.button = actionBase.button;
                    ActionBar.action = actionBase.action;
                    ActionBar.type = actionBase.type;
                    ActionBar.created_at = DateTime.Now;

                    scope.Complete();
                }
            }
        }

        // DONE: Generate Spells for Creation Char Player
        public void GenerateSpells(Characters character)
        {
            var initRace = XmlReader.GetRace(character.race);
            var initRaceClass = XmlReader.GetRaceClass(character.race, character.classe);

            foreach (var spellBase in initRace.spells)
            {
                using (var scope = new DataAccessScope())
                {
                    var Spell = Model.CharactersSpells.Create();
                    Spell.character = character;
                    Spell.spell = spellBase.id;
                    Spell.active = 1;
                    Spell.created_at = DateTime.Now;

                    scope.Complete();
                }
            }

            foreach (var spellBase in initRaceClass.spells)
            {
                using (var scope = new DataAccessScope())
                {
                    var Spell = Model.CharactersSpells.Create();
                    Spell.character = character;
                    Spell.spell = spellBase.id;
                    Spell.active = 1;
                    Spell.created_at = DateTime.Now;

                    scope.Complete();
                }
            }
        }

        // Generate Player Taxi Zones

        // Generate Inventory for Creation Char
        public void GenerateInventory(Characters character)
        {
            var stack = 1;
            uint countBag = 0;
            CharStartOutfit startItems = MainForm.CharacterOutfitReader.Get(character.classe, character.race, character.gender);

            if (startItems == null)
                return;

            for (int j = 0; j < startItems.Items.Length; ++j)
            {
                if (startItems.Items[j] <= 0)
                    continue;

                var item = XmlReader.GetItem(startItems.Items[j]);

                if (item == null)
                    continue;

                if (item.@class == 0)
                    stack = 5;

                using (var scope = new DataAccessScope())
                {
                    var inventory = Model.CharactersInventorys.Create();
                        inventory.character  = character;
                        inventory.item       = (ulong)item.id;
                        inventory.stack      = (uint) stack;
                        inventory.slot       = (PrefInvSlot(item) == 23) ? 23 + countBag : PrefInvSlot(item);
                        inventory.created_at = DateTime.Now;

                    scope.Complete();
                }

                if (CharacterInitializator.PrefInvSlot(item) == 23)
                    countBag++;
            }
        }
    }
}
