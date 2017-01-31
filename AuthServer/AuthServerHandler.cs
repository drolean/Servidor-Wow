using System;
using System.Net;
using Common.Globals;
using Common.Network;
using Framework.Helpers;

namespace AuthServer
{
    #region CMD_AUTH_LOGON_CHALLENGE
    public sealed class AuthLogonChallenge : PacketReader
    {
        public IPAddress IP;
        public string Version;
        public ushort Build;
        public string Username;
        public string Language;

        public AuthLogonChallenge(byte[] data) : base(data)
        {
            IP       = IPAddress.Parse(data[29] + "." + data[30] + "." + data[31] + "." + data[32]);
            Version  = data[8] + "." + data[9] + "." + data[10];
            Build    = (ushort) BitConverter.ToInt16(new[] { data[11], data[12] }, 0);
            Language = (data[24] + data[23] +data[22] + data[21]).ToString();
            for (int i = 0; i <= data[33] - 1; i++)
            {
                Username = Username + (char) (data[34 + i]);
            }
        }
    }
    #endregion

    internal class AuthServerHandler
    {
        public static void OnAuthLogonChallenge(AuthServerSession session, AuthLogonChallenge packet)
        {
            Log.Print(LogType.AuthServer,
                $"[{session.ConnectionSocket.RemoteEndPoint}] CMD_AUTH_LOGON_CHALLENGE [{packet.Username}], WoW Version [{packet.Version}.{packet.Build}]");

            AccountState accState = default(AccountState);
            byte[] dataResponse;

            if (packet.Version == null)
            {
                Log.Print(LogType.Error, $"{session.ConnectionSocket.RemoteEndPoint} Invalid Client");

                dataResponse = new byte[2];
                dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                dataResponse[1] = (byte)AccountState.LOGIN_BADVERSION;
                session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_BADVERSION]");
            }
            else if (packet.Version == "1.12.1")
            {
                try
                {
                    // Get Account info
                    //_accountDatabase.Query(String.Format("SELECT id, sha_pass_hash, gmlevel, expansion FROM account WHERE username = ""{0}"";", packetAccount), result)

                    // Check Account state
                    //If _accountDatabase.QuerySQL("SELECT id FROM account_banned WHERE id = '" & result.Rows(0).Item("id") & "';") Then
                    //accState = AccountState.LOGIN_BANNED
                    //Else
                    //accState = AccountState.LOGIN_OK
                    //End If
                    //Else
                    //accState = AccountState.LOGIN_UNKNOWN_ACCOUNT
                }
                catch (Exception)
                {
                    accState = AccountState.LOGIN_DBBUSY;
                }


                switch (accState)
                {
                    case AccountState.LOGIN_OK:
                        break;
                    case AccountState.LOGIN_UNKNOWN_ACCOUNT:
                        dataResponse = new byte[2];
                        dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                        dataResponse[1] = (byte)AccountState.LOGIN_UNKNOWN_ACCOUNT;
                        session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_UNKNOWN_ACCOUNT]");
                        break;
                    case AccountState.LOGIN_BANNED:
                        dataResponse = new byte[2];
                        dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                        dataResponse[1] = (byte)AccountState.LOGIN_BANNED;
                        session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_BANNED]");
                        break;
                    case AccountState.LOGIN_NOTIME:
                        dataResponse = new byte[2];
                        dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                        dataResponse[1] = (byte)AccountState.LOGIN_NOTIME;
                        session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_NOTIME]");
                        break;
                    case AccountState.LOGIN_ALREADYONLINE:
                        dataResponse = new byte[2];
                        dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                        dataResponse[1] = (byte)AccountState.LOGIN_ALREADYONLINE;
                        session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_ALREADYONLINE]");
                        break;
                    default:
                        dataResponse = new byte[2];
                        dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                        dataResponse[1] = (byte)AccountState.LOGIN_FAILED;
                        session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_FAILED]");
                        break;
                }
            }
            else
            {
                Log.Print(LogType.Error, $"{session.ConnectionSocket.RemoteEndPoint} Wrong Version [{packet.Version}.{packet.Build}]");

                dataResponse = new byte[2];
                dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                dataResponse[1] = (byte)AccountState.LOGIN_BADVERSION;
                session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE-LOGIN_BADVERSION");
            }
        }
    }

}
