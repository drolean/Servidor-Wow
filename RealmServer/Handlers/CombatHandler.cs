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
            //If(packet.Data.Length - 1) < 9 Then Exit Sub

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

            session.Target = RealmServerSession.Sessions.FirstOrDefault(s => s.Character.Id == (int) guid)?.Character;
            session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_TARGET, session.Target);
            //session.Entity.SendCharacterUpdate();
        }
    }
}