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
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte)ObjectUpdateType.UPDATETYPE_VALUES);

            byte[] guidBytes = GenerateGuidBytes(player.ObjectGuid.RawGuid);
            WriteBytes(writer, guidBytes, guidBytes.Length);

            player.WriteUpdateFields(writer);

            return new SMSG_UPDATE_OBJECT(new List<byte[]> { (writer.BaseStream as MemoryStream)?.ToArray() });
        }


        internal static void WriteBytes(BinaryWriter writer, byte[] data, int count = 0)
        {
            if (count == 0)
                writer.Write(data);
            else
                writer.Write(data, 0, count);
        }

        internal static byte[] GenerateGuidBytes(ulong id)
        {
            byte[] packedGuid = new byte[9];
            byte length = 1;

            for (byte i = 0; id != 0; i++)
            {
                if ((id & 0xFF) != 0)
                {
                    packedGuid[0] |= (byte)(1 << i);
                    packedGuid[length] = (byte)(id & 0xFF);
                    ++length;
                }

                id >>= 8;
            }

            byte[] clippedArray = new byte[length];
            Array.Copy(packedGuid, clippedArray, length);

            return clippedArray;
        }

        public static SMSG_UPDATE_OBJECT CreateItem(SubInventory inventory, Characters character)
        {
            Log.Print(LogType.RealmServer, $"[{character.Name}] Bag: {inventory.Bag} Item: {inventory.Item} " +
                                           $"Stack: {inventory.StackCount} Flag: {inventory.Flags} Slot: {inventory.Slot}");

            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT);

            ItemEntity entity = new ItemEntity(inventory, character)
            {
                ObjectGuid = new ObjectGuid((UInt32)inventory.Item),
                Guid = (UInt32)inventory.Item
            };

            writer.WritePackedUInt64(entity.ObjectGuid.RawGuid);
            writer.Write((byte)TypeId.TypeidItem);

            ObjectUpdateFlag updateFlags = ObjectUpdateFlag.Transport |
                                           ObjectUpdateFlag.All |
                                           ObjectUpdateFlag.HasPosition;

            writer.Write((byte)updateFlags);

            writer.Write(0f);
            writer.Write(0f);
            writer.Write(0f);

            writer.Write((float)0);

            writer.Write((uint)0x1);
            writer.Write((uint)0);

            entity.WriteUpdateFields(writer);

            return new SMSG_UPDATE_OBJECT(new List<byte[]> { (writer.BaseStream as MemoryStream)?.ToArray() });
        }

        public static SMSG_UPDATE_OBJECT CreateOwnCharacterUpdate(Characters character, out PlayerEntity entity)
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
    }
}