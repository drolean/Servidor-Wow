using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_UNLEARN_SKILL : Common.Network.PacketReader
    {
        public uint SkillId;
        public int Max;

        public CMSG_UNLEARN_SKILL(byte[] data) : base(data)
        {
            SkillId = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_UNLEARN_SKILL] SkillId: {SkillId}");
#endif
        }
    }
}