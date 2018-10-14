using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_INIT_WORLD_STATES : Common.Network.PacketServer
    {
        public SMSG_INIT_WORLD_STATES(Characters character) : base(RealmEnums.SMSG_INIT_WORLD_STATES)
        {
            ushort numberOfFields;

            switch (character.SubMap.MapZone)
            {
                case 2918:
                    numberOfFields = 6;
                    break;
                default:
                    numberOfFields = 10;
                    break;
            }

            Write((ulong) character.SubMap.MapId);
            Write((uint) character.SubMap.MapZone);
            Write((uint) 0); // Area ID
            Write(numberOfFields);
            Write((ulong) 0x8d8);
            Write((ulong) 0x0);
            Write((ulong) 0x8d7);
            Write((ulong) 0x0);
            Write((ulong) 0x8d6);
            Write((ulong) 0x0);
            Write((ulong) 0x8d5);
            Write((ulong) 0x0);
            Write((ulong) 0x8d4);
            Write((ulong) 0x0);
            Write((ulong) 0x8d3);
            Write((ulong) 0x0);
        }
    }
}