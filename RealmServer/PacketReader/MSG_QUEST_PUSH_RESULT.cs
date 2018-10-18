using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class MSG_QUEST_PUSH_RESULT : Common.Network.PacketReader
    {
        public UInt64 Uid;
        public byte Result;

        public MSG_QUEST_PUSH_RESULT(byte[] data) : base(data)
        {
            Uid = ReadUInt64();
            Result = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[MSG_QUEST_PUSH_RESULT] Uid: {Uid} Result: {Result}");
#endif
        }
    }
}