using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    public static class Utils
    {
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