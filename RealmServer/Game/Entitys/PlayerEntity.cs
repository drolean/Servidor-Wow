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

            ChrRaces chrRaces = MainForm.ChrRacesReader.GetData(character.race);

            /* Definindo Character */
            Model = (int) CharacterHelper.GetRaceModel(character.race, character.gender);
            ModelNative = (int) CharacterHelper.GetRaceModel(character.race, character.gender);
            Scale = CharacterHelper.GetScale(character.race, character.gender);
            /* FIM das definições */

            SetUpdateField((int) ObjectFields.OBJECT_FIELD_TYPE, 25);
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_SCALE_X, Size);

            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, Life.Current);

            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, Life.Maximum);

            // To Display Level Correct need this [START] 
            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, Level);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, chrRaces.FactionId);
            // [END]

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, (byte) character.race);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, (byte) character.classe, 1);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, (byte) character.gender, 2);

            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, ModelNative);

            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_skin);
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_face, 1);
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_hairStyle, 2);
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_hairColor, 3);

            SetUpdateField((int) PlayerFields.PLAYER_BYTES_2, character.char_facialHair);

            SetUpdateField((int) PlayerFields.PLAYER_XP, 0);
            SetUpdateField((int) PlayerFields.PLAYER_NEXT_LEVEL_XP, 400);
            SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1, 26);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, character.watched_faction);

            // Helpers
            SkillGenerate();

            // Setup Equiped Itens
            EquipamentoGenerate();

        }

        #region Skills 
        private void SkillGenerate()
        {
            int a = 0;
            foreach (CharactersSkills skill in MainForm.Database.GetSkills(Character))
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
            var inventory = MainForm.Database.GetInventory(Character);
            for (int j = 0; j < 112; j++)
            {
                if (inventory.Find(item => item.slot == j) != null)
                {
                    if (j < 19)
                    {
                        Console.WriteLine($"Veio aqui algo besta => {inventory.Find(item => item.slot == j).slot} em => {inventory.Find(item => item.slot == j).item}");
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