using Common.Database.Tables;
using Common.Globals;
using RealmServer.Enums;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_FRIEND_STATUS : Common.Network.PacketServer
    {
        public SMSG_FRIEND_STATUS(FriendResults result, Characters friend = null) :
            base(RealmEnums.SMSG_FRIEND_STATUS)
        {
            var uid = friend?.Uid ?? 0;
            Write((byte) result);
            Write(uid);

            if (friend == null)
                return;

            switch (result)
            {
                case FriendResults.ADDED_OFFLINE:
                    WriteCString(friend.Name);
                    break;
                case FriendResults.ADDED_ONLINE:
                    Write((byte) FriendStatus.Online);
                    Write(friend.SubMap.MapZone);
                    Write((int) friend.Level);
                    Write((int) friend.Classe);
                    break;
            }
        }
    }
}