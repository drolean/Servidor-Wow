using System;
using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnJoinChannel
    {
        public static void Handler(RealmServerSession session, CMSG_JOIN_CHANNEL handler)
        {
            Console.WriteLine(handler.Channel);
        }
    }
}