namespace RealmServer.Handlers
{
    internal class OnBattleFieldStatus
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            // session.User

            /*
             * SMSG_BATTLEFIELD_STATUS
             *
                data << uint32(0x0);                                    // Unknown 1
                data << uint32(MapID);                                  // MapID
                data << uint8(0);                                       // Unknown
                data << uint32(InstanceID);                             // Instance ID
                data << uint32(StatusID);                               // Status ID
                data << uint32(Time);                                   // Time
             */
        }
    }
}
