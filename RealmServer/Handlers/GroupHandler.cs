using Common.Network;

namespace RealmServer.Handlers
{
    internal class GroupHandler
    {
        internal static void OnRequestRaidInfo(RealmServerSession session, PacketReader handler)
        {
            // SMSG_RAID_INSTANCE_INFO

            /* Packet Struct
             * Int32 = Instances Counts
             * Foreach()
             * Int32 = MapID
             * Int32 = TimeLeft Expries
             * Int32 = InstanceId
             * Int32 = Is this is a counter, shouldn't it be counting ? = contador do foreach???
             * EndForeach()
             */
        }
    }
}