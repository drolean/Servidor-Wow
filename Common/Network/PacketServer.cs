using System.IO;
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

        public PacketServer(AuthCMD authOpcode) : this((byte)authOpcode) { }

        public PacketServer(RealmCMD realmOpcode) : this((int)realmOpcode) { }

        public byte[] Packet => (BaseStream as MemoryStream)?.ToArray();
    }
}
