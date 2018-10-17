using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_TEXT_EMOTE : Common.Network.PacketReader
    {
        public int Emote;
        public int Unk;
        public ulong Uid;

        public CMSG_TEXT_EMOTE(byte[] data) : base(data)
        {
            Emote = ReadInt32();
            Unk = ReadInt32();
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_TEXT_EMOTE] Emote: {Emote} Unk: {Unk} Uid: {Uid}");
#endif
        }
    }
}