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
                case MovementFlags.MoveflagNone:
                    break;
                case MovementFlags.MoveflagForward:
                    break;
                case MovementFlags.MoveflagBackward:
                    break;
                case MovementFlags.MoveflagStrafeLeft:
                    break;
                case MovementFlags.MoveflagStrafeRight:
                    break;
                case MovementFlags.MoveflagTurnLeft:
                    break;
                case MovementFlags.MoveflagTurnRight:
                    break;
                case MovementFlags.MoveflagPitchUp:
                    break;
                case MovementFlags.MoveflagPitchDown:
                    break;
                case MovementFlags.MoveflagWalkMode:
                    break;
                case MovementFlags.MoveflagLevitating:
                    break;
                case MovementFlags.MoveflagRoot:
                    break;
                case MovementFlags.MoveflagFalling:
                    break;
                case MovementFlags.MoveflagFallingfar:
                    break;
                case MovementFlags.MoveflagAscending:
                    break;
                case MovementFlags.MoveflagCanFly:
                    break;
                case MovementFlags.MoveflagFlying:
                    break;
                case MovementFlags.MoveflagSplineElevation:
                    break;
                case MovementFlags.MoveflagSplineEnabled:
                    break;
                case MovementFlags.MoveflagWaterwalking:
                    break;
                case MovementFlags.MoveflagSafeFall:
                    break;
                case MovementFlags.MoveflagHover:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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