using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     Response to ping.
    /// </summary>
    public sealed class SMSG_PONG : Common.Network.PacketServer
    {
        public SMSG_PONG(uint ping) : base(RealmEnums.SMSG_PONG)
        {
            Write((ulong) ping);
        }
    }
}