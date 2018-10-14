using System;
using Common.Globals;
using RealmServer.Database;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnCharDelete
    {
        internal static void Handler(RealmServerSession session, CMSG_CHAR_DELETE handler)
        {
            // TODO: Waiting for transfer NOT DELETE
            // TODO: Guild Leader NOT DELETE
            try
            {
                Characters.DeleteCharacter(handler);
                session.SendPacket(new SMSG_CHAR_DELETE(LoginErrorCode.CHAR_DELETE_SUCCESS));
            }
            catch (Exception)
            {
                session.SendPacket(new SMSG_CHAR_DELETE(LoginErrorCode.CHAR_DELETE_FAILED));
            }
        }
    }
}