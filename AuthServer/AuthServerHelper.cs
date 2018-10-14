using System;
using System.Net.Sockets;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;

namespace AuthServer
{
    public class AuthServerHelper
    {
        public static bool CheckRealmStatus(Realms realm)
        {
            var spliIp = realm.Address.Split(':');
            int.TryParse(spliIp[1], out var port);

            using (var tcpTestRealm = new TcpClient())
            {
                try
                {
                    tcpTestRealm.Connect(spliIp[0], port);
                    MainProgram.Database.UpdateRealmStatus(realm, RealmFlag.Recommended);
                    return true;
                }
                catch (Exception)
                {
                    var realmName = realm.Name.PadRight(25, ' ');
                    Log.Print(LogType.Error, $"{realmName} |=> Off-Line");
                    MainProgram.Database.UpdateRealmStatus(realm, RealmFlag.Offline);
                    return false;
                }
            }
        }
    }
}