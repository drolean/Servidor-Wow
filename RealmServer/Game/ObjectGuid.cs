namespace RealmServer.Game
{
    public class ObjectGuid
    {
        public TypeId TypeId { get; private set; }
        public HighGuid HighGuid { get; private set; }
        public ulong RawGuid { get; }

        public ObjectGuid(ulong guid)
        {
            RawGuid = guid;
        }

        public ObjectGuid(uint index, TypeId type, HighGuid high)
        {
            TypeId = type;
            HighGuid = high;
            RawGuid = index | ((ulong) type << 24) | ((ulong) high << 48);
        }
    }

    public enum HighGuid
    {
        HighguidPlayer        = 0x0000,
        HighguidItem          = 0x4700,
        HighguidContainer     = 0x4700,
        HighguidDynamicobject = 0xF100,
        HighguidGameobject    = 0xF110,
        HighguidTransport     = 0xF120,
        HighguidUnit          = 0xF130,
        HighguidPet           = 0xF140,
        HighguidVehicle       = 0xF150,       
        HighguidCorpse        = 0xF500,
        HighguidMoTransport   = 0x1FC0,
    }
}