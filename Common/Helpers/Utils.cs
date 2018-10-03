using System;

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
        public static string ToProperCase(this string source)
        {
            // If there are 0 or 1 characters, just return the string.
            if (source == null) return null;
            if (source.Length < 2) return source.ToUpper();

            // Start with the first character.
            string result = source.Substring(0, 1).ToUpper();

            // Add the remaining characters.
            for (int i = 1; i < source.Length; i++)
            {
                if (Char.IsUpper(source[i])) result += " ";
                result += source[i];
            }

            return result;
        }

        // Convert the string to Pascal case.
        public static string ToPascalCase(this string source)
        {
            if (source == null) return null;
            if (source.Length < 2) return source.ToUpper();

            // Split the string into words.
            string[] words = source.Split(
                new char[] {},
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = "";
            foreach (string word in words)
            {
                result +=
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1);
            }

            return result;
        }

        public static string ToCamelCase(this string source)
        {
            // If there are 0 or 1 characters, just return the string.
            if (source == null || source.Length < 2)
                return source;

            // Split the string into words.
            string[] words = source.Split(
                new char[] {},
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = words[0].ToLower();
            for (int i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }
    }
}