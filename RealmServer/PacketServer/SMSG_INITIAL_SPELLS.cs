using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_INITIAL_SPELLS represents a message sent by the server to indicate the spells known by the character.
    /// </summary>
    internal sealed class SMSG_INITIAL_SPELLS : Common.Network.PacketServer
    {
        public SMSG_INITIAL_SPELLS(Characters character) : base(RealmEnums.SMSG_INITIAL_SPELLS)
        {
            Write((byte) 0);
            Write((ushort) character.SubSpells.Count);

            ushort slot = 1;
            foreach (var spell in character.SubSpells)
            {
                Write((ushort) spell.Spell);
                Write(slot++);
            }

            Write((ushort) 0);
        }
    }
}