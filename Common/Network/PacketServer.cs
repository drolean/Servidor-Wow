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

        public PacketServer(AuthCMD AuthOpcode) : this((byte)AuthOpcode) { }

        public PacketServer(RealmCMD RealmOpcode) : this((int)RealmOpcode) { }

        public byte[] Packet
        {
            get { return (BaseStream as MemoryStream).ToArray(); }
        }
    }
}
