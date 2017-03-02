using System;
using System.IO;
using System.Threading.Tasks;
using Common.Globals;
using Common.Helpers;
using Common.Network;
using RealmServer.Game;

namespace RealmServer.Handlers
{
    #region MSG_MOVE_INFO
    public sealed class MsgMoveInfo : PacketReader
    {
        public MovementFlags MoveFlags { get; set; }
        public float MapX { get; set; }
        public float MapY { get; set; }
        public float MapZ { get; set; }
        public float MapR { get; set; }
        public uint Time { get; set; }

        public MsgMoveInfo(byte[] data) : base(data)
        {
            MoveFlags = (MovementFlags)ReadUInt32();
            Time = ReadUInt32();
            MapX = ReadSingle();
            MapY = ReadSingle();
            MapZ = ReadSingle();
            MapR = ReadSingle();
        }
    }
    #endregion

    #region CMSG_MOVE_TIME_SKIPPED
    public sealed class CmsgMoveTimeSkipped : PacketReader
    {
        public ulong Guid;

        public uint OutOfSyncDelay;

        public CmsgMoveTimeSkipped(byte[] data) : base(data)
        {
            Guid = ReadPackedGuid(this);
            OutOfSyncDelay = ReadUInt32();
        }

        public ulong ReadPackedGuid(BinaryReader reader)
        {
            byte mask = reader.ReadByte();

            if (mask == 0)
                return 0;

            ulong res = 0;

            int i = 0;
            while (i < 8)
            {
                if ((mask & 1 << i) != 0)
                    res += (ulong)reader.ReadByte() << (i * 8);

                i++;
            }

            return res;
        }
    }
    #endregion

    #region PS Movement handler
    internal sealed class PsMovement : PacketServer
    {
        public PsMovement(RealmServerSession session, MsgMoveInfo handler, RealmCMD opcode) : base(opcode)
        {
            byte[] packedGuid = UpdateObject.GenerateGuidBytes((ulong)session.Character.Id);
            UpdateObject.WriteBytes(this, packedGuid);
            UpdateObject.WriteBytes(this, (handler.BaseStream as MemoryStream)?.ToArray());

            // We then overwrite the original moveTime (sent from the client) with ours
            ((MemoryStream)BaseStream).Position = 4 + packedGuid.Length;
            UpdateObject.WriteBytes(this, BitConverter.GetBytes((uint)Environment.TickCount));
        }
    }
    #endregion

    internal class MovementHandler
    {
        internal static void OnMoveTimeSkipped(RealmServerSession session, CmsgMoveTimeSkipped handler)
        {
            session.OutOfSyncDelay = handler.OutOfSyncDelay;
        }

        internal static void OnMoveFallLand(RealmServerSession session, PacketReader handler)
        {
            // If FallTime > 1100 and not Dead

            // Caluclate fall damage

            // Prevent the fall damage to be more than your maximum health

            // Deal the damage

            // Initialize packet
        }

        internal static RealmServerRouter.ProcessLoginPacketCallbackTypes<MsgMoveInfo> GenerateResponse(RealmCMD code)
        {
            return async delegate (RealmServerSession session, MsgMoveInfo handler) { await TransmitMovement(session, handler, code); };
        }

        private static async Task TransmitMovement(RealmServerSession session, MsgMoveInfo handler, RealmCMD code)
        {
            session.Character.MapX = handler.MapX;
            session.Character.MapY = handler.MapY;
            session.Character.MapZ = handler.MapZ;
            
            await MainForm.Database.UpdateMovement(session.Character);
            /*
            // If character is falling below the world
            if (session.Character.MapZ < -500f)
                //AllGraveYards.GoToNearestGraveyard(client.Character, false, true);
                return;

            // Boarding transport

            // Unmount when boarding

            // Unboarding transport

            // Checking Explore System

            // Fire quest event to check for if this area is used in explore area quest

            // If character is moving
            // - Stop emotes if moving
            // - Stop casting

            // If character is turning

            // Movement time calculation

            // Send to nearby players
            RealmServerSession.Sessions.FindAll(s => s != session).ForEach(s => s.SendPacket(new PsMovement(session, handler, code)));

            // They may slow the movement down so let's do them after the packet is sent

            // Remove auras that requires you to not move

            // Remove auras that requires you to not turn

            /* MSG_MOVE_HEARTBEAT
                Check for out of continent - coordinates from WorldMapContinent.dbc
                Duel check
                Aggro range
                Creatures that are following you will have a more smooth movement
             */
        }

        internal static void OnMoveHeartbeat(RealmServerSession session, PacketReader handler)
        {

        }

        internal static void OnZoneUpdate(RealmServerSession session, PacketReader handler)
        {
            if (handler.BaseStream.Length < 8)
                return;

            uint zoneId = handler.ReadUInt32();
            session.Character.MapZone = (int) zoneId;

            // CheckZone

            // Send Weather
        }

        internal static void OnAreaTrigger(RealmServerSession session, PacketReader handler)
        {
            uint triggerId = handler.ReadUInt32();
            Log.Print(LogType.Debug, $"CMSG_AREATRIGGER [triggerID={triggerId}]");

            // Check if in combat?

            // Handling quest triggers

            // Handling tavern triggers

            // Handling teleport triggers

            // Handling all other scripted triggers
        }
    }
}