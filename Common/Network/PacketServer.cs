using Common.Globals;
using System.IO;

namespace Common.Network
{
    public class PacketServer : BinaryWriter
    {
        public int Opcode;

        public PacketServer(int opcode) : base(new MemoryStream())
        {
            Opcode = opcode;
        }

        public PacketServer(AuthCMD opcode) : this((byte)opcode)
        {

        }

        public PacketServer(RealmCMD worldOpcode) : this((int)worldOpcode)
        {

        }

        public byte[] Packet
        {
            get { return (BaseStream as MemoryStream).ToArray(); }
        }
    }
}
