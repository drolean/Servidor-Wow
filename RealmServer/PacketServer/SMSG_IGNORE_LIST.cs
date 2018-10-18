using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_IGNORE_LIST : Common.Network.PacketServer
    {
        public SMSG_IGNORE_LIST(Characters character) : base(RealmEnums.SMSG_IGNORE_LIST)
        {
            Write((byte) character.SubIgnoreds.Count);

            foreach (var ignored in character.SubIgnoreds) Write(ignored.Uid);
        }
    }
}