using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BUG : Common.Network.PacketReader
    {
        public string Content;
        public uint ContentLen;
        public uint Suggestion;
        public string Type;
        public uint TypeLen;

        public CMSG_BUG(byte[] data) : base(data)
        {
            Suggestion = ReadUInt32();
            ContentLen = ReadUInt32();
            Content = ReadString();
            TypeLen = ReadUInt32();
            Type = ReadString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_BUG] Suggestion: {Suggestion} ContentLen: {ContentLen} " +
                                     $"Content: {Content} TypeLen: {TypeLen} Type: {Type}");
#endif
        }
    }
}