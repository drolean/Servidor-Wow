using System;
using Common.Database.Dbc;
using RealmServer.Enums;
using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnTextEmote
    {
        public static void Handler(RealmServerSession session, CMSG_TEXT_EMOTE handler)
        {
            var checkEmote = EmotesTextReader.GetData(handler.Emote);
            Console.WriteLine(checkEmote);
            Console.WriteLine(checkEmote.EmoteName);

            if (checkEmote != null)
                session.Entity.SetUpdateField((int) UnitFields.UNIT_NPC_EMOTESTATE, checkEmote.EmoteId);
        }
    }
}
