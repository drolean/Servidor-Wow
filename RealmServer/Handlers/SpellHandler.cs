using System;
using Common.Globals;
using Common.Helpers;
using Common.Network;
using RealmServer.Game;
using RealmServer.Game.Entitys;

namespace RealmServer.Handlers
{
    enum SpellCastFlags
    {
        CAST_FLAG_NONE = 0x00000000,
        CAST_FLAG_HIDDEN_COMBATLOG = 0x00000001,               // hide in combat log?
        CAST_FLAG_UNKNOWN2 = 0x00000002,
        CAST_FLAG_UNKNOWN3 = 0x00000004,
        CAST_FLAG_UNKNOWN4 = 0x00000008,
        CAST_FLAG_UNKNOWN5 = 0x00000010,
        CAST_FLAG_AMMO = 0x00000020,               // Projectiles visual
        CAST_FLAG_UNKNOWN7 = 0x00000040,               // !0x41 mask used to call CGTradeSkillInfo::DoRecast
        CAST_FLAG_UNKNOWN8 = 0x00000080,
        CAST_FLAG_UNKNOWN9 = 0x00000100
    }

    #region SMSG_SPELL_GO
    public sealed class SmsgSpellGo : PacketServer
    {
        private static int aba = 0;
        public SmsgSpellGo(PlayerEntity caster, PlayerEntity target, uint spellId) : base(RealmCMD.SMSG_SPELL_GO)
        {
            byte[] casterGuid = UpdateObject.GenerateGuidBytes(caster.ObjectGuid.RawGuid);
            byte[] targetGuid = UpdateObject.GenerateGuidBytes(target.ObjectGuid.RawGuid);

            UpdateObject.WriteBytes(this, casterGuid);
            UpdateObject.WriteBytes(this, targetGuid);
            Write(spellId);
            Write((UInt16) aba); //SpellCastFlags.CAST_FLAG_UNKNOWN9); // Cast Flags!?
            Write((Byte) 1); // Target Length
            Write(target.ObjectGuid.RawGuid);
            Write((Byte) 0); // End
            Write((UInt16) 2); // TARGET_FLAG_UNIT
            UpdateObject.WriteBytes(this, targetGuid); // Packed GUID
            Console.WriteLine($"vem aqui baragad = {aba}");
            aba++;
        }
    }
    #endregion

    #region SMSG_CAST_FAILED
    public sealed class SmsgCastFailed : PacketServer
    {
        public SmsgCastFailed(uint spellId) : base(RealmCMD.SMSG_CAST_FAILED)
        {
            Write(spellId);
        }
    }
    #endregion

    internal class SpellHandler
    {
        internal static void OnCastSpell(RealmServerSession session, PacketReader handler)
        {
            uint spellId = handler.ReadUInt32();
            string target = handler.ReadCString();

            Log.Print(LogType.Debug, $"CSMG_CAST_SPELL [spellID={spellId}] om = {target}");

            session.SendPacket(new SmsgSpellGo(session.Entity, session.Entity, spellId));

            session.SendPacket(new SmsgCastFailed(spellId));


            // Check if spell exists

            // Check if have spell

            // Get spell cooldown

            //  In duel disable
        }
    }
}
