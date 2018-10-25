using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    public static class Utils
    {
        private static readonly Random Rand = new Random();

        public static double GetDistance(float aX, float aY, float bX, float bY)
        {
            double a = aX - bX;
            double b = bY - aY;

            return Math.Sqrt(a * a + b * b);
        }

        public static ulong GenerateRandUlong()
        {
            var thirtyBits = (uint) Rand.Next(1 << 30);
            var twoBits = (uint) Rand.Next(1 << 2);

            return (thirtyBits << 2) | twoBits;
        }

        public static void DumpPacket(byte[] data)
        {
            int j;
            const string buffer = "";

            Log.Print("DEBUG: Packet Dump");

            if (data.Length % 16 == 0)
            {
                for (j = 0; j <= data.Length - 1; j += 16)
                    Log.Print($"| {BitConverter.ToString(data, j, 16).Replace("-", " ")} | " +
                              Encoding.ASCII.GetString(data, j, 16)
                                  .Replace("\t", "?")
                                  .Replace("\b", "?")
                                  .Replace("\r", "?")
                                  .Replace("\f", "?")
                                  .Replace("\n", "?") + " |");
            }
            else
            {
                for (j = 0; j <= data.Length - 1 - 16; j += 16)
                    Log.Print($"| {BitConverter.ToString(data, j, 16).Replace("-", " ")} | " +
                              Encoding.ASCII.GetString(data, j, 16)
                                  .Replace("\t", "?")
                                  .Replace("\b", "?")
                                  .Replace("\r", "?")
                                  .Replace("\f", "?")
                                  .Replace("\n", "?") + " |");

                Log.Print($"| {BitConverter.ToString(data, j, data.Length % 16).Replace("-", " ")} " +
                          $"{buffer.PadLeft((16 - data.Length % 16) * 3, ' ')}" +
                          "| " + Encoding.ASCII.GetString(data, j, data.Length % 16)
                              .Replace("\t", "?")
                              .Replace("\b", "?")
                              .Replace("\r", "?")
                              .Replace("\f", "?")
                              .Replace("\n", "?") +
                          $"{buffer.PadLeft(16 - data.Length % 16, ' ')}|");
            }
        }

        /// <summary>
        ///     Gets the hex representation of the byte array. The length is 2xdata.Length
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToHex(IReadOnlyCollection<byte> data)
        {
            var packetOutput = string.Empty;

            for (var i = 0; i < data.Count; i++) packetOutput += i.ToString("X2") + " ";

            return packetOutput;
        }

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

            var result = source.Substring(0, 1).ToUpper();

            for (var i = 1; i < source.Length; i++)
            {
                if (char.IsUpper(source[i])) result += " ";
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
            var sample = string.Join("",
                source?.Select(c => char.IsLetterOrDigit(c) ? c.ToString().ToLower() : "_").ToArray());

            var arr = sample.Split(new[] {'_'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => $"{s.Substring(0, 1).ToUpper()}{s.Substring(1)}");

            sample = string.Join("", arr);

            return sample;
        }

        /// <summary>
        ///     Converts the given string value into camelCase.
        /// </summary>
        /// <param name="source">String to convert</param>
        /// <returns>
        ///     The camel case value.
        /// </returns>
        public static string ToCamelCase(this string source)
        {
            return Regex.Replace(source, @"[_-](\w)", m => m.Groups[1].Value.ToUpper());
        }
    }
}