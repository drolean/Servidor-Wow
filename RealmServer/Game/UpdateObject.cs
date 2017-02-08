using System;
using System.Collections.Generic;
using System.IO;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using Common.Network;
using RealmServer.Game.Entitys;

namespace RealmServer.Game
{
    public class UpdateObject : PacketServer
    {
        public UpdateObject(List<byte[]> blocks, int hasTansport = 0) : base(RealmCMD.SMSG_UPDATE_OBJECT)
		{
            Write((uint)blocks.Count);
            Write((byte)hasTansport);
            blocks.ForEach(Write);
        }

        public static UpdateObject CreateOwnCharacterUpdate(Characters character, out PlayerEntity entity)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT_SELF);

            writer.WritePackedUInt64((ulong) character.Id);

            writer.Write((byte) TypeId.TypeidPlayer);

            const ObjectUpdateFlag updateFlags = ObjectUpdateFlag.UpdateflagAll |
                                                 ObjectUpdateFlag.UpdateflagHasPosition |
                                                 ObjectUpdateFlag.UpdateflagLiving |
                                                 ObjectUpdateFlag.UpdateflagSelf;

            writer.Write((byte) updateFlags);

            writer.Write((uint) MovementFlags.MoveflagNone);
            writer.Write((uint) Environment.TickCount);

            writer.Write(character.MapX);
            writer.Write(character.MapY);
            writer.Write(character.MapZ);
            writer.Write(character.MapO);

            writer.Write((float) 0);

            writer.Write(2.5f);
            writer.Write(7f * 10);
            writer.Write(4.5f);
            writer.Write(4.72f);
            writer.Write(2.5f);
            writer.Write(3.14f);

            writer.Write(0x1);

            entity = new PlayerEntity(character)
            {
                ObjectGuid = new ObjectGuid((ulong) character.Id),
                Guid       = (ulong) character.Id,
            };

            entity.WriteUpdateFields(writer);          

            return new UpdateObject(new List<byte[]> { ((MemoryStream) writer.BaseStream).ToArray() });
        }
    }

    public enum TypeId : byte
    {
        TypeidObject = 0,
        TypeidItem = 1,
        TypeidContainer = 2,
        TypeidUnit = 3,
        TypeidPlayer = 4,
        TypeidGameobject = 5,
        TypeidDynamicobject = 6,
        TypeidCorpse = 7
    }

    [Flags]
    public enum ObjectUpdateFlag : byte
    {
        UpdateflagNone = 0x0000,
        UpdateflagSelf = 0x0001,
        UpdateflagTransport = 0x0002,
        UpdateflagFullguid = 0x0004,
        UpdateflagHighguid = 0x0008,
        UpdateflagAll = 0x0010,
        UpdateflagLiving = 0x0020,
        UpdateflagHasPosition = 0x0040
    }

    [Flags]
    public enum MovementFlags
    {
        MoveflagNone = 0x00000000,
        MoveflagForward = 0x00000001,
        MoveflagBackward = 0x00000002,
        MoveflagStrafeLeft = 0x00000004,
        MoveflagStrafeRight = 0x00000008,
        MoveflagTurnLeft = 0x00000010,
        MoveflagTurnRight = 0x00000020,
        MoveflagPitchUp = 0x00000040,
        MoveflagPitchDown = 0x00000080,
        MoveflagWalkMode = 0x00000100,               // Walking

        MoveflagLevitating = 0x00000400,
        MoveflagRoot = 0x00000800,               // [-ZERO] is it really need and correct value
        MoveflagFalling = 0x00002000,
        MoveflagFallingfar = 0x00004000,
        MoveflagSwimming = 0x00200000,               // appears with fly flag also
        MoveflagAscending = 0x00400000,               // [-ZERO] is it really need and correct value
        MoveflagCanFly = 0x00800000,               // [-ZERO] is it really need and correct value
        MoveflagFlying = 0x01000000,               // [-ZERO] is it really need and correct value

        MoveflagOntransport = 0x02000000,               // Used for flying on some creatures
        MoveflagSplineElevation = 0x04000000,               // used for flight paths
        MoveflagSplineEnabled = 0x08000000,               // used for flight paths
        MoveflagWaterwalking = 0x10000000,               // prevent unit from falling through water
        MoveflagSafeFall = 0x20000000,               // active rogue safe fall spell (passive)
        MoveflagHover = 0x40000000
    }
}
