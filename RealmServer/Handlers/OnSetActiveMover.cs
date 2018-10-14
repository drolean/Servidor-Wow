using System;
using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnSetActiveMover
    {
        public static void Handler(RealmServerSession session, CMSG_SET_ACTIVE_MOVER handler)
        {
            Console.WriteLine(handler.Guid);
        }
    }
}