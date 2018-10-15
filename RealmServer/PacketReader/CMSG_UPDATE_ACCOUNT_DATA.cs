using System.Text;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_UPDATE_ACCOUNT_DATA : Common.Network.PacketReader
    {
        public CMSG_UPDATE_ACCOUNT_DATA(byte[] data) : base(data)
        {
            Type = ReadUInt32();
            Size = ReadUInt32();

            if (Size <= 0) return;

            var accountData = ReadBytes((int) BaseStream.Length - (int) BaseStream.Position);
            var decompressed = ZLib.Decompress(accountData);

            Data = Encoding.ASCII.GetString(decompressed);
        }

        public uint Type { get; }
        public uint Size { get; }
        public string Data { get; }
    }
}