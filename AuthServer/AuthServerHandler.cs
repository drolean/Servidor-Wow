using System;
using System.Diagnostics;
using AuthServer.PacketReader;
using AuthServer.PacketServer;
using Common.Crypt;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;

namespace AuthServer
{
    internal class AuthServerHandler
    {
        private static Users _user;

        internal static void OnAuthLogonChallenge(AuthServerSession session, AuthLogonChallenge packet)
        {
            Log.Print(LogType.AuthServer,
                $"[{session.ConnectionSocket.RemoteEndPoint}] CMD_AUTH_LOGON_CHALLENGE [{packet.Username}], " +
                $"WoW Version [{packet.Version}.{packet.Build}] {packet.Country}");

            var dataResponse = new byte[2];
            dataResponse[0] = (byte) LoginErrorCode.RESPONSE_FAILURE;

            switch (packet.Version)
            {
                case "1.12.1":
                case "1.12.2":
                case "1.12.3":
                {
                    AccountState accState;
                    try
                    {
                        // Get Account info
                        _user = MainProgram.Database.GetAccount(packet.Username);

                        if (_user != null)
                            accState = _user.bannet_at != null ? AccountState.BANNED : AccountState.OK;
                        else
                            accState = AccountState.UNKNOWN_ACCOUNT;
                    }
                    catch (Exception e)
                    {
                        accState = AccountState.DBBUSY;
                        var trace = new StackTrace(e, true);
                        Log.Print(LogType.Error,
                            $"{e.Message}: {e.Source}" +
                            $"{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                    }

                    switch (accState)
                    {
                        case AccountState.OK:
                            Log.Print(LogType.AuthServer,
                                $"[{session.ConnectionSocket.RemoteEndPoint}] Account found [{packet.Username}]");
                            session.AccountName = _user?.username;
                            if (_user != null)
                                session.Srp = new Srp6(_user.username.ToUpper(), _user.password.ToUpper());
                            session.SendData(new PsAuthLogonChallange(session.Srp, AccountState.OK));
                            return;
                        case AccountState.UNKNOWN_ACCOUNT:
                            dataResponse[1] = (byte) AccountState.UNKNOWN_ACCOUNT;
                            break;
                        case AccountState.BANNED:
                            dataResponse[1] = (byte) AccountState.BANNED;
                            break;
                        case AccountState.NOTIME:
                            dataResponse[1] = (byte) AccountState.NOTIME;
                            break;
                        case AccountState.ALREADYONLINE:
                            dataResponse[1] = (byte) AccountState.ALREADYONLINE;
                            break;
                        case AccountState.FAILED:
                            dataResponse[1] = (byte) AccountState.FAILED;
                            break;
                        case AccountState.BAD_PASS:
                            dataResponse[1] = (byte) AccountState.BAD_PASS;
                            break;
                        case AccountState.DBBUSY:
                            dataResponse[1] = (byte) AccountState.DBBUSY;
                            break;
                        case AccountState.BADVERSION:
                            dataResponse[1] = (byte) AccountState.BADVERSION;
                            break;
                        case AccountState.DOWNLOADFILE:
                            dataResponse[1] = (byte) AccountState.DOWNLOADFILE;
                            break;
                        case AccountState.SUSPENDED:
                            dataResponse[1] = (byte) AccountState.SUSPENDED;
                            break;
                        case AccountState.PARENTALCONTROL:
                            dataResponse[1] = (byte) AccountState.PARENTALCONTROL;
                            break;
                        default:
                            dataResponse[1] = (byte) AccountState.FAILED;
                            break;
                    }

                    break;
                }
                default:
                    dataResponse[1] = (byte) AccountState.BADVERSION;
                    break;
            }

            session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE");
        }

        internal static void OnAuthLogonProof(AuthServerSession session, AuthLogonProof handler)
        {
            session.Srp.ClientEphemeral = handler.A.ToPositiveBigInteger();
            session.Srp.ClientProof = handler.M1.ToPositiveBigInteger();

            if (session.Srp.ClientProof == session.Srp.GenerateClientProof())
            {
                MainProgram.Database.SetSessionKey(session.AccountName, session.Srp.SessionKey.ToProperByteArray());
                session.SendData(new PsAuthLogonProof(session.Srp, AccountState.OK));
                return;
            }

            var dataResponse = new byte[2];
            dataResponse[0] = (byte) AuthCMD.CMD_AUTH_LOGON_PROOF;
            dataResponse[1] = (byte) AccountState.UNKNOWN_ACCOUNT;
            session.SendData(dataResponse, "RS_LOGON_PROOF-WRONGPASS");
        }

        /// <summary>
        ///     Send packet Realm List
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data"></param>
        internal static void OnAuthRealmList(AuthServerSession session, byte[] data)
        {
            var realms = MainProgram.Database.GetRealms();
            session.SendPacket(new PsAuthRealmList(realms, session.AccountName));
        }
    }
}