﻿using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GUILD_SET_PUBLIC_NOTE : Common.Network.PacketReader
    {
        public string Note;
        public string PlayerName;

        public CMSG_GUILD_SET_PUBLIC_NOTE(byte[] data) : base(data)
        {
            PlayerName = ReadCString();
            Note = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GUILD_SET_PUBLIC_NOTE] PlayerName: {PlayerName} Note: {Note}");
#endif
        }
    }
}
