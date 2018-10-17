using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_EMOTE : Common.Network.PacketReader
    {
        public int Emote;

        public CMSG_EMOTE(byte[] data) : base(data)
        {
            Emote = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_EMOTE] Emote: {Emote}");
#endif
        }
    }
}