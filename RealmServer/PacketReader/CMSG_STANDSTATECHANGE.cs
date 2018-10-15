namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Handles an incoming stand state change request.
    /// </summary>
    public sealed class CMSG_STANDSTATECHANGE : Common.Network.PacketReader
    {
        public byte StandState;

        public CMSG_STANDSTATECHANGE(byte[] data) : base(data)
        {
            StandState = ReadByte();
        }
    }
}