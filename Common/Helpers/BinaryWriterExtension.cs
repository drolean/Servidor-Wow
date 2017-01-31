using System.IO;
using System.Text;

namespace Common.Helpers
{
    public static class BinaryWriterExtension
    {
        public static void WriteNullByte(this BinaryWriter writer, uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                writer.Write((byte)0);
            }
        }

        public static void WriteCString(this BinaryWriter writer, string input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input + '\0');
            writer.Write(data);
        }
    }
}
