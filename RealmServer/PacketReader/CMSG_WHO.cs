using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_WHO represents a packet sent by the client when it wants to retrieve who information.
    /// </summary>
    public sealed class CMSG_WHO : Common.Network.PacketReader
    {
        public uint LevelMax;
        public uint LevelMin;
        public uint MaskClass;
        public uint MaskRace;
        public string NameGuild;
        public string NamePlayer;
        public uint ZonesCount;

        public CMSG_WHO(byte[] data) : base(data)
        {
            LevelMin = ReadUInt32();
            LevelMax = ReadUInt32();

            NamePlayer = ReadCString();
            NameGuild = ReadCString();

            MaskRace = ReadUInt32();
            MaskClass = ReadUInt32();
            ZonesCount = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_WHO] LevelMin: {LevelMin} LevelMax: {LevelMax} " +
                                     $"NamePlayer: {NamePlayer} NameGuild: {NameGuild} " +
                                     $"MaskRace: {MaskRace} MaskClass: {MaskClass} ZonesCount: {ZonesCount}");
#endif
            /*
            If zonesCount > 10 Then Exit Sub
                Dim zones As New List(Of UInteger)
            For i As Integer = 1 To zonesCount
            zones.Add(packet.GetUInt32)
            Next
                Dim stringsCount As UInteger = packet.GetUInt32         'Limited to 4
            If stringsCount > 4 Then Exit Sub
                Dim strings As New List(Of String)
            For i As Integer = 1 To stringsCount
            strings.Add(UCase(EscapeString(packet.GetString())))
            Next
                */
        }
    }
}