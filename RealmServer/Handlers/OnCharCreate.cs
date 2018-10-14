using System;
using Common.Globals;
using Common.Helpers;
using RealmServer.Database;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnCharCreate
    {
        internal static void Handler(RealmServerSession session, CMSG_CHAR_CREATE handler)
        {
            session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.CHAR_CREATE_SUCCESS));

            try
            {
                // If limit character reached
                if (Characters.GetCharacters(session.Users).Count >= Config.Instance.LimitCharacterRealm)
                {
                    session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.CHAR_CREATE_SERVER_LIMIT));
                    return;
                }

                // check if name in use

                Characters.Create(handler, session.Users);

                session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.CHAR_CREATE_SUCCESS));
            }
            catch (Exception)
            {
                session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.CHAR_CREATE_ERROR));
            }
        }
    }
}