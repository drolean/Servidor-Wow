using System;
using Common.Helpers;
using Common.Network;

namespace RealmServer.Handlers
{
    internal class SpellHandler
    {
        internal static void OnCastSpell(RealmServerSession session, PacketReader handler)
        {
            uint spellId = handler.ReadUInt32();
            string target = handler.ReadCString();

            Log.Print(LogType.Debug, $"CSMG_CAST_SPELL [spellID={spellId}]");

            // Check if spell exists

            // Check if have spell

            // Get spell cooldown

            //  In duel disable
        }
    }
}
