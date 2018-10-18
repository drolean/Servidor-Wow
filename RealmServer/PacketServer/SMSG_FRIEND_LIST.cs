using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_FRIEND_LIST : Common.Network.PacketServer
    {
        public SMSG_FRIEND_LIST(Characters character) : base(RealmEnums.SMSG_FRIEND_LIST)
        {
            Write((byte) character.SubFriends.Count);

            foreach (var friend in character.SubFriends)
            {
                var friendChar = Database.Characters.FindCharacaterByUid(friend.Uid);
                Write(friend.Uid);

                /*
                FRIEND_STATUS_OFFLINE = 0,
                FRIEND_STATUS_ONLINE = 1,
                FRIEND_STATUS_AFK = 2,
                FRIEND_STATUS_UNK3 = 3,
                FRIEND_STATUS_DND = 4
                 */
                Write((byte) 0);
                Write((uint) friendChar.SubMap.MapZone); // uint32   = area
                Write((uint) friendChar.Level); // uint32   = level
                Write((uint) friendChar.Classe); // uint32   = class
            }
        }
    }
}