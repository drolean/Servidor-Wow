using System;
using System.Diagnostics;
using Common.Helpers;

namespace AuthServer.PacketReader
{
    public sealed class AuthLogonProof : Common.Network.PacketReader
    {
        public byte OptCode { get; }
        public byte[] A { get; }
        public byte[] M1 { get; }
        public byte[] CrcHash { get; }
        public byte NKeys { get; }
        public byte Unk { get; }

        public AuthLogonProof(byte[] data) : base(data)
        {
            OptCode = ReadByte();
            A       = ReadBytes(32);
            M1      = ReadBytes(20);

            CrcHash = ReadBytes(20);
            try
            {
                NKeys = ReadByte();
                Unk = ReadByte();
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }
        }
    }
}