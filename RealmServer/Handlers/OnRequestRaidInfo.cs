using RealmServer.Database;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    internal class OnRequestRaidInfo
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            // session.User

            /*
             * SMSG_RAID_INSTANCE_INFO
             *
             * int32 = Count
             * @for
             *   uint32 = MapID
             *   uint32 = TimeLeft
             *   uint32 = InstanceID
             *   uint32 = counter For
             */
        }
    }
}