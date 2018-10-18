using System;
using Common.Database.Tables;
using RealmServer.Enums;
using RealmServer.Helpers;

namespace RealmServer.World.Enititys
{
    public class PlayerEntity : UnitEntity
    {
        public new int Model;
        public override int DataLength => (int) PlayerFields.PLAYER_END - 0x4;

        public Characters Character { get; }
        public override string Name => Character.Name;
        public RealmServerSession Session { get; set; }

        public PlayerEntity(Characters character)
            : base(new ObjectGuid((uint) character.Uid, TypeId.TypeidPlayer, HighGuid.HighguidPlayer))
        {
            Character = character;

            var chrRaces = MainProgram.ChrRacesReader.GetData(character.Race);

            Model = (int) CharacterHelper.GetRaceModel(character.Race, character.Gender);
            Scale = CharacterHelper.GetScale(character.Race, character.Gender);

            SetUpdateField((int) ObjectFields.Type, (uint) 0x19); // 25
            SetUpdateField((int) ObjectFields.ScaleX, Size);


            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, 1);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_POWER1, 2);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_POWER2, 3);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_POWER3, 4);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_POWER4, 5);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_POWER5, 11);

            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, 6);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER1, 7);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER2, 8);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER3, 9);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER4, 10);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER5, 20);

            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, character.Level);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, chrRaces.FactionId);

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, BitConverter.ToUInt32(new byte[]
            {
                (byte) character.Race,
                (byte) character.Classe,
                (byte) character.Gender,
                1
            }, 0));

            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT0, 1); //character.SubStats.Strength);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT1, 1); //character.SubStats.Agility);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT2, 1); //character.SubStats.Stamina);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT3, 1); //character.SubStats.Intellect);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT4, 1); //character.SubStats.Spirit);

            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES, 15); //character.SubResistances.Armor
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_01, 16); //character.SubResistances.
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_02, 17); //character.SubResistances.Fire
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_03, 18); //character.SubResistances.Nature
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_04, 19); //character.SubResistances.Frost
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_05, 20); //character.SubResistances.Shadow
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_06, 21); //character.SubResistances.Arcane

            SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, 8);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_MANA, 60);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_HEALTH, 70);

            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MOUNTDISPLAYID, 0);
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

        private void SkillGenerate()
        {
            int a = 0;
            foreach (var skill in Character.SubSkills)
            {
                SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1 + a * 3, skill.Skill);
                SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1 + a * 3 + 1, skill.Value + (skill.Max << 16));
                a++;
            }
        }
    }
}