using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PET_SET_ACTION : Common.Network.PacketReader
    {
        public short ActionState;
        public int Position;
        public ushort SpellId;
        public ulong Uid;

        public CMSG_PET_SET_ACTION(byte[] data) : base(data)
        {
            Uid = ReadUInt64();
            Position = ReadInt32();
            SpellId = ReadUInt16();
            ActionState = ReadInt16();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_PET_SET_ACTION] Uid: {Uid} Position: {Position} SpellId: {SpellId} ActionState: {ActionState}");
#endif
        }
    }
}