using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_AMMO : Common.Network.PacketReader
    {
        public uint AmmoId;

        public CMSG_SET_AMMO(byte[] data) : base(data)
        {
            AmmoId = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_AMMO] AmmoId: {AmmoId}");
#endif
        }
    }
}
