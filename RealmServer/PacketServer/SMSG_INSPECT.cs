using Common.Globals;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_INSPECT : Common.Network.PacketServer
    {
        /// <summary>
        /// </summary>
        /// <param name="playerUid"></param>
        public SMSG_INSPECT(ulong playerUid) : base(RealmEnums.SMSG_INSPECT)
        {
            Write(playerUid);
        }
    }
}