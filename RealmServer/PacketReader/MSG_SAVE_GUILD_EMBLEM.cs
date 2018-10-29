using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class MSG_SAVE_GUILD_EMBLEM : Common.Network.PacketReader
    {
        public int BackgroundColor;
        public int BorderColor;
        public int BorderStyle;
        public int EmblemColor;
        public int EmblemStyle;
        public int Unk0;
        public int Unk1;

        public MSG_SAVE_GUILD_EMBLEM(byte[] data) : base(data)
        {
            Unk0 = ReadInt32();
            Unk1 = ReadInt32();
            EmblemStyle = ReadInt32();
            EmblemColor = ReadInt32();
            BorderStyle = ReadInt32();
            BorderColor = ReadInt32();
            BackgroundColor = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[MSG_SAVE_GUILD_EMBLEM] Unk0: {Unk0} Unk1: {Unk1} EmblemStyle: {EmblemStyle} " +
                                     $"EmblemColor: {EmblemColor} BorderStyle: {BorderStyle} BorderColor: {BorderColor} " +
                                     $"BackgroundColor: {BackgroundColor}");
#endif
        }
    }
}
