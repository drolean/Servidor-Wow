using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AUCTION_SELL_ITEM : Common.Network.PacketReader
    {
        public UInt64 AuctioneerId;
        public uint Unknown;
        public UInt64 ItemId;
        public uint StackSize;
        public uint Bid;
        public uint Buyout;
        public uint Time;

        public CMSG_AUCTION_SELL_ITEM(byte[] data) : base(data)
        {
            /*
            Dim cGUID As ULong = packet.GetUInt64
            Dim iGUID As ULong = packet.GetUInt64
            Dim Bid As Integer = packet.GetInt32
            Dim Buyout As Integer = packet.GetInt32
            Dim Time As Integer = packet.GetInt32
             */
            AuctioneerId = ReadUInt64();
            Unknown = ReadUInt32();
            ItemId = ReadUInt64();
            StackSize = ReadUInt32();
            Bid = ReadUInt32();
            Buyout = ReadUInt32();
            Time = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_AUCTION_SELL_ITEM] AuctioneerId: {AuctioneerId} Unknown: {Unknown} ItemId: {ItemId} " +
                                     $"StackSize: {StackSize} Bid: {Bid} Buyout: {Buyout} Time: {Time}");
#endif
        }
    }
}