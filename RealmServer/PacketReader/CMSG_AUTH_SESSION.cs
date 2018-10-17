using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_AUTH_SESSION represents a packet sent by the client after the server has sent the challenge.
    /// </summary>
    public sealed class CMSG_AUTH_SESSION : Common.Network.PacketReader
    {
        public int ClientAddOnsSize;
        public byte[] ClientHash;

        public CMSG_AUTH_SESSION(byte[] data) : base(data)
        {
            ClientVersion = ReadInt32();
            ClientSessionId = ReadInt32();
            ClientAccount = ReadCString();
            ClientSeed = ReadInt32();
            ClientHash = ReadBytes(20);
            ClientAddOnsSize = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_AUTH_SESSION] ClientVersion: {ClientVersion} " +
                                     $"ClientSessionId: {ClientSessionId}: ClientAccount: {ClientAccount} " +
                                     $"ClientSeed: {ClientSeed} ClientHash: {ClientHash} ClientAddOnsSize: {ClientAddOnsSize}");
#endif
        }

        public int ClientVersion { get; }
        public int ClientSessionId { get; }
        public string ClientAccount { get; }
        public int ClientSeed { get; }
    }
}