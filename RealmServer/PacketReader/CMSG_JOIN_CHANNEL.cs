using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Handles an incoming channel join request
    /// </summary>
    public class CMSG_JOIN_CHANNEL : Common.Network.PacketReader
    {
        public CMSG_JOIN_CHANNEL(byte[] data) : base(data)
        {
            Channel = ReadCString();
            Password = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_JOIN_CHANNEL] Channel: {Channel} Password: {Password}");
#endif
        }

        public string Channel { get; }
        public string Password { get; }
    }
}