using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Crypt;

namespace Common.Helpers
{
    public static class Utils
    {
        public static VanillaCrypt PacketCrypto { get; set; }

        public static byte[] Encode(int size, int opcode)
        {
            int index = 0;
            int newSize = size + 2;
            byte[] header = new byte[4];
            if (newSize > 0x7FFF)
                header[index++] = (byte)(0x80 | (0xFF & (newSize >> 16)));

            header[index++] = (byte)(0xFF & (newSize >> 8));
            header[index++] = (byte)(0xFF & (newSize >> 0));
            header[index++] = (byte)(0xFF & opcode);
            header[index] = (byte)(0xFF & (opcode >> 8));

            if (PacketCrypto != null) header = PacketCrypto.Encrypt(header);

            return header;
        }

        public static void Decode(byte[] header, out ushort length, out short opcode)
        {
            PacketCrypto?.Decrypt(header, 6);

            if (PacketCrypto == null)
            {
                length = BitConverter.ToUInt16(new[] { header[1], header[0] }, 0);
                opcode = BitConverter.ToInt16(header, 2);
            }
            else
            {
                length = BitConverter.ToUInt16(new[] { header[1], header[0] }, 0);
                opcode = BitConverter.ToInt16(new[] { header[2], header[3] }, 0);
            }
        }

        public static string ByteArrayToHex(IReadOnlyCollection<byte> data)
        {
            string packetOutput = string.Empty;

            for (int i = 0; i < data.Count; i++)
            {
                packetOutput += i.ToString("X2") + " ";
            }

            return packetOutput;
        }

        private static string Int32ToBigEndianHexByteString(int i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            string format = BitConverter.IsLittleEndian ? "0x{0:X2}" : "c0x{0:X2}";
            return String.Format(format, bytes[0]);
        }

        // Capitalize the first character and add a space before
        // each capitalized letter (except the first character).

        /// <summary>
        ///     Capitalize the first character and add a space before
        ///     each capitalized letter (except the first character).
        /// </summary>
        /// <param name="source">String to convert</param>
        /// <returns></returns>
        public static string ToProperCase(this string source)
        {
            if (source == null) return null;
            if (source.Length < 2) return source.ToUpper();

            string result = source.Substring(0, 1).ToUpper();

            for (int i = 1; i < source.Length; i++)
            {
                if (Char.IsUpper(source[i])) result += " ";
                result += source[i];
            }

            return result;
        }

        /// <summary>
        ///     Convert a string to PascalCase.
        /// </summary>
        /// <param name="source">String to convert</param>
        /// <returns></returns>
        public static string ToPascalCase(this string source)
        {
            string sample = string.Join("", source?.Select(c => Char.IsLetterOrDigit(c) ? c.ToString().ToLower() : "_").ToArray());

            var arr = sample?
                .Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => $"{s.Substring(0, 1).ToUpper()}{s.Substring(1)}");

            sample = string.Join("", arr);

            return sample;
        }

        /// <summary>
        /// Converts the given string value into camelCase.
        /// </summary>
        /// <param name="source">String to convert</param>
        /// <returns>
        /// The camel case value.
        /// </returns>
        public static string ToCamelCase(this string source)
        {
            return Regex.Replace(source, @"[_-](\w)", m => m.Groups[1].Value.ToUpper());
        }
    }
}