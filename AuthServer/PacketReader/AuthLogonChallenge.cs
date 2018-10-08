using System.Net;

namespace AuthServer.PacketReader
{
    public sealed class AuthLogonChallenge : Common.Network.PacketReader
    {
        public ushort Build;
        public string Country;
        public byte Error;

        public string GameName;
        public IPAddress Ip;
        public byte OptCode;
        public string OS;

        public string Platform;
        public ushort Size;

        public uint TimeZone;
        public string Username;
        public string Version;

        public AuthLogonChallenge(byte[] data) : base(data)
        {
            OptCode = ReadByte();
            Error = ReadByte();
            Size = ReadUInt16();

            GameName = ReadStringReversed(4);
            Version = ReadByte().ToString() + '.' + ReadByte() + '.' + ReadByte();

            Build = ReadUInt16();
            Platform = ReadStringReversed(4);
            OS = ReadStringReversed(4);
            Country = ReadStringReversed(4);

            TimeZone = ReadUInt32();
            Ip = ReadIpAddress();
            Username = ReadPascalString(1);
        }
    }
}