using System;
using RealmServer.Enums;

namespace RealmServer.PacketReader
{
    public sealed class MSG_MOVE_FALL_LAND : Common.Network.PacketReader
    {
        public MovementFlags MoveFlags { get; set; }
        public uint Time { get; set; }
        public float MapX { get; set; }
        public float MapY { get; set; }
        public float MapZ { get; set; }
        public float MapR { get; set; }

        public UInt64 TransportIdk { get; set; }
        public float TransportMapX { get; set; }
        public float TransportMapY { get; set; }
        public float TransportMapZ { get; set; }
        public float TransportMapR { get; set; }

        public float Swimming { get; set; }

        public Int32 Idk { get; set; }

        public MSG_MOVE_FALL_LAND(byte[] data) : base(data)
        {
            MoveFlags = (MovementFlags) ReadUInt32();
            Time = ReadUInt32();
            MapX = ReadSingle();
            MapY = ReadSingle();
            MapZ = ReadSingle();
            MapR = ReadSingle();

            switch (MoveFlags)
            {
                case MovementFlags.MoveflagOntransport:
                    TransportIdk = ReadUInt64();
                    TransportMapX = ReadSingle();
                    TransportMapY = ReadSingle();
                    TransportMapZ = ReadSingle();
                    TransportMapR = ReadSingle();
                    break;
                case MovementFlags.MoveflagSwimming:
                    Swimming = ReadSingle();
                    break;
            }

            Idk = ReadInt32();
        }
    }
}