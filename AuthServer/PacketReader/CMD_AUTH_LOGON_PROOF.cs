using Common.Helpers;

namespace AuthServer.PacketReader
{
    public sealed class CMD_AUTH_LOGON_PROOF : Common.Network.PacketReader
    {
        public CMD_AUTH_LOGON_PROOF(byte[] data) : base(data)
        {
            Cmd = ReadByte();
            A = ReadBytes(32);
            M1 = ReadBytes(20);
            CrcHash = ReadBytes(20);
            NumberOfKey = ReadByte();
            SecurityFlag = ReadByte(); // 0x00-0x04

#if DEBUG
            Log.Print(LogType.Debug, $"[CMD_AUTH_LOGON_PROOF] Cmd: {Cmd} A: {A.Length} M1: {M1.Length} " +
                                     $"CrcHash: {CrcHash.Length} NumberOfKey: {NumberOfKey} SecurityFlag: {SecurityFlag}");
#endif
        }

        public byte Cmd { get; }
        public byte[] A { get; }
        public byte[] M1 { get; }
        public byte[] CrcHash { get; }
        public byte NumberOfKey { get; }
        public byte SecurityFlag { get; }
    }
}