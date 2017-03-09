using System;
using System.Linq;
using Common.Globals;
using Common.Helpers;
using Common.Network;
using RealmServer.Game.Entitys;

namespace RealmServer.Handlers
{
    internal class CombatHandler
    {
        internal static void OnSetsHeathed(RealmServerSession session, PacketReader handler)
        {
            SHEATHE_SLOT sheathed = (SHEATHE_SLOT) handler.ReadInt32();
            SetSheath(session.Entity, sheathed);
        }

        internal static void SetSheath(PlayerEntity objCharacter, SHEATHE_SLOT state)
        {
            switch (state)
            {
                default:
                    Log.Print(LogType.Debug, $"Unhandled sheathe state [{state}]");
                    break;
            }
        }

        internal static void OnSetSelection(RealmServerSession session, PacketReader handler)
        {
            UInt64 guid = handler.ReadUInt64();

            if (guid == 0)
            {
                session.Target = null;
                return;
            }
            
            session.Target = session.Entity.KnownPlayers.FirstOrDefault(s => s.Character.Id == (int) guid)?.Character;
            session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_TARGET, guid);
        }

        internal static void OnAttackSwing(RealmServerSession session, PacketReader handler)
        {
            ulong guid = handler.ReadUInt64();

                PacketServer packet = new PacketServer(RealmCMD.SMSG_ATTACKSTART);
                packet.Write(session.Entity.ObjectGuid.RawGuid);
                packet.Write(session.Entity.ObjectGuid.RawGuid);
                session.SendPacket(packet);
        }

        internal static void OnAttackStop(RealmServerSession session, PacketReader handler)
        {

            PacketServer packet = new PacketServer(RealmCMD.SMSG_ATTACKSTOP);
            packet.Write(session.Entity.ObjectGuid.RawGuid);
            packet.Write(session.Entity.ObjectGuid.RawGuid);
            packet.Write((int) 0);
            packet.Write((byte) 0);
            session.SendPacket(packet);

            //SendAttackStop(client.Character.GUID, client.Character.TargetGUID, client)
            //client.Character.attackState.AttackStop()

            /*
            'AttackerGUID stopped attacking victimGUID
        Dim SMSG_ATTACKSTOP As New PacketClass(OPCODES.SMSG_ATTACKSTOP)
        SMSG_ATTACKSTOP.AddPackGUID(attackerGUID)
        SMSG_ATTACKSTOP.AddPackGUID(victimGUID)
        SMSG_ATTACKSTOP.AddInt32(0)
        SMSG_ATTACKSTOP.AddInt8(0)
        client.Character.SendToNearPlayers(SMSG_ATTACKSTOP)
                */
        }
    }
}