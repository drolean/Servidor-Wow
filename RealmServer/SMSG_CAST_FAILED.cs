using System;
using Common.Database.Tables;
using Common.Globals;

namespace RealmServer
{
    internal sealed class SMSG_CAST_FAILED : Common.Network.PacketServer
    {
        public SMSG_CAST_FAILED(Items dbItem = null) : base(RealmEnums.SMSG_CAST_FAILED)
        {
            Console.WriteLine($"Vai: {MainProgram.Vai} Count: {MainProgram.Count}");

            Write(8690); // SpellID
            Write((byte) 2); // IDK
            Write((byte) MainProgram.Vai); // RESULT

            MainProgram.Vai++;
        }
    }
}
