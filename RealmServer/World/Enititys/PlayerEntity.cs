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
            : base(new ObjectGuid((uint) character.Uid, TypeId.TypeidPlayer, HighGuid.HighguidPlayer))
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
            SetUpdateField((int)UnitFields.UNIT_FIELD_POWER5, 11);

            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, 6);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER1, 7);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER2, 8);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER3, 9);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER4, 10);
            SetUpdateField((int)UnitFields.UNIT_FIELD_MAXPOWER5, 20);

            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, 50); //Level);
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

            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES, 15);     //character.SubResistances.Armor
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_01, 16);  //character.SubResistances.
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_02, 17);  //character.SubResistances.Fire
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_03, 18);  //character.SubResistances.Nature
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_04, 19);  //character.SubResistances.Frost
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_05, 20);  //character.SubResistances.Shadow
            SetUpdateField((int) UnitFields.UNIT_FIELD_RESISTANCES_06, 21);  //character.SubResistances.Arcane

            SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, 8);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_MANA, 60);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_HEALTH, 70);

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
            /*
            SetUpdateField((int)UnitFields.UNIT_TRAINING_POINTS, 26);
            SetUpdateField((int)PlayerFields.PLAYER_FLAGS, 8);

            SetUpdateField((int)PlayerFields.PLAYER_CHARACTER_POINTS1, 21);
            SetUpdateField((int)PlayerFields.PLAYER_CHARACTER_POINTS2, 22);
            SetUpdateField((int)PlayerFields.PLAYER_TRACK_CREATURES, 23);
            SetUpdateField((int)PlayerFields.PLAYER_TRACK_RESOURCES, 24);
            SetUpdateField((int)PlayerFields.PLAYER_BLOCK_PERCENTAGE,25);
            SetUpdateField((int)PlayerFields.PLAYER_DODGE_PERCENTAGE,26);
            SetUpdateField((int)PlayerFields.PLAYER_PARRY_PERCENTAGE,27);
            SetUpdateField((int) PlayerFields.PLAYER_CRIT_PERCENTAGE, 28);

            SetUpdateField((int) PlayerFields.PLAYER_RANGED_CRIT_PERCENTAGE, 29);
            SetUpdateField((int)PlayerFields.PLAYER_EXPLORED_ZONES_1, 30);
            SetUpdateField((int)PlayerFields.PLAYER_REST_STATE_EXPERIENCE, 31);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_COINAGE,  32); // GOLD
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_POSSTAT0, 33);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_POSSTAT1, 34);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_POSSTAT2, 35);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_POSSTAT3, 36);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_POSSTAT4, 37);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_NEGSTAT0, 38);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_NEGSTAT1, 39);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_NEGSTAT2, 40);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_NEGSTAT3, 41);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_NEGSTAT4, 42);

            SetUpdateField((int) PlayerFields.PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE, 43);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE, 44);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_MOD_DAMAGE_DONE_POS, 45);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_MOD_DAMAGE_DONE_NEG, 46);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_MOD_DAMAGE_DONE_PCT, 47);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_BYTES, 48);
            SetUpdateField((int)PlayerFields.PLAYER_AMMO_ID, 49);
            SetUpdateField((int)PlayerFields.PLAYER_SELF_RES_SPELL, 50);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_PVP_MEDALS, 51);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_BUYBACK_PRICE_1, 52);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_BUYBACK_PRICE_LAST, 53);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_BUYBACK_TIMESTAMP_1, 54);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_BUYBACK_TIMESTAMP_LAST, 55);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_SESSION_KILLS, 56);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_YESTERDAY_KILLS, 57);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_LAST_WEEK_KILLS, 58);
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_THIS_WEEK_KILLS, 59);
            /*
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_THIS_WEEK_CONTRIBUTION = 0
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_LIFETIME_HONORABLE_KILLS =
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_LIFETIME_DISHONORABLE_KILLS = 0x42c + UnitFields.UNIT_END, // Size:1
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_YESTERDAY_CONTRIBUTION = 0x42d + UnitFields.UNIT_END, // Size:1
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_LAST_WEEK_CONTRIBUTION = 0x42e + UnitFields.UNIT_END, // Size:1
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_LAST_WEEK_RANK = 0x42f + UnitFields.UNIT_END, // Size:1
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_BYTES2 = 0x430 + UnitFields.UNIT_END, // Size:1
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX = 0x431 + UnitFields.UNIT_END, // Size:1
            SetUpdateField((int)PlayerFields.PLAYER_FIELD_COMBAT_RATING_1 = 0x432 + UnitFields.UNIT_END, // Size:20
            */
        }

        public override int DataLength => (int) PlayerFields.PLAYER_END - 0x4;

        public Characters Character { get; }
        public override string Name => Character.Name;
    }
}