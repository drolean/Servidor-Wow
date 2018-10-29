using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PET_CANCEL_AURA : Common.Network.PacketReader
    {
        public uint AuraId;

        public CMSG_PET_CANCEL_AURA(byte[] data) : base(data)
        {
            AuraId = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PET_CANCEL_AURA] AuraId: {AuraId}");
#endif
        }
    }
}
