using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Common.Crypt;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using Common.Network;

namespace AuthServer
{
    #region CMD_AUTH_LOGON_CHALLENGE (DONE)
    public sealed class AuthLogonChallenge : PacketReader
    {
        public IPAddress Ip;
        public string Version;
        public ushort Build;
        public string Language;
        public string Username;

        public AuthLogonChallenge(byte[] data) : base(data)
        {
            Ip       = IPAddress.Parse(data[29] + "." + data[30] + "." + data[31] + "." + data[32]);
            Version  = data[8] + "." + data[9] + "." + data[10];
            Build    = (ushort) BitConverter.ToInt16(new[] { data[11], data[12] }, 0);
            Language = (data[24] + data[23] +data[22] + data[21]).ToString();
            for (int i = 0; i <= data[33] - 1; i++)
            {
                Username = Username + (char) data[34 + i];
            }
        }
    }

    internal sealed class PsAuthLogonChallange : PacketServer
    {
        /// <summary>
        ///   Opcode          : byte;            0x00
        ///   Error           : byte;            AccountState
        ///   Size            : byte;            unkown1 is set to 0 by all private servers.
        ///   =====           : array of byte;   SRP6 server public ephemeral
        ///   =====           : byte;            generator_len is the length of the generator field following it. All servers (including ours) set this to 1.
        ///   =====           : array of byte;   All servers (including ours) hardcode this to 7
        ///   =====           : byte;            All servers (including ours) set this to 32.
        ///
        ///   =====           : array of byte;   All servers (including ours) set this to 0x894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7
        ///   =====           : array of byte;   A salt is a randomly generated value used to strengthen a user's password against attacks where pre-computations are performed
        ///
        ///   =====           : byte;            unknown2 is set to 16 random bytes by all servers.
        /// </summary>
        /// <param name="srp"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public PsAuthLogonChallange(Srp6 srp, AccountState result) : base(AuthCMD.CMD_AUTH_LOGON_CHALLENGE)
        {
            Write((byte) AuthCMD.CMD_AUTH_LOGON_CHALLENGE);
            Write((byte) result);
            Write((byte) 0);
            Write(srp.ServerEphemeral.ToProperByteArray());
            Write((byte) 1);
            Write(srp.Generator.ToByteArray());
            Write((byte) 32);
            Write(srp.Modulus.ToProperByteArray().Pad(32));
            Write(srp.Salt.ToProperByteArray().Pad(32));
            this.WriteNullByte(17);
        }
    }
    #endregion

    #region CMD_AUTH_LOGON_PROOF (DONE)
    public sealed class AuthLogonProof : PacketReader
    {
        public byte OptCode { get; }
        public byte[] A { get; }
        public byte[] M1 { get; }
        public byte[] CrcHash { get; }
        public byte NKeys { get; }
        public byte Unk { get; }

        public AuthLogonProof(byte[] data) : base(data)
        {
            OptCode = ReadByte();
            A       = ReadBytes(32);
            M1      = ReadBytes(20);

            CrcHash = ReadBytes(20);
            try
            {
                NKeys = ReadByte();
                Unk = ReadByte();
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }
        }
    }

    internal sealed class PsAuthLogonProof : PacketServer
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="srp"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public PsAuthLogonProof(Srp6 srp, AccountState result) : base(AuthCMD.CMD_AUTH_LOGON_PROOF)
        {
            Write((byte) AuthCMD.CMD_AUTH_LOGON_PROOF);
            Write((byte) result);
            Write(srp.ServerProof.ToByteArray().Pad(20));
            this.WriteNullByte(4);
        }
    }
    #endregion

    #region CMD_AUTH_REALMLIST (TODO)
    internal sealed class PsAuthRealmList : PacketServer
    {
        /// <summary>
        /// @for
        ///   Type       : byte;
        ///   Flag       : byte;
        ///   Name       : byte;
        ///   Address    : array of byte;
        ///   Population : byte;            Pop {400F -> Full; 5F -> Medium; 1.6F -> Low; 200F -> New; 2F -> High}
        ///   Chars      : array of byte;
        ///   Time       : byte;
        ///   ?????      : byte;
        /// </summary>
        /// <param name="realms"></param>
        /// <param name="accountName"></param>
        /// <returns></returns>
        /// <todo>
        /// Count Population of Realm
        /// </todo>
        public PsAuthRealmList(List<Realms> realms, string accountName) : base(AuthCMD.CMD_AUTH_REALMLIST)
        {
            Write((uint) 0x0000);
            Write((byte) realms.Count);

            foreach (var realm in realms)
            {
                int count = MainProgram.Database.GetCharactersUsers(realm.Id, accountName);

                Write((uint) realm.type);
                Write((byte) realm.flag);
                WriteCString(realm.name);
                WriteCString(realm.address);
                Write(1.6f);
                Write((byte) count);
                Write((byte) realm.timezone);
                Write((byte) 0x01);
            }

            Write((UInt16)0x0002);
        }
    }
    #endregion

    internal class AuthServerHandler
    {
        private static Users _user;

        internal static void OnAuthLogonChallenge(AuthServerSession session, AuthLogonChallenge packet)
        {
            Log.Print(LogType.AuthServer,
                $"[{session.ConnectionSocket.RemoteEndPoint}] CMD_AUTH_LOGON_CHALLENGE [{packet.Username}], " +
                $"WoW Version [{packet.Version}.{packet.Build}] {packet.Language}");

            var dataResponse = new byte[2];
            dataResponse[0] = (byte) AuthCMD.CMD_AUTH_AUTHENTIFICATOR;

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

                        if(_user != null)
                            accState = _user.bannet_at != null ? AccountState.BANNED : AccountState.OK;
                        else
                            accState = AccountState.UNKNOWN_ACCOUNT;
                    }
                    catch (Exception)
                    {
                        accState = AccountState.DBBUSY;
                    }

                    switch (accState)
                    {
                        case AccountState.OK:
                            Log.Print(LogType.AuthServer, $"[{session.ConnectionSocket.RemoteEndPoint}] Account found [{packet.Username}]");
                            session.AccountName = _user?.username;
                            if (_user != null) session.Srp = new Srp6(_user.username.ToUpper(), _user.password.ToUpper());
                            session.SendData(new PsAuthLogonChallange(session.Srp, AccountState.OK));
                            return;
                        case AccountState.UNKNOWN_ACCOUNT:
                            dataResponse[1] = (byte)AccountState.UNKNOWN_ACCOUNT;
                            break;
                        case AccountState.BANNED:
                            dataResponse[1] = (byte)AccountState.BANNED;
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
                    dataResponse[1] = (byte)AccountState.BADVERSION;
                    break;
            }

            session.SendData(dataResponse, $"CMD_AUTH_LOGON_CHALLENGE [{dataResponse[1]}]");
        }

        internal static void OnAuthLogonProof(AuthServerSession session, AuthLogonProof handler)
        {
            session.Srp.ClientEphemeral = handler.A.ToPositiveBigInteger();
            session.Srp.ClientProof     = handler.M1.ToPositiveBigInteger();

            if(session.Srp.ClientProof == session.Srp.GenerateClientProof())
            {
                MainProgram.Database.SetSessionKey(session.AccountName, session.Srp.SessionKey.ToProperByteArray());
                session.SendData(new PsAuthLogonProof(session.Srp, AccountState.OK));
                return;
            }

            var dataResponse = new byte[2];
            dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
            dataResponse[1] = (byte)AccountState.UNKNOWN_ACCOUNT;
            session.SendData(dataResponse, "RS_LOGON_PROOF-WRONGPASS");
        }

        /// <summary>
        /// Send packet Realm List
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