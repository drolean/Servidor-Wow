using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_PING represents a packet sent by the client to ping the server.
    /// </summary>
    public sealed class CMSG_PING : Common.Network.PacketReader
    {
        /// <summary>
        ///     This packet is sent by the player to indicate he/she is changing
        ///     her state. For example when the player switches from sitting to
        ///     lying position.
        /// </summary>
        /// <param name="data"></param>
        public CMSG_PING(byte[] data) : base(data)
        {
            Ping = ReadUInt32();
            Latency = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PING] Ping: {Ping} Latency: {Latency}");
#endif
        }

        public uint Ping { get; }
        public uint Latency { get; }
    }
}
