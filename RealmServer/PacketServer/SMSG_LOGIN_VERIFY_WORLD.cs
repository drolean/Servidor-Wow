using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_LOGIN_VERIFY_WORLD represents a message sent by the server whenever a CMSG_PLAYER_LOGIN message is sent.
    /// </summary>
    public sealed class SMSG_LOGIN_VERIFY_WORLD : Common.Network.PacketServer
    {
        /// <summary>
        ///     Sends SMSG_LOGIN_VERIFY_WORLD (first ingame packet, sends char-location: Seems unnecessary?).
        /// </summary>
        /// <param name="character"></param>
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
