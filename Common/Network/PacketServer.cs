using System;
using System.IO;
using System.Text;
using Common.Globals;

namespace Common.Network
{
    public class PacketServer : BinaryWriter
    {
        public int Opcode;

        public PacketServer(int opcode) : base(new MemoryStream())
        {
            Opcode = opcode;
        }

        public PacketServer(AuthCMD authOpcode) : this((byte) authOpcode)
        {
        }

        public PacketServer(RealmEnums realmOpcode) : this((int) realmOpcode)
        {
        }

        public byte[] Packet => (BaseStream as MemoryStream)?.ToArray();

        /// <summary>
        ///     Writes a C-style string. (Ends with a null terminator)
        /// </summary>
        /// <param name="input">The input.</param>
        public void WriteCString(string input)
        {
            var data = Encoding.UTF8.GetBytes(input + '\0');
            Write(data);
        }

        protected internal static void WriteBytes(BinaryWriter writer, byte[] data, int count = 0)
        {
            if (count == 0)
                writer.Write(data);
            else
                writer.Write(data, 0, count);
        }

        protected internal static byte[] GenerateGuidBytes(ulong id)
        {
            byte[] packedGuid = new byte[9];
            byte length = 1;

            for (byte i = 0; id != 0; i++)
            {
                if ((id & 0xFF) != 0)
                {
                    packedGuid[0] |= (byte)(1 << i);
                    packedGuid[length] = (byte)(id & 0xFF);
                    ++length;
                }

                id >>= 8;
            }

            byte[] clippedArray = new byte[length];
            Array.Copy(packedGuid, clippedArray, length);

            return clippedArray;
        }
    }
}