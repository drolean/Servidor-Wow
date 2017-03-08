using System;
using Common.Database.Xml;
using Common.Globals;

namespace RealmServer.Game.Entitys
{
    public class GameObjectEntity : ObjectEntity
    {
        public TypeId TypeId => TypeId.TypeidGameobject;
        public override int DataLength => (int)GameObjectFields.GAMEOBJECT_END;

        public zoneObjeto GameObjects { get; set; }

        //
        public GameObjectLootState State = GameObjectLootState.LootUnlooted;
        public int Faction = 0;

        public static int Ababa;
        public GameObjectEntity(zoneObjeto zoneObjeto)
            : base(new ObjectGuid((uint)(zoneObjeto.id + Ababa), TypeId.TypeidGameobject, HighGuid.HighguidGameobject))
        {
            GameObjects = zoneObjeto;

            SetUpdateField((int) ObjectFields.OBJECT_FIELD_TYPE, 33); // 33 <= aqui existe algum id que faz cachear no client
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_ENTRY, (byte) zoneObjeto.id); // 31
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_SCALE_X, 1f); // scala do objeto

            SetUpdateField((int) GameObjectFields.GAMEOBJECT_DISPLAYID, (uint) zoneObjeto.model); // Modelo display do Objeto
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_TYPE_ID, (uint) zoneObjeto.type); // tipo do objeto se e clicavel correio ou mouse hoiver
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_FLAGS, (int) zoneObjeto.flags);
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_FACTION, Faction); // realmente precisa disso aqui

            // Se o objeto tem alum dono ou alguem que criou tem que setar isso aqui em baixo
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_POS_X, zoneObjeto.map.mapX);
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_POS_Y, zoneObjeto.map.mapY);
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_POS_Z, zoneObjeto.map.mapZ);
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_FACING, zoneObjeto.map.mapO);

            // If a game object has bit 4 set in the flag it needs to be activated (used for quests)
            // DynFlags = Activate a game object (Chest = 9, Goober = 1)
            int DynFlags = 0;

            if (DynFlags != 0)
                SetUpdateField((int) GameObjectFields.GAMEOBJECT_DYN_FLAGS, DynFlags);

            // Estado do Objeto @GameObjectLootState
            SetUpdateField((int) GameObjectFields.GAMEOBJECT_STATE, State);

            // Level do Objeto ??? precisa disso
            if (Level > 0)
                SetUpdateField((int) GameObjectFields.GAMEOBJECT_LEVEL, Level);           

            //Update.SetUpdateFlag(GameObjectFields.GAMEOBJECT_ROTATION, Rotations(0))
            //Update.SetUpdateFlag(GameObjectFields.GAMEOBJECT_ROTATION + 1, Rotations(1))
            //Update.SetUpdateFlag(GameObjectFields.GAMEOBJECT_ROTATION + 2, Rotations(2))
            //Update.SetUpdateFlag(GameObjectFields.GAMEOBJECT_ROTATION + 3, Rotations(3))

            Console.WriteLine($@"=> Adicionado Objeto [{zoneObjeto.name}] => [{(GameObjectType) zoneObjeto.type}] => {Ababa}");
            Ababa++;
        }
    }

    public enum GameObjectLootState
    {
        DoorOpen = 0,
        DoorClosed = 1,
        LootUnaviable = 0,
        LootUnlooted = 1,
        LootLooted = 2
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