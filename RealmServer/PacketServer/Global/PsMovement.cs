using System;
using System.IO;
using Common.Globals;
using RealmServer.PacketReader;

namespace RealmServer.PacketServer.Global
{
    internal sealed class PsMovement : Common.Network.PacketServer
    {
        public PsMovement(RealmServerSession session, MSG_MOVE_FALL_LAND handler, RealmEnums opcode) : base(opcode)
        {
            byte[] packedGuid = GenerateGuidBytes(session.Character.Uid);
            WriteBytes(this, packedGuid);
            WriteBytes(this, (handler.BaseStream as MemoryStream)?.ToArray());

            // We then overwrite the original moveTime (sent from the client) with ours
            ((MemoryStream)BaseStream).Position = 4 + packedGuid.Length;
            WriteBytes(this, BitConverter.GetBytes((uint)Environment.TickCount));
        }
    }
}