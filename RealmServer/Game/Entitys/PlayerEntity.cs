using Common.Database.Tables;
using Common.Globals;
using RealmServer.Helpers;

namespace RealmServer.Game.Entitys
{
    public class UnitEntity : ObjectEntity
    {
        public UnitEntity(ObjectGuid objectGuid) : base(objectGuid)
        {
        }

        public override int DataLength => (int) UnitFields.UNIT_END - 0x4;
    }

    public class PlayerEntity : UnitEntity
    {
        public override int DataLength => (int) PlayerFields.PLAYER_END - 0x4;
        
        public UnitEntity Target;

        // [ ] Character Information
        public int ModelNative;

        public PlayerEntity(Characters character)
            : base(new ObjectGuid((uint) character.Id, TypeId.TypeidPlayer, HighGuid.HighguidMoTransport))
        {
            /* Definindo Character */
            Model = (int) CharacterHelper.GetRaceModel(character.race, character.gender);
            ModelNative = (int) CharacterHelper.GetRaceModel(character.race, character.gender);
            /* FIM das definições */

            SetUpdateField((int) ObjectFields.OBJECT_FIELD_TYPE, 25);
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_SCALE_X, Size);

            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, Life.Current);

            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, Life.Maximum);

            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, Level);

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, (byte) character.race);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, (byte) character.classe, 1);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, (byte) character.gender, 2);

            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, ModelNative);

            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_skin);
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_face, 1);
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_hairStyle, 2);
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_hairColor, 3);
        }
    }
}