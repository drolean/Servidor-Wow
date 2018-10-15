using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     TODO
    ///     The information we have is all good and well, but we need to send a packet to get it to the client. For this, we
    ///     need to use two SMSGs; INIT_WORLD_STATES and UPDATE_WORLD_STATE.
    ///     SMSG_INIT_WORLD_STATES is used to set the INITIAL World State values.This needs to be used each time you're
    ///     creating a new World State.
    ///     It's usually 18 bytes per packet.
    ///     Structure
    ///     uint32 MapID (Must match WorldStateUI.dbc)
    ///     uint32 ZoneID(Must match WorldStateUI.dbc)
    ///     uint32 0
    ///     uint16 1
    ///     uint32 WorldStateID
    ///     uint32 Value(Must match WorldStateUI.dbc)
    ///     SMSG_UPDATE_WORLD_STATE
    ///     It's usually 8 bytes per packet.
    ///     Structure
    ///     uint32 WorldStateID(Must match WorldStateUI.dbc)
    ///     uint32 Value
    ///     https://www.ownedcore.com/forums/world-of-warcraft/world-of-warcraft-emulator-servers/wow-emu-questions-requests/327009-making-capturable-pvp-zones.html
    /// </summary>
    public sealed class SMSG_INIT_WORLD_STATES : Common.Network.PacketServer
    {
        /// <summary>
        ///     TODO
        /// </summary>
        /// <param name="character"></param>
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