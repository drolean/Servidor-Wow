using System;
using System.Collections.Generic;
using Common.Database.Dbc;
using Common.Database.Tables;
using Common.Database.Xml;
using Common.Globals;
using RealmServer.Helpers;

namespace RealmServer.Game.Entitys
{
    public class PlayerEntity : UnitEntity
    {
        public override int DataLength => (int) PlayerFields.PLAYER_END - 0x4;

        public UnitEntity Target;
        public Characters Character { get; }

        // [ ] Character Information
        public int ModelNative;
        public override string Name => Character.name;

        public RealmServerSession Session { get; internal set; }

        // Know Lists
        public List<PlayerEntity> KnownPlayers { get; set; }
        public List<zoneObjeto> KnownGameObjects { get; set; }
        public List<UnitEntity> KnownUnitys { get; set; }

        // gambiarra
        public bool Caindo;

        //
        public PlayerEntity(Characters character)
            : base(new ObjectGuid((uint) character.Id, TypeId.TypeidPlayer, HighGuid.HighguidMoTransport))
        {
            /* Inicializadores */
            Character = character;
            KnownPlayers = new List<PlayerEntity>();
            KnownGameObjects = new List<zoneObjeto>();
            KnownUnitys = new List<UnitEntity>();

            ChrRaces chrRaces = MainProgram.ChrRacesReader.GetData(character.race);

            /* Definindo Character */
            Model = (int) CharacterHelper.GetRaceModel(character.race, character.gender);
            ModelNative = (int) CharacterHelper.GetRaceModel(character.race, character.gender);
            Scale = CharacterHelper.GetScale(character.race, character.gender);
            /* FIM das definições */

            SetUpdateField((int) ObjectFields.OBJECT_FIELD_TYPE, (uint)0x19); // 25
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_SCALE_X, Size);
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_PADDING, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_TARGET, (ulong)0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, Life.Current);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER2, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, Life.Maximum);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER2, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, Level);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, chrRaces.FactionId);

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, BitConverter.ToUInt32(new byte[]
            {
                (byte) character.race,
                (byte) character.classe,
                (byte) character.gender,
                1
            }, 0));

            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT0, 10); //this.Strength);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT1, 20); //this.Agility);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT2, 30); //this.Stamina);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT3, 40); //this.Intellect);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT4, 50); //this.Spirit);


            SetUpdateField((int)UnitFields.UNIT_FIELD_FLAGS, 8);
            SetUpdateField((int)UnitFields.UNIT_FIELD_BASE_MANA, 60);

            
            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, ModelNative);
            // SetUpdateField((int) UnitFields.UNIT_FIELD_MOUNTDISPLAYID, MountDisplayId);

            SetUpdateField((int)UnitFields.UNIT_FIELD_BYTES_1, BitConverter.ToUInt32(new byte[]
            {
                (byte)StandStates.STANDSTATE_STAND,
                0,
                0,
                0
            }, 0));
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_2, 0);

            SetUpdateField((int) PlayerFields.PLAYER_BYTES, BitConverter.ToUInt32(new byte[]
            {
                character.char_skin,
                character.char_face,
                character.char_hairStyle,
                character.char_hairColor
            }, 0));

            SetUpdateField((int)PlayerFields.PLAYER_BYTES_2, BitConverter.ToUInt32(new byte[]
            {
                character.char_facialHair,
                0,
                0,
                3 //RestedState
            }, 0));

            SetUpdateField((int) PlayerFields.PLAYER_BYTES_3, (uint) character.gender);
            SetUpdateField((int) PlayerFields.PLAYER_XP, 47);
            SetUpdateField((int) PlayerFields.PLAYER_NEXT_LEVEL_XP, 400);
            SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1, 26);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, character.watched_faction);

            // Helpers
            //SkillGenerate();

            // Setup Equiped Itens
            //EquipamentoGenerate();

        }

        #region Skills 
        private void SkillGenerate()
        {
            int a = 0;
            foreach (CharactersSkills skill in MainProgram.Database.GetSkills(Character))
            {
                SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1 + skill.Id * 3, skill.skill);
                SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1 + skill.Id * 3 + 1, skill.value + (skill.max << 16));
                // + 2 = Bonus
                a++;
            }
        }

        // Learn Skill
        public void SkillLearn(int skillId, short current = 1, short maximun = 1)
        {
            // if Have Skill
            // --- Already know this skill, just increase value

            // else
            // --- Learn this skill as new

            // Send Skill Update Object
        }
        #endregion

        #region Equipamento
        public void EquipamentoGenerate()
        {
            var inventory = MainProgram.Database.GetInventory(Character);
            for (int j = 0; j < 112; j++)
            {
                if (inventory.Find(item => item.slot == j) != null)
                {
                    if (j < 19)
                    {
                        Console.WriteLine($@"Equip => {inventory.Find(item => item.slot == j).slot} in => {inventory.Find(item => item.slot == j).item}");
                        SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + (int)inventory.Find(item => item.slot == j).slot * 12, inventory.Find(item => item.slot == j).item);
                        SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + j * 12, 0);
                    }

                    SetUpdateField((int) PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + j * 2, inventory.Find(item => item.slot == j).item);
                }
                else
                {
                    if (j < 19)
                    {
                        SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + j * 12, 0);
                        SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + j * 12, 0);
                    }

                    SetUpdateField((int) PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + j * 2, 0);
                }
            }
        }
        #endregion
    }
}