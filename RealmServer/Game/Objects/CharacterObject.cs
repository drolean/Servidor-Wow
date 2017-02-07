using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using Common.Network;

namespace RealmServer.Game.Objects
{
    internal class CharacterObject : PacketServer
    {
        public CharacterObject(List<byte[]> blocks) : base(RealmCMD.SMSG_UPDATE_OBJECT)
        {
            Write((uint)blocks.Count);
            Write((byte)0);
            // If character is on a transport, create the transport right here

            blocks.ForEach(Write);
        }

        internal static CharacterObject CreateObjectSelf(Characters character)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            // Mover isso para variavel da funcao [int8]
            writer.Write((byte) ObjectUpdateType.UPDATETYPE_CREATE_OBJECT_SELF);
            // Aqui vai o ID  do inferno
            writer.Write((ushort) 257); // 257

            writer.Write((byte)ObjectTypeID.TYPEID_PLAYER);

            ObjectFlags updateFlags = ObjectFlags.UPDATEFLAG_ALL |
                                      ObjectFlags.UPDATEFLAG_HAS_POSITION |
                                      ObjectFlags.UPDATEFLAG_LIVING |
                                      ObjectFlags.UPDATEFLAG_SELF;
            Console.WriteLine(updateFlags);
            writer.Write((byte) updateFlags);

            writer.Write((uint) MovementFlags.MOVEFLAG_NONE);
            writer.Write((uint) Environment.TickCount); // Time?

            // Position
            writer.Write(character.MapX);
            writer.Write(character.MapY);
            writer.Write(character.MapZ);
            writer.Write(character.MapO); // R

            // Movement speeds
            writer.Write((float) 0); // ????
            /*
            packet.AddInt32(0) 'Unk
            packet.AddInt32(0) 'Unk
            packet.AddInt32(0) 'AttackCycle?
            packet.AddInt32(0) 'TimeID?
            packet.AddInt32(0) 'VictimGUID?
            */
            writer.Write(2.5f);     // MOVE_WALK
            writer.Write(7f * 10);  // MOVE_RUN
            writer.Write(4.5f);     // MOVE_RUN_BACK
            writer.Write(4.72f);    // MOVE_SWIM
            writer.Write(2.5f);     // MOVE_SWIM_BACK
            writer.Write(3.14f);    // MOVE_TURN_RATE

            writer.Write(0x1); // Unkown...

            writer.Write(StringToByteArray("29 15 00 40 54 1D C0 00 00 00 00 00 80 20 00 00 C0 D9 04 C2 4F 38 19 00 00 06 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 E0 B6 6D DB B6 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 6C 80 00 00 00 00 00 00 80 00 40 00 00 80 3F 00 00 00 00 20 00 00 00 00 00 00 01 00 00 00 19 00 00 00 CD CC AC 3F 54 00 00 00 64 00 00 00 54 00 00 00 E8 03 00 00 64 00 00 00 01 00 00 00 06 00 00 00 06 01 00 01 08 00 00 00 99 09 00 00 09 00 00 00 01 00 00 00 D0 07 00 00 D0 07 00 00 D0 07 00 00 3B 00 00 00 3B 00 00 00 25 49 D2 40 25 49 F2 40 00 EE 11 00 00 00 80 3F 1C 00 00 00 0F 00 00 00 18 00 00 00 0F 00 00 00 16 00 00 00 1E 00 00 00 0A 00 00 00 14 00 00 00 00 28 00 00 27 00 00 00 06 00 00 00 DC B6 ED 3F 6E DB 36 40 07 00 07 01 02 00 00 01 90 01 00 00 1A 00 00 00 01 00 01 00 2C 00 00 00 01 00 05 00 36 00 00 00 01 00 05 00 5F 00 00 00 01 00 05 00 6D 00 00 00 2C 01 2C 01 73 00 00 00 2C 01 2C 01 A0 00 00 00 01 00 05 00 A2 00 00 00 01 00 05 00 9D 01 00 00 01 00 01 00 9E 01 00 00 01 00 01 00 9F 01 00 00 01 00 01 00 B1 01 00 00 01 00 01 00 02 00 00 00 48 E1 9A 40 3E 0A 17 3F 3E 0A 17 3F CD CC 0C 3F 00 00 04 00 29 00 00 00 0A 00 00 00 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F FF FF FF FF"));

            return new CharacterObject(new List<byte[]> { ((MemoryStream) writer.BaseStream).ToArray() });
        }

        public static byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace(" ", "");

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }

    [Flags]
    public enum ObjectFlags : byte
    {
        UPDATEFLAG_NONE         = 0x0000,
        UPDATEFLAG_SELF         = 0x0001,
        UPDATEFLAG_TRANSPORT    = 0x0002,
        UPDATEFLAG_FULLGUID     = 0x0004,
        UPDATEFLAG_HIGHGUID     = 0x0008,
        UPDATEFLAG_ALL          = 0x0010,
        UPDATEFLAG_LIVING       = 0x0020,
        UPDATEFLAG_HAS_POSITION = 0x0040
    }

    public enum MovementFlags : int
    {
        MOVEFLAG_NONE         = 0x0,
        MOVEFLAG_FORWARD      = 0x1,
        MOVEFLAG_BACKWARD     = 0x2,
        MOVEFLAG_STRAFE_LEFT  = 0x4,
        MOVEFLAG_STRAFE_RIGHT = 0x8,
        MOVEFLAG_TURN_LEFT    = 0x10,
        MOVEFLAG_TURN_RIGHT   = 0x20,
        MOVEFLAG_PITCH_UP     = 0x40,
        MOVEFLAG_PITCH_DOWN   = 0x80,

        MOVEFLAG_WALK_MODE    = 0x100,         // Walking

        MOVEFLAG_LEVITATING   = 0x400,
        MOVEFLAG_ROOT         = 0x800,         // [-ZERO] is it really need and correct value
        MOVEFLAG_FALLING      = 0x2000,
        MOVEFLAG_FALLINGFAR   = 0x4000,
        MOVEFLAG_SWIMMING     = 0x200000,      // appears with fly flag also
        MOVEFLAG_ASCENDING    = 0x400000,      // [-ZERO] is it really need and correct value
        MOVEFLAG_CAN_FLY      = 0x800000,      // [-ZERO] is it really need and correct value
        MOVEFLAG_FLYING       = 0x1000000,     // [-ZERO] is it really need and correct value

        MOVEFLAG_ONTRANSPORT  = 0x2000000,     // Used for flying on some creatures
        MOVEFLAG_SPLINE_ELEVATION = 0x4000000, // used for flight paths
        MOVEFLAG_SPLINE_ENABLED   = 0x8000000, // used for flight paths
        MOVEFLAG_WATERWALKING = 0x10000000,    // prevent unit from falling through water
        MOVEFLAG_SAFE_FALL    = 0x20000000,    // active rogue safe fall spell (passive)
        MOVEFLAG_HOVER        = 0x40000000
    }
}
