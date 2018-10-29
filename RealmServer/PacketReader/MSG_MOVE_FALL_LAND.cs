using System;
using RealmServer.Enums;

namespace RealmServer.PacketReader
{
    public sealed class MSG_MOVE_FALL_LAND : Common.Network.PacketReader
    {
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
                case MovementFlags.Ontransport:
                    TransportIdk = ReadUInt64();
                    TransportMapX = ReadSingle();
                    TransportMapY = ReadSingle();
                    TransportMapZ = ReadSingle();
                    TransportMapR = ReadSingle();
                    break;
                case MovementFlags.Swimming:
                    Swimming = ReadSingle();
                    break;
                default:
                    Console.WriteLine(MoveFlags);
                    break;
            }

            Idk = ReadInt32();
        }

        public MovementFlags MoveFlags { get; set; }
        public uint Time { get; set; }
        public float MapX { get; set; }
        public float MapY { get; set; }
        public float MapZ { get; set; }
        public float MapR { get; set; }

        public ulong TransportIdk { get; set; }
        public float TransportMapX { get; set; }
        public float TransportMapY { get; set; }
        public float TransportMapZ { get; set; }
        public float TransportMapR { get; set; }

        public float Swimming { get; set; }

        public int Idk { get; set; }
    }
}
