using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_CREATURE_QUERY represents a packet sent by the client when it wants to retrieve creature information.
    /// </summary>
    public sealed class CMSG_CREATURE_QUERY : Common.Network.PacketReader
    {
        public int CreatureId;
        public ulong CreatureUid;

        public CMSG_CREATURE_QUERY(byte[] data) : base(data)
        {
            CreatureId = ReadInt32();
            CreatureUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CREATURE_QUERY] CreatureId: {CreatureId} CreatureUid: {CreatureUid}");
#endif
        }
    }
}