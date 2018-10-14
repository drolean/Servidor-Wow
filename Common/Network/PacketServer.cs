using System.IO;
using System.Text;
using Common.Globals;
using MongoDB.Bson;

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
    }
}