using System.Net;
using Common.Helpers;

namespace AuthServer.PacketReader
{
    public sealed class CMD_AUTH_LOGON_CHALLENGE : Common.Network.PacketReader
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

        public CMD_AUTH_LOGON_CHALLENGE(byte[] data) : base(data)
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
            // Length <<<<
            Username = ReadString();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMD_AUTH_LOGON_CHALLENGE] OptCode: {OptCode} Error: {Error} GameName: {GameName} " +
                $"Version: {Version} Build: {Build} Platform: {Platform} OS: {OS} " +
                $"Country: {Country} TimeZone: {TimeZone} Ip: {Ip} Username: {Username}");
#endif
        }
    }
}