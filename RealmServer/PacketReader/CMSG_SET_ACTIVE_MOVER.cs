using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Used to set active mover from client side.
    /// </summary>
    public sealed class CMSG_SET_ACTIVE_MOVER : Common.Network.PacketReader
    {
        public CMSG_SET_ACTIVE_MOVER(byte[] data) : base(data)
        {
            PlayerUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_ACTIVE_MOVER] PlayerUid: {PlayerUid}");
#endif
        }

        public ulong PlayerUid { get; }
    }
}
