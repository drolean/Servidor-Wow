namespace RealmServer.PacketReader
{
    public sealed class CMSG_CHAR_CREATE : Common.Network.PacketReader
    {
        public CMSG_CHAR_CREATE(byte[] data) : base(data)
        {
            Name = ReadCString();

            Race = ReadByte();
            Classe = ReadByte();
            Gender = ReadByte();

            Skin = ReadByte();
            Face = ReadByte();
            HairStyle = ReadByte();
            HairColor = ReadByte();
            FacialHair = ReadByte();
            OutfitId = ReadByte();
        }

        public string Name { get; }

        public byte Race { get; }
        public byte Classe { get; }
        public byte Gender { get; }

        public byte Skin { get; }
        public byte Face { get; }
        public byte HairStyle { get; }
        public byte HairColor { get; }
        public byte FacialHair { get; }

        public byte OutfitId { get; }
    }
}