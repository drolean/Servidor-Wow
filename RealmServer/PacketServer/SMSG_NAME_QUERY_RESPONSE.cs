using System.Text;
using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_NAME_QUERY_RESPONSE represents a message sent by the server as the result of a CMSG_NAME_QUERY packet.
    /// </summary>
    public sealed class SMSG_NAME_QUERY_RESPONSE : Common.Network.PacketServer
    {
        public SMSG_NAME_QUERY_RESPONSE(Characters character) : base(RealmEnums.SMSG_NAME_QUERY_RESPONSE)
        {
            Write(character.Uid);
            Write(Encoding.UTF8.GetBytes(character.Name + '\0'));
            Write((byte) 0); // realm name for cross realm BG usage
            Write((uint) character.Race);
            Write((uint) character.Gender);
            Write((uint) character.Classe);
        }
    }
}
