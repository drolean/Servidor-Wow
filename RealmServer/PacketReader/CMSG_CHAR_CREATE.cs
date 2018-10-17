using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_CHAR_CREATE represents a packet sent by the client whenever it tries to create a character.
    /// </summary>
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

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CHAR_CREATE] Name: {Name} Race: {Race} Classe: {Classe} Gender: {Gender} " +
                                     $"Skin: {Skin} Face: {Face} HairStyle: {HairStyle} HairColor: {HairColor} OutfitId: {OutfitId}");
#endif
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