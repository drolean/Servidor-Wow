using System;
using Common.Database.Tables;
using RealmServer.Enums;

namespace RealmServer.World.Enititys
{
    public class PlayerEntity : UnitEntity
    {
        // [ ] Character Information
        public int ModelNative;

        //
        public PlayerEntity(Characters character)
            : base(new ObjectGuid((uint) character.Uid, TypeId.TypeidPlayer, HighGuid.HighguidMoTransport))
        {
            /* Inicializadores */
            Character = character;

            var chrRaces = MainProgram.ChrRacesReader.GetData(character.Race);

            /* Definindo Character */
            Model = 50;
            ModelNative = 50;
            Scale = 1f;
            /* FIM das definições */

            SetUpdateField((int) ObjectFields.OBJECT_FIELD_TYPE, (uint) 0x19); // 25
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_SCALE_X, Size);
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_PADDING, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_TARGET, (ulong) 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, 1);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER1, 2);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER2, 3);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER3, 4);
            SetUpdateField((int) UnitFields.UNIT_FIELD_POWER4, 5);

            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, 6);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER1, 7);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER2, 8);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER3, 9);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER4, 10);

            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, Level);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, chrRaces.FactionId);

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, BitConverter.ToUInt32(new byte[]
            {
                (byte) character.Race,
                (byte) character.Classe,
                (byte) character.Gender,
                1
            }, 0));

            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT0, 11); //this.Strength);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT1, 12); //this.Agility);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT2, 13); //this.Stamina);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT3, 14); //this.Intellect);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT4, 15); //this.Spirit);

            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES, 16);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_01, 16);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_02, 17);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_03, 18);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_04, 19);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_05, 20);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_06, 21);

            SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, 8);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_MANA, 60);


            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, ModelNative);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MOUNTDISPLAYID, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_1, BitConverter.ToUInt32(new byte[]
            {
                (byte) StandStates.STANDSTATE_STAND,
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
            //SetUpdateField((int)PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, character.watched_faction);
        }

        public override int DataLength => (int) PlayerFields.PLAYER_END - 0x4;

        public Characters Character { get; }
        public override string Name => Character.Name;
    }
}