using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class MSG_RANDOM_ROLL : Common.Network.PacketReader
    {
        public int Min;
        public int Max;

        public MSG_RANDOM_ROLL(byte[] data) : base(data)
        {
            Min = ReadInt32();
            Max = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[MSG_RANDOM_ROLL] Min: {Min} Max: {Max}");
#endif
        }
    }
}