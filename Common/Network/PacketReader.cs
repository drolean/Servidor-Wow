using System;
using System.IO;
using System.Net;
using System.Text;

namespace Common.Network
{
    public class PacketReader : BinaryReader
    {
        public PacketReader(Stream input) : base(input, Encoding.UTF8)
        {
        }

        public PacketReader(byte[] data) : base(new MemoryStream(data), Encoding.UTF8)
        {
        }

        public string ReadCString()
        {
            var ret = string.Empty;

            var c = ReadChar();
            while (c != '\0')
            {
                ret += c;
                c = ReadChar();
            }

            return ret;
        }

        public string ReadPascalString(byte numBytesForLength)
        {
            uint readCount;
            switch (numBytesForLength)
            {
                case 1:
                    readCount = ReadByte();
                    break;
                case 2:
                    readCount = ReadUInt16();
                    break;
                case 4:
                    readCount = ReadUInt32();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(numBytesForLength));
            }

            var ret = "";
            for (var i = 0; i < readCount; i++) ret += ReadChar();
            return ret;
        }

        public IPAddress ReadIpAddress()
        {
            return new IPAddress(ReadBytes(4));
        }

        public string ReadStringReversed(int length)
        {
            var str = ReadBytes(length);
            Array.Reverse(str);
            return Encoding.UTF8.GetString(str);
        }

        public byte[] ReadBytesReversed(int count)
        {
            var ret = ReadBytes(count);
            Array.Reverse(ret);
            return ret;
        }
    }
}