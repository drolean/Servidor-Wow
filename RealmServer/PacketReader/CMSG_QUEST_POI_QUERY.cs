using System.Collections.Generic;
using System.Linq;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_QUEST_POI_QUERY : Common.Network.PacketReader
    {
        public uint Count;

        public CMSG_QUEST_POI_QUERY(byte[] data) : base(data)
        {
            Count = ReadUInt32();

            var questIds = new List<uint>();

            for (var i = 0; i < Count; i++)
                questIds.Add(ReadUInt32());

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_QUEST_POI_QUERY] Count: {Count} questIds: {string.Join("\t", questIds.Cast<string>().ToArray())}");
#endif
        }
    }
}