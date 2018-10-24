using System;
using System.Collections.Generic;
using System.IO;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using RealmServer.Enums;
using RealmServer.World;
using RealmServer.World.Enititys;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_UPDATE_OBJECT : Common.Network.PacketServer
    {
        private SMSG_UPDATE_OBJECT(List<byte[]> blocks, int hasTansport = 0) : base(RealmEnums.SMSG_UPDATE_OBJECT)
        {
            Write((uint) blocks.Count);
            Write((byte) hasTansport);
            blocks.ForEach(Write);
        }

        internal static SMSG_UPDATE_OBJECT UpdateValues(PlayerEntity player)
        {
            var writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte) ObjectUpdateType.UPDATETYPE_VALUES);

            var guidBytes = GenerateGuidBytes(player.ObjectGuid.RawGuid);
            WriteBytes(writer, guidBytes, guidBytes.Length);

            player.WriteUpdateFields(writer);

            return new SMSG_UPDATE_OBJECT(new List<byte[]> {(writer.BaseStream as MemoryStream)?.ToArray()});
        }

        internal static SMSG_UPDATE_OBJECT CreateItem(SubInventory inventory, PlayerEntity session)
        {
            var writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte) ObjectUpdateType.UPDATETYPE_CREATE_OBJECT);

            var entity = new ItemEntity(inventory, session)
            {
                ObjectGuid = new ObjectGuid((ulong) inventory.Item),
                Guid = (ulong) inventory.Item
            };

            writer.WritePackedUInt64(entity.ObjectGuid.RawGuid);
            writer.Write((byte) TypeId.TypeidItem);

            var updateFlags = ObjectUpdateFlag.Transport |
                              ObjectUpdateFlag.All |
                              ObjectUpdateFlag.HasPosition;

            writer.Write((byte) updateFlags);

            writer.Write(0f);
            writer.Write(0f);
            writer.Write(0f);

            writer.Write((float) 0);

            writer.Write((uint) 1);
            writer.Write((uint) 0);

            entity.WriteUpdateFields(writer);

            return new SMSG_UPDATE_OBJECT(new List<byte[]> {(writer.BaseStream as MemoryStream)?.ToArray()});
        }

        internal static SMSG_UPDATE_OBJECT CreateOutOfRangeUpdate(List<ObjectEntity> despawnPlayer)
        {
            var writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte) ObjectUpdateType.UPDATETYPE_OUT_OF_RANGE_OBJECTS);
            writer.Write((uint) despawnPlayer.Count);

            foreach (var entity in despawnPlayer) writer.WritePackedUInt64(entity.ObjectGuid.RawGuid);

            return new SMSG_UPDATE_OBJECT(new List<byte[]> {((MemoryStream) writer.BaseStream).ToArray()});
        }

        internal static SMSG_UPDATE_OBJECT CreateOwnCharacterUpdate(Characters character, out PlayerEntity entity)
        {
            var writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte) ObjectUpdateType.UPDATETYPE_CREATE_OBJECT_SELF);

            writer.WritePackedUInt64(character.Uid);

            writer.Write((byte) TypeId.TypeidPlayer);

            const ObjectUpdateFlag updateFlags = ObjectUpdateFlag.All |
                                                 ObjectUpdateFlag.HasPosition |
                                                 ObjectUpdateFlag.Living |
                                                 ObjectUpdateFlag.Self;

            writer.Write((byte) updateFlags);

            writer.Write((uint) MovementFlags.None);
            writer.Write((uint) Environment.TickCount);

            writer.Write(character.SubMap.MapX);
            writer.Write(character.SubMap.MapY);
            writer.Write(character.SubMap.MapZ);
            writer.Write(character.SubMap.MapO);

            writer.Write((float) 0);

            writer.Write(2.5f); // WalkSpeed
            writer.Write(7f * 1); // RunSpeed
            writer.Write(2.5f); // Backwards WalkSpeed
            writer.Write(4.7222f); // SwimSpeed
            writer.Write(2.5f); // Backwards SwimSpeed
            writer.Write(3.14f); // TurnSpeed

            writer.Write(0x1);

            entity = new PlayerEntity(character)
            {
                ObjectGuid = new ObjectGuid(character.Uid),
                Guid = character.Uid
            };

            entity.WriteUpdateFields(writer);

            return new SMSG_UPDATE_OBJECT(new List<byte[]> {((MemoryStream) writer.BaseStream).ToArray()});
        }

        internal static SMSG_UPDATE_OBJECT CreateUnit(SpawnCreatures creature)
        {
            var writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte) ObjectUpdateType.UPDATETYPE_CREATE_OBJECT);

            var entity = new UnitEntity(creature)
            {
                ObjectGuid = new ObjectGuid(creature.Uid),
                Guid = creature.Uid
            };

            writer.WritePackedUInt64(entity.ObjectGuid.RawGuid);

            writer.Write((byte) TypeId.TypeidUnit);

            var updateFlags = ObjectUpdateFlag.All |
                              ObjectUpdateFlag.Living |
                              ObjectUpdateFlag.HasPosition;

            writer.Write((byte) updateFlags);
            writer.Write((uint) MovementFlags.None); //MovementFlags
            writer.Write((uint) Environment.TickCount); // Time

            // Position
            writer.Write(creature.SubMap.MapX);
            writer.Write(creature.SubMap.MapY);
            writer.Write(creature.SubMap.MapZ);
            writer.Write(creature.SubMap.MapO);

            // Movement speeds
            writer.Write((float) 0); // ????

            writer.Write(2.5f); // WalkSpeed
            writer.Write(7f * 1); // RunSpeed
            writer.Write(4.5f); // MOVE_RUN_BACK
            writer.Write(4.72f); // MOVE_SWIM
            writer.Write(2.5f); // MOVE_SWIM_BACK
            writer.Write(3.14f); // MOVE_TURN_RATE

            writer.Write(0x1); // Unkown...

            entity.WriteUpdateFields(writer);

            return new SMSG_UPDATE_OBJECT(new List<byte[]> {(writer.BaseStream as MemoryStream)?.ToArray()}, 1);
        }

        internal static SMSG_UPDATE_OBJECT CreateCharacterUpdate(Characters character)
        {
            var writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte) ObjectUpdateType.UPDATETYPE_CREATE_OBJECT_SELF);

            var guidBytes = GenerateGuidBytes(character.Uid);
            WriteBytes(writer, guidBytes, guidBytes.Length);

            writer.Write((byte) TypeId.TypeidPlayer);

            var updateFlags = ObjectUpdateFlag.All |
                              ObjectUpdateFlag.HasPosition |
                              ObjectUpdateFlag.Living;

            writer.Write((byte) updateFlags);

            writer.Write((uint) MovementFlags.None);
            writer.Write((uint) Environment.TickCount); // Time?

            // Position
            writer.Write(character.SubMap.MapX);
            writer.Write(character.SubMap.MapY);
            writer.Write(character.SubMap.MapZ);
            writer.Write(character.SubMap.MapO); // R

            // Movement speeds
            writer.Write((float) 0); // ????

            writer.Write(2.5f); // MOVE_WALK
            writer.Write(7f); // MOVE_RUN
            writer.Write(4.5f); // MOVE_RUN_BACK
            writer.Write(4.72f * 20); // MOVE_SWIM
            writer.Write(2.5f); // MOVE_SWIM_BACK
            writer.Write(3.14f); // MOVE_TURN_RATE

            writer.Write(0x1); // Unkown...

            var playerEntity = new PlayerEntity(character) {Guid = (uint) character.Uid};

            playerEntity.WriteUpdateFields(writer);

            return new SMSG_UPDATE_OBJECT(new List<byte[]> {(writer.BaseStream as MemoryStream)?.ToArray()});
        }
    }
}