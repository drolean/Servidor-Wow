using System.Collections.Generic;
using Common.Database.Tables;
using Common.Globals;

namespace AuthServer.PacketServer
{
    internal sealed class CMD_AUTH_REALMLIST : Common.Network.PacketServer
    {
        /// <summary>
        ///     Third authentication step, the client is requesting the realm list.
        ///     @for
        ///     Type       : byte;
        ///     Flag       : byte;
        ///     Name       : byte;
        ///     Address    : array of byte;
        ///     Population : byte;            Pop {400F -> Full; 5F -> Medium; 1.6F -> Low; 200F -> New; 2F -> High}
        ///     Chars      : array of byte;
        ///     Time       : byte;
        ///     ?????      : byte;
        /// </summary>
        /// <param name="realms"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        /// <todo>/// Count Population of Realm/// </todo>
        public CMD_AUTH_REALMLIST(IReadOnlyCollection<Realms> realms, AuthServerSession session) : base(
            AuthCMD.CMD_AUTH_REALMLIST)
        {
            Write((uint) 0x0000);
            Write((byte) realms.Count);

            foreach (var realm in realms)
            {
                var count = MainProgram.Database.GetCharactersByUser(realm, session.User.Username);

                Write((uint) realm.Type);
                Write((byte) realm.Flag);
                WriteCString(realm.Name);
                WriteCString(realm.Address);
                Write(1.6f);
                Write((byte) count.Count);
                Write((byte) realm.Timezone);
                Write((byte) 0x01);
            }

            Write((ushort) 0x0002);
    
        }
    }
}
