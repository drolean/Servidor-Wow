using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_UPDATE_ACCOUNT_DATA : Common.Network.PacketReader
    {
        public CMSG_UPDATE_ACCOUNT_DATA(byte[] data) : base(data)
        {
            Data = ReadUInt32();
            Size = ReadUInt32();
            Console.WriteLine(Data);

            if (Size <= 0) return;

            var accountData = ReadBytes((int)BaseStream.Length - (int)BaseStream.Position);
            var decompressed = ZLib.Decompress(accountData);

            Console.WriteLine(System.Text.Encoding.ASCII.GetString(decompressed));
        }

        public uint Data { get; }
        public uint Size { get; }
    }
}