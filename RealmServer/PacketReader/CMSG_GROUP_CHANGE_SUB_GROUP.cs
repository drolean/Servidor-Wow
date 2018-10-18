using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_GROUP_CHANGE_SUB_GROUP : Common.Network.PacketReader
    {
        public string TargetName;
        public byte TargetGroupId;

        public CMSG_GROUP_CHANGE_SUB_GROUP(byte[] data) : base(data)
        {
            TargetName = ReadCString();
            TargetGroupId = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GROUP_CHANGE_SUB_GROUP] TargetName: {TargetName} TargetGroupId: {TargetGroupId}");
#endif
        }
    }
}