using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_GUILD_QUERY represents a packet sent by the client when it wants to retrieve guild information.
    /// </summary>
    public sealed class CMSG_GUILD_QUERY : Common.Network.PacketReader
    {
        public uint GuildId;

        public CMSG_GUILD_QUERY(byte[] data) : base(data)
        {
            GuildId = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GUILD_QUERY] GuildId: {GuildId}");
#endif
        }
    }
}