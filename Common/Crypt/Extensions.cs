using System;
using System.IO;
using System.IO.Compression;

namespace Common.Crypt
{
    public static class Extensions
    {
        public static byte[] Pad(this byte[] bytes, int count)
        {
            Array.Resize(ref bytes, count);
            return bytes;
        }

        public static byte[] Decompress(this byte[] data)
        {
            var uncompressedLength = BitConverter.ToUInt32(data, 0);
            var output = new byte[uncompressedLength];

            using (var ms = new MemoryStream(data, 6, data.Length - 6))
            using (var ds = new DeflateStream(ms, CompressionMode.Decompress))
            {
                ds.Read(output, 0, output.Length);
            }
            return output;
        }
    }
}