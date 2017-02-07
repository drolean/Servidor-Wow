using Common.Database;
using Common.Database.Dbc;
using Common.Database.Tables;
using Common.Database.Xml;
using Common.Globals;
using Shaolinq;
using System;

namespace RealmServer.Helpers
{
    public class CharacterHelper : DatabaseModel<Models>
    {
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
        
        public static ManaTypes GetClassManaType(Classes classe)
        {
            switch (classe)
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

        public static Genders GetRaceModel(Races race, Genders gender)
        {
            switch (race)
            {
                case Races.RACE_HUMAN:
                    return 49 + gender;
                case Races.RACE_ORC:
                    return 51 + gender;
                case Races.RACE_DWARF:
                    return 53 + gender;
                case Races.RACE_NIGHT_ELF:
                    return 55 + gender;
                case Races.RACE_UNDEAD:
                    return 57 + gender;
                case Races.RACE_TAUREN:
                    return 59 + gender;
                case Races.RACE_GNOME:
                    return 1563 + gender;
                case Races.RACE_TROLL:
                    return 1478 + gender;
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
                    var skill = Model.CharactersSkills.Create();
                        skill.character = character;
                        skill.skill = skillBase.id;
                        skill.value = skillBase.min;
                        skill.max = skillBase.max;
                        skill.created_at = DateTime.Now;

                    scope.Complete();
                }
            }

            foreach (var skillBase in initRaceClass.skills)
            {
                using (var scope = new DataAccessScope())
                {
                    var skill = Model.CharactersSkills.Create();
                    skill.character = character;
                    skill.skill = skillBase.id;
                    skill.value = skillBase.min;
                    skill.max = skillBase.max;
                    skill.created_at = DateTime.Now;

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
                    var actionBar = Model.CharactersActionBars.Create();
                    actionBar.character = character;
                    actionBar.button = actionBase.button;
                    actionBar.action = actionBase.action;
                    actionBar.type = actionBase.type;
                    actionBar.created_at = DateTime.Now;

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
                    var spell = Model.CharactersSpells.Create();
                    spell.character = character;
                    spell.spell = spellBase.id;
                    spell.active = 1;
                    spell.created_at = DateTime.Now;

                    scope.Complete();
                }
            }

            foreach (var spellBase in initRaceClass.spells)
            {
                using (var scope = new DataAccessScope())
                {
                    var spell = Model.CharactersSpells.Create();
                    spell.character = character;
                    spell.spell = spellBase.id;
                    spell.active = 1;
                    spell.created_at = DateTime.Now;

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

                if (PrefInvSlot(item) == 23)
                    countBag++;
            }
        }
    }

    public class TStatBar
    {
        private int _current;
        public int Bonus;
        public int Base;
        public float Modifier = 1;

        public void Increment(int incrementator = 1)
        {
            if (Current + incrementator < (Bonus + Base))
                Current = Current + incrementator;
            else
                Current = Maximum;
        }

        public TStatBar(int currentVal, int baseVal, int bonusVal)
        {
            _current = currentVal;
            Bonus = bonusVal;
            Base = baseVal;
        }

        public int Maximum => (int)((Bonus + Base) * Modifier);

        public int Current
        {
            get { return (int)(_current * Modifier); }
            set
            {
                _current = value <= Maximum ? value : Maximum;
                if (_current < 0)
                    _current = 0;
            }
        }
    }

    public class TStat
    {
        public int Base;
        public short PositiveBonus;
        public short NegativeBonus = 0;
        public float BaseModifier = 1;
        public float Modifier = 1;
        public int RealBase
        {
            get { return Base - PositiveBonus + NegativeBonus; }
            set
            {
                Base = Base - PositiveBonus + NegativeBonus;
                Base = value;
                Base = Base + PositiveBonus - NegativeBonus;
            }
        }

        public TStat(byte baseValue = 0, byte posValue = 0, byte negValue = 0)
        {
            Base = baseValue;
            PositiveBonus = posValue;
            PositiveBonus = negValue;
        }
    }

    public class TDamage
    {
        public float Minimum = 0;
        public float Maximum = 0;
        public int Type = (int) DamageTypes.DMG_PHYSICAL;
    }
}
