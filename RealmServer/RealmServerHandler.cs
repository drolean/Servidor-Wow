using Common.Globals;
using Common.Network;
using System;

namespace RealmServer
{
    internal class RealmServerHandler
    {
        #region SMSG_AUTH_CHALLENGE
        internal sealed class SmsgAuthChallenge : PacketServer
        {
            private readonly uint _serverSeed = (uint)new Random().Next(0, int.MaxValue);

            public SmsgAuthChallenge() : base(RealmCMD.SMSG_AUTH_CHALLENGE)
            {
                Write(1);
                Write(_serverSeed);
                Write(0);
                Write(0);
                Write(0);
                Write(0);
            }
        }
        #endregion
    }
}
