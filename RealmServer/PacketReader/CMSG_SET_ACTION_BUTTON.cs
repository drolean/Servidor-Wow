using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_ACTION_BUTTON : Common.Network.PacketReader
    {
        public byte Button;
        public ushort Action;
        public byte ActionMisc;
        public byte ActionType;

        public CMSG_SET_ACTION_BUTTON(byte[] data) : base(data)
        {
            Button = ReadByte();
            Action = ReadUInt16();
            ActionMisc = ReadByte();
            ActionType = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_ACTION_BUTTON] Button: {Button} Action: {Action} ActionMisc: {ActionMisc} ActionType: {ActionType}");
#endif
        }
    }
}