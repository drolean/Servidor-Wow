namespace RealmServer.Game.Entitys
{
    public class GameObjectEntity : ObjectEntity
    {
        public GameObjectEntity(ObjectGuid objectGuid) : base(objectGuid)
        {
        }

        public TypeId TypeId => TypeId.TypeidGameobject;
        public override int DataLength => (int) GameObjectFields.GAMEOBJECT_END;

        //public WorldGameObjects GameObjects { get; private set; }
    }


    public enum ObjectFields
    {
        OBJECT_FIELD_GUID = 0x00, // Size:2
        OBJECT_FIELD_DATA = 0x01, // Size:2
        OBJECT_FIELD_TYPE = 0x02, // Size:1
        OBJECT_FIELD_ENTRY = 0x03, // Size:1
        OBJECT_FIELD_SCALE_X = 0x04, // Size:1
        OBJECT_FIELD_PADDING = 0x05, // Size:1
        OBJECT_END = 0x06,
    };

    public enum GameObjectFields
    {
        OBJECT_FIELD_CREATED_BY = ObjectFields.OBJECT_END + 0x00,
        GAMEOBJECT_DISPLAYID = ObjectFields.OBJECT_END + 0x02,
        GAMEOBJECT_FLAGS = ObjectFields.OBJECT_END + 0x03,
        GAMEOBJECT_ROTATION = ObjectFields.OBJECT_END + 0x04,
        GAMEOBJECT_STATE = ObjectFields.OBJECT_END + 0x08,
        GAMEOBJECT_POS_X = ObjectFields.OBJECT_END + 0x09,
        GAMEOBJECT_POS_Y = ObjectFields.OBJECT_END + 0x0A,
        GAMEOBJECT_POS_Z = ObjectFields.OBJECT_END + 0x0B,
        GAMEOBJECT_FACING = ObjectFields.OBJECT_END + 0x0C,
        GAMEOBJECT_DYN_FLAGS = ObjectFields.OBJECT_END + 0x0D,
        GAMEOBJECT_FACTION = ObjectFields.OBJECT_END + 0x0E,
        GAMEOBJECT_TYPE_ID = ObjectFields.OBJECT_END + 0x0F,
        GAMEOBJECT_LEVEL = ObjectFields.OBJECT_END + 0x10,
        GAMEOBJECT_ARTKIT = ObjectFields.OBJECT_END + 0x11,
        GAMEOBJECT_ANIMPROGRESS = ObjectFields.OBJECT_END + 0x12,
        GAMEOBJECT_PADDING = ObjectFields.OBJECT_END + 0x13,
        GAMEOBJECT_END = ObjectFields.OBJECT_END + 0x14,
    };
}