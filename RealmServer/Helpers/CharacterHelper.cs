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
        internal static uint PrefInvSlot(ItemsItem item)
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

        internal static float GetScale(Races race, Genders gender)
        {
            if (race == Races.RACE_TAUREN && gender== Genders.GENDER_MALE)
                return 1.3f;

            if (race == Races.RACE_TAUREN)
                return 1.25f;

            return 1f;
        }

        internal static ManaTypes GetClassManaType(Classes classe)
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
                    return ManaTypes.TypeMana;
                case Classes.CLASS_ROGUE:
                    return ManaTypes.TypeEnergy;
                case Classes.CLASS_WARRIOR:
                    return ManaTypes.TypeRage;
                default:
                    return ManaTypes.TypeMana;
            }
        }

        internal static Genders GetRaceModel(Races race, Genders gender)
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
        internal void GenerateSkills(Characters character)
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
        internal void GenerateFactions(Characters character)
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
        internal void GenerateActionBar(Characters character)
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
        internal void GenerateSpells(Characters character)
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
        internal void GenerateInventory(Characters character)
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

                if (item.id == 6948)
                    stack = 1;

                using (var scope = new DataAccessScope())
                {
                    var inventory = Model.CharactersInventorys.Create();
                        inventory.item = (ulong) item.id;
                        inventory.bag = character.Id;
                        inventory.slot = PrefInvSlot(item) == 23 ? 23 + countBag : PrefInvSlot(item);
                        inventory.stack = (uint) stack;
                        //inventory.durability = item.
                        inventory.flags = 1;

                        inventory.character  = character;                       
                        inventory.created_at = DateTime.Now;

                    scope.Complete();
                }

                if (PrefInvSlot(item) == 23)
                    countBag++;
            }
        }
    }

    public class StatBar
    {
        private int _current;
        public int Bonus;
        public int Base;
        public float Modifier = 1;

        public void Increment(int incrementator = 1)
        {
            if (Current + incrementator < Bonus + Base)
                Current = Current + incrementator;
            else
                Current = Maximum;
        }

        public StatBar(int currentVal, int baseVal, int bonusVal)
        {
            _current = currentVal;
            Bonus = bonusVal;
            Base = baseVal;
        }

        public int Maximum => (int)((Bonus + Base) * Modifier);

        public int Current
        {
            get => (int)(_current * Modifier);
            set
            {
                _current = value <= Maximum ? value : Maximum;
                if (_current < 0)
                    _current = 0;
            }
        }
    }

    public class Stat
    {
        public int Base;
        public short PositiveBonus;
        public short NegativeBonus = 0;
        public float BaseModifier = 1;
        public float Modifier = 1;
        public int RealBase
        {
            get => Base - PositiveBonus + NegativeBonus;
            set
            {
                Base = Base - PositiveBonus + NegativeBonus;
                Base = value;
                Base = Base + PositiveBonus - NegativeBonus;
            }
        }

        public Stat(byte baseValue = 0, byte posValue = 0, byte negValue = 0)
        {
            Base = baseValue;
            PositiveBonus = posValue;
            PositiveBonus = negValue;
        }
    }

    public class Damage
    {
        public float Minimum = 0;
        public float Maximum = 0;
        public int Type = (int) DamageTypes.DmgPhysical;
    }

    public class DamageBonus
    {
        public int PositiveBonus;
        public int NegativeBonus = 0;
        public float Modifier = 1;
        public int Value => (int) ((PositiveBonus - NegativeBonus) * Modifier);

        public DamageBonus(byte posValue = 0, byte negValue = 0)
        {
            PositiveBonus = posValue;
            PositiveBonus = negValue;
        }
    }
}
