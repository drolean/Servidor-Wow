using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_LOGIN_VERIFY_WORLD : Common.Network.PacketServer
    {
        public SMSG_LOGIN_VERIFY_WORLD(Characters character) : base(RealmEnums.SMSG_LOGIN_VERIFY_WORLD)
        {
            Write(character.SubMap.MapId);
            Write(character.SubMap.MapX);
            Write(character.SubMap.MapY);
            Write(character.SubMap.MapZ);
            Write(character.SubMap.MapO);
        }
    }
}