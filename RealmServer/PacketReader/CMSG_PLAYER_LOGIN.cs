using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_PLAYER_LOGIN represents a packet sent by the client when it tries to login a character.
    /// </summary>
    public sealed class CMSG_PLAYER_LOGIN : Common.Network.PacketReader
    {
        public CMSG_PLAYER_LOGIN(byte[] data) : base(data)
        {
            Id = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PLAYER_LOGIN] Id: {Id}");
#endif
        }

        public ulong Id { get; }
    }
}