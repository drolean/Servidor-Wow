using System.Linq;
using Common.Database.Tables;
using Common.Globals;
using RealmServer.World.Managers;

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
                var status = PlayerManager.Players.Any(p => p.Character.Uid == friend.Uid);
                Write(friend.Uid);

                Write((byte) (status ? 1 : 0));
                Write(friendChar.SubMap.MapZone); // uint32   = area
                Write((int) friendChar.Level); // uint32   = level
                Write((int) friendChar.Classe); // uint32   = class
            }
        }
    }
}
