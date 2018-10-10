using System;
using Common.Globals;
using Common.Helpers;
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
                if (MainProgram.RealmServerDatabase.GetCharacters(session.Users.username).Count >= Config.Instance.LimitCharacterRealm)
                {
                    session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.CHAR_CREATE_SERVER_LIMIT));
                    return;
                }

                // check if name in use
                if (MainProgram.RealmServerDatabase.FindCharacaterByName(handler.Name) != null)
                {
                    session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.CHAR_CREATE_NAME_IN_USE));
                    return;
                }

                //MainProgram.RealmServerDatabase.CreateChar(handler, session.Users);

                session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.CHAR_CREATE_SUCCESS));
            }
            catch (Exception)
            {
                session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.CHAR_CREATE_ERROR));
            }
        }
    }
}