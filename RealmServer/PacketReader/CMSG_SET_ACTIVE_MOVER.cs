namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Used to set active mover from client side.
    /// </summary>
    public sealed class CMSG_SET_ACTIVE_MOVER : Common.Network.PacketReader
    {
        public CMSG_SET_ACTIVE_MOVER(byte[] data) : base(data)
        {
            Guid = ReadUInt64();
        }

        public ulong Guid { get; }
    }
}