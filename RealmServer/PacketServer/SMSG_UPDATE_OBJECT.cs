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