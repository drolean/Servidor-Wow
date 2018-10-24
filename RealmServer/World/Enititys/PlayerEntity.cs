using System;
using System.Collections.Generic;
using Common.Database.Tables;
using RealmServer.Enums;
using RealmServer.Helpers;

namespace RealmServer.World.Enititys
{
    public class PlayerEntity : UnitEntity
    {
        public new int Model;

        public PlayerEntity(Characters character)
            : base(new ObjectGuid((uint) character.Uid, TypeId.TypeidPlayer, HighGuid.HighguidPlayer))
        {
            Character = character;
            KnownPlayers = new List<PlayerEntity>();
            KnownCreatures = new List<SpawnCreatures>();

            var chrRaces = MainProgram.ChrRacesReader.GetData(character.Race);

            Model = (int) CharacterHelper.GetRaceModel(character.Race, character.Gender);
            Scale = CharacterHelper.GetScale(character.Race, character.Gender);

            SetUpdateField((int) ObjectFields.Type, (uint) 0x19); // 25
            SetUpdateField((int) ObjectFields.ScaleX, Size);
            SetUpdateField((int) ObjectFields.Padding, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_TARGET, (ulong) 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, 1);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER1, 2);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER2, 3);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER3, 4);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER4, 5);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER5, 6);

            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, 7);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER1, 8);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER2, 9);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER3, 10);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER4, 11);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER5, 12);

            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, character.Level);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, chrRaces.FactionId);

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, BitConverter.ToUInt32(new byte[]
            {
                (byte) character.Race,
                (byte) character.Classe,
                (byte) character.Gender,
                1
            }, 0));

            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT0, 13); //character.SubStats.Strength);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT1, 14); //character.SubStats.Agility);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT2, 15); //character.SubStats.Stamina);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT3, 16); //character.SubStats.Intellect);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT4, 17); //character.SubStats.Spirit);

            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES, 18); //character.SubResistances.Armor
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_01, 19); //character.SubResistances.
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_02, 20); //character.SubResistances.Fire
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_03, 21); //character.SubResistances.Nature
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_04, 22); //character.SubResistances.Frost
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_05, 23); //character.SubResistances.Shadow
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_06, 24); //character.SubResistances.Arcane

            SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, 8);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_MANA, 60);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_HEALTH, 70);

            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MOUNTDISPLAYID, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_1, BitConverter.ToUInt32(new byte[]
            {
                (byte) StandStates.Stand,
                0,
                0,
                0
            }, 0));
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_2, 0);

            SetUpdateField((int) PlayerFields.PLAYER_BYTES, BitConverter.ToUInt32(new[]
            {
                character.SubSkin.Skin,
                character.SubSkin.Face,
                character.SubSkin.HairStyle,
                character.SubSkin.HairColor
            }, 0));

            SetUpdateField((int) PlayerFields.PLAYER_BYTES_2, BitConverter.ToUInt32(new byte[]
            {
                character.SubSkin.FacialHair,
                0,
                0,
                3 //RestedState
            }, 0));

            SetUpdateField((int) PlayerFields.PLAYER_BYTES_3, (uint) character.Gender);
            SetUpdateField((int) PlayerFields.PLAYER_XP, 0);
            SetUpdateField((int) PlayerFields.PLAYER_NEXT_LEVEL_XP, 400);
            SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1, 26);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, character.WatchFaction);

            SkillGenerate();
        }

        public override int DataLength => (int) PlayerFields.PLAYER_END - 0x4;

        public Characters Character { get; }
        public override string Name => Character.Name;
        public RealmServerSession Session { get; set; }
        public List<PlayerEntity> KnownPlayers { get; set; }
        public List<SpawnCreatures> KnownCreatures { get; set; }

        private void SkillGenerate()
        {
            var a = 0;
            foreach (var skill in Character.SubSkills)
            {
                SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1 + a * 3, skill.Skill);
                SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1 + a * 3 + 1, skill.Value + (skill.Max << 16));
                a++;
            }
        }
    }
}