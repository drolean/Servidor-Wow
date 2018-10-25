using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_PONG represents a message sent by the server to a client ping request.
    /// </summary>
    public sealed class SMSG_PONG : Common.Network.PacketServer
    {
        public SMSG_PONG(uint ping) : base(RealmEnums.SMSG_PONG)
        {
            Write((ulong) ping);
        }
    }
}