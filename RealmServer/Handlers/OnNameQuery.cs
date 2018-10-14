using System;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnNameQuery
    {
        public static void Handler(RealmServerSession session, CMSG_NAME_QUERY handler)
        {
            Console.WriteLine(handler.Guid);
        }
    }
}