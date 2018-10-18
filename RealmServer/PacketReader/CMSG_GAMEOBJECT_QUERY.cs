using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_GAMEOBJECT_QUERY represents a packet sent by the client when it wants to retrieve gameobject information.
    /// </summary>
    public sealed class CMSG_GAMEOBJECT_QUERY : Common.Network.PacketReader
    {
        public int GameObjectId;
        public ulong GameObjectUid;

        public CMSG_GAMEOBJECT_QUERY(byte[] data) : base(data)
        {
            GameObjectId = ReadInt32();
            GameObjectUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_GAMEOBJECT_QUERY] GameObjectId: {GameObjectId} GameObjectUid: {GameObjectUid}");
#endif
        }
    }
}