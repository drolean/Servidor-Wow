using System;
using System.Collections.Generic;
using System.IO;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using RealmServer.Enums;
using RealmServer.World.Enititys;

namespace RealmServer.World
{
    public sealed class UpdateObject : Common.Network.PacketServer
    {
        private UpdateObject(List<byte[]> blocks, int hasTansport = 0) : base(RealmEnums.SMSG_UPDATE_OBJECT)
        {
            Write((uint) blocks.Count);
            Write((byte) hasTansport);
            blocks.ForEach(Write);
        }

        public static UpdateObject CreateItem(SubInventory inventory, Characters character)
        {
            Log.Print(LogType.RealmServer, $"[{character.Name}] Bag: {inventory.Bag} Item: {inventory.Item} " +
                                           $"Stack: {inventory.StackCount} Flag: {inventory.Flags} Slot: {inventory.Slot}");

            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte) ObjectUpdateType.UPDATETYPE_CREATE_OBJECT);

            ItemEntity entity = new ItemEntity(inventory, character)
            {
                ObjectGuid = new ObjectGuid((UInt32) inventory.Item),
                Guid = (UInt32) inventory.Item
            };

            writer.WritePackedUInt64(entity.ObjectGuid.RawGuid);
            writer.Write((byte) TypeId.TypeidItem);

            ObjectUpdateFlag updateFlags = ObjectUpdateFlag.Transport |
                                           ObjectUpdateFlag.All |
                                           ObjectUpdateFlag.HasPosition;

            writer.Write((byte) updateFlags);

            writer.Write(0f);
            writer.Write(0f);
            writer.Write(0f);

            writer.Write((float) 0);

            writer.Write((uint) 0x1);
            writer.Write((uint) 0);

            entity.WriteUpdateFields(writer);

            return new UpdateObject(new List<byte[]> {(writer.BaseStream as MemoryStream)?.ToArray()});
        }
    }
}