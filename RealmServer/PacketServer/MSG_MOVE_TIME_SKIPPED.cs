using Common.Globals;
using RealmServer.PacketReader;

namespace RealmServer.PacketServer
{
    internal sealed class MSG_MOVE_TIME_SKIPPED : Common.Network.PacketServer
    {
        /// <summary>
        /// </summary>
        /// <param name="handler"></param>
        public MSG_MOVE_TIME_SKIPPED(CMSG_MOVE_TIME_SKIPPED handler) : base(RealmEnums.MSG_MOVE_TIME_SKIPPED)
        {
            Write(handler.Lag);
        }
    }
}
