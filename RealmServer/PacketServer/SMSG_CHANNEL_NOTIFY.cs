using System.Text;
using Common.Globals;
using RealmServer.Enums;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_CHANNEL_NOTIFY : Common.Network.PacketServer
    {
        public SMSG_CHANNEL_NOTIFY(ChatChannelNotify type, ulong uid, string channelName) : base(RealmEnums
            .SMSG_CHANNEL_NOTIFY)
        {
            Write((byte) type);
            Write(Encoding.UTF8.GetBytes(channelName + '\0'));
            Write(uid);
        }
    }
}