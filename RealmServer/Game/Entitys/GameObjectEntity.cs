using System;
using Common.Database.Xml;

namespace RealmServer.Game.Entitys
{
    public class GameObjectEntity : ObjectEntity
    {
        public TypeId TypeId => TypeId.TypeidGameobject;
        public override int DataLength => (int)GameObjectFields.GAMEOBJECT_END;

        public zoneObjeto GameObjects { get; set; }

        public GameObjectEntity(zoneObjeto zoneObjeto) : base(new ObjectGuid(zoneObjeto.id, TypeId.TypeidGameobject, HighGuid.HighguidGameobject))
        {
            GameObjects = zoneObjeto;

            SetUpdateField((int) ObjectFields.OBJECT_FIELD_TYPE, 0x21);
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_ENTRY, (byte) 1);
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_SCALE_X, 1);
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_DISPLAYID, 31); //zoneObjeto.entry);

            SetUpdateField((int) GameObjectFields.GAMEOBJECT_FLAGS, 00); //(int)zoneObjeto.flags);
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_TYPE_ID, 2); //zoneObjeto.type);

            SetUpdateField((int) GameObjectFields.GAMEOBJECT_POS_X, zoneObjeto.map.mapX);
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_POS_Y, zoneObjeto.map.mapY);
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_POS_Z, zoneObjeto.map.mapZ);
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_FACING, zoneObjeto.map.mapO);

            SetUpdateField((int) GameObjectFields.GAMEOBJECT_DYN_FLAGS, 2); //zoneObjeto.type);

            Console.WriteLine($@"=> Adicionado Objeto [{zoneObjeto.name}] => [{(GameObjectType) zoneObjeto.type}]");
        }
    }

    public enum ObjectFields
    {
        OBJECT_FIELD_GUID = 0x00, // Size:2
        OBJECT_FIELD_DATA = 0x01, // Size:2
        OBJECT_FIELD_TYPE = 0x02, // Size:1
        OBJECT_FIELD_ENTRY = 0x03, // Size:1
        OBJECT_FIELD_SCALE_X = 0x04, // Size:1
        OBJECT_FIELD_PADDING = 0x05, // Size:1
        OBJECT_END = 0x06
    }

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
        GAMEOBJECT_END = ObjectFields.OBJECT_END + 0x14
    }

    public enum GameObjectType : byte
    {
        GAMEOBJECT_TYPE_DOOR = 0,
        GAMEOBJECT_TYPE_BUTTON = 1,
        GAMEOBJECT_TYPE_QUESTGIVER = 2,
        GAMEOBJECT_TYPE_CHEST = 3,
        GAMEOBJECT_TYPE_BINDER = 4,
        GAMEOBJECT_TYPE_GENERIC = 5,
        GAMEOBJECT_TYPE_TRAP = 6,
        GAMEOBJECT_TYPE_CHAIR = 7,
        GAMEOBJECT_TYPE_SPELL_FOCUS = 8,
        GAMEOBJECT_TYPE_TEXT = 9,
        GAMEOBJECT_TYPE_GOOBER = 10,
        GAMEOBJECT_TYPE_TRANSPORT = 11,
        GAMEOBJECT_TYPE_AREADAMAGE = 12,
        GAMEOBJECT_TYPE_CAMERA = 13,
        GAMEOBJECT_TYPE_MAPOBJECT = 14,
        GAMEOBJECT_TYPE_MO_TRANSPORT = 15,
        GAMEOBJECT_TYPE_DUELFLAG = 16,
        GAMEOBJECT_TYPE_FISHINGNODE = 17,
        GAMEOBJECT_TYPE_SUMMONING_RITUAL = 18,
        GAMEOBJECT_TYPE_MAILBOX = 19,
        GAMEOBJECT_TYPE_DONOTUSE = 20,
        GAMEOBJECT_TYPE_GUARDPOST = 21,
        GAMEOBJECT_TYPE_SPELLCASTER = 22,
        GAMEOBJECT_TYPE_MEETINGSTONE = 23,
        GAMEOBJECT_TYPE_FLAGSTAND = 24,
        GAMEOBJECT_TYPE_FISHINGHOLE = 25,
        GAMEOBJECT_TYPE_FLAGDROP = 26,
        GAMEOBJECT_TYPE_MINI_GAME = 27,
        GAMEOBJECT_TYPE_LOTTERYKIOSK = 28,
        GAMEOBJECT_TYPE_CAPTURE_POINT = 29,
        GAMEOBJECT_TYPE_AURA_GENERATOR = 30,
        GAMEOBJECT_TYPE_DUNGEON_DIFFICULTY = 31,
        GAMEOBJECT_TYPE_BARBER_CHAIR = 32,
        GAMEOBJECT_TYPE_DESTRUCTIBLE_BUILDING = 33,
        GAMEOBJECT_TYPE_GUILD_BANK = 34,
        GAMEOBJECT_TYPE_TRAPDOOR = 35
    }
}