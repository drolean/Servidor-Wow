using System.Collections.Generic;
using Common.Database.Tables;
using Common.Globals;

namespace AuthServer.PacketServer
{
    internal sealed class PsAuthRealmList : Common.Network.PacketServer
    {
        /// <summary>
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
        /// <param name="accountName"></param>
        /// <returns></returns>
        /// <todo>/// Count Population of Realm/// </todo>
        public PsAuthRealmList(List<Realms> realms, string accountName) : base(AuthCMD.CMD_AUTH_REALMLIST)
        {
            Write((uint) 0x0000);
            Write((byte) realms.Count);
            /*
            foreach (var realm in realms)
            {
                var count = MainProgram.Database.GetCharactersUsers(realm.Id, accountName);

                Write((uint) realm.type);
                Write((byte) realm.flag);
                WriteCString(realm.name);
                WriteCString(realm.address);
                Write(1.6f);
                Write((byte) count);
                Write((byte) realm.timezone);
                Write((byte) 0x01);
            }
            */
            Write((ushort) 0x0002);
        }
    }
}