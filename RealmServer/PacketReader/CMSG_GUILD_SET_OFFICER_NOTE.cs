using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GUILD_SET_OFFICER_NOTE : Common.Network.PacketReader
    {
        public string PlayerName;
        public string Note;

        public CMSG_GUILD_SET_OFFICER_NOTE(byte[] data) : base(data)
        {
            PlayerName = ReadCString();
            Note = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GUILD_SET_OFFICER_NOTE] PlayerName: {PlayerName} Note: {Note}");
#endif
        }
    }
}