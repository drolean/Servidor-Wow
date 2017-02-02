using System;
using System.Collections.Generic;
using System.Net;
using Common.Crypt;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using Common.Network;

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
                Username = Username + (char) data[34 + i];
            }
        }
    }
    #endregion

    #region CMD_AUTH_LOGON_PROOF
    public sealed class AuthLogonProof : PacketReader
    {
        public byte OptCode { get; private set; }
        public byte[] A { get; private set; }
        public byte[] M1 { get; private set; }
        public byte[] CrcHash { get; private set; }
        public byte NKeys { get; private set; }
        public byte Unk { get; private set; }

        public AuthLogonProof(byte[] data) : base(data)
        {
            OptCode = ReadByte();
            A       = ReadBytes(32);
            M1      = ReadBytes(20);

            CrcHash = ReadBytes(20);
            NKeys   = ReadByte();
            Unk     = ReadByte();
        }
    }
    #endregion

    sealed class PsAuthLogonChallange : PacketServer
    {
        public PsAuthLogonChallange(Srp6 srp, AccountState result) : base(AuthCMD.CMD_AUTH_LOGON_CHALLENGE)
        {
            Write((byte) AuthCMD.CMD_AUTH_LOGON_CHALLENGE);
            Write((byte) result);
            Write((byte)0); // unkown1 is set to 0 by all private servers.
            Write(srp.ServerEphemeral.ToProperByteArray()); // SRP6 server public ephemeral
            Write((byte)1); // generator_len is the length of the generator field following it. All servers (including ours) set this to 1.
            Write(srp.Generator.ToByteArray()); // All servers (including ours) hardcode this to 7
            Write((byte)32); // All servers (including ours) set this to 32.
            Write(srp.Modulus.ToProperByteArray().Pad(32)); // All servers (including ours) set this to 0x894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7
            Write(srp.Salt.ToProperByteArray().Pad(32)); // A salt is a randomly generated value used to strengthen a user's password against attacks where pre-computations are performed
            this.WriteNullByte(17); // unknown2 is set to 16 random bytes by all servers.
        }
    }

    sealed class PsAuthLogonProof : PacketServer
    {
        public PsAuthLogonProof(Srp6 srp, AccountState result) : base(AuthCMD.CMD_AUTH_LOGON_PROOF)
        {
            Write((byte) AuthCMD.CMD_AUTH_LOGON_PROOF);
            Write((byte) result);
            Write(srp.ServerProof.ToByteArray().Pad(20));
            this.WriteNullByte(4);
        }
    }

    sealed class PsAuthRealmList : PacketServer
    {
        public PsAuthRealmList(List<Realms> realms) : base(AuthCMD.CMD_AUTH_REALMLIST)
        {
            Write((uint) 0x0000);
            Write((byte) realms.Count);

            foreach (var realm in realms)
            {
                Write((uint) realm.type);         // Type
                Write((byte) realm.flag);         // Flag
                this.WriteCString(realm.name);    // Name World
                this.WriteCString(realm.address); // IP World
                Write(0.5f);                      // Pop
                Write((byte) 0x00);               // Chars
                Write((byte) realm.timezone);     // time
                Write((byte) 0x01);               // ?????  
            }

            Write((UInt16)0x0002);
        }
    }

    internal class AuthServerHandler
    {
        private static Users _user;

        internal static void OnAuthLogonChallenge(AuthServerSession session, AuthLogonChallenge packet)
        {
            Log.Print(LogType.AuthServer,
                $"[{session.ConnectionSocket.RemoteEndPoint}] CMD_AUTH_LOGON_CHALLENGE [{packet.Username}], WoW Version [{packet.Version}.{packet.Build}]");

            byte[] dataResponse;

            if (packet.Version == null)
            {
                Log.Print(LogType.Error, $"[{session.ConnectionSocket.RemoteEndPoint}] Invalid Client");

                dataResponse = new byte[2];
                dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                dataResponse[1] = (byte)AccountState.BADVERSION;
                session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_BADVERSION]");
            }
            else if (packet.Version == "1.12.1")
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
                        session.Srp = new Srp6(_user.username.ToUpper(), _user.password.ToUpper());
                        session.SendData(new PsAuthLogonChallange(session.Srp, AccountState.OK));
                        break;
                    case AccountState.UNKNOWN_ACCOUNT:
                        dataResponse = new byte[2];
                        dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                        dataResponse[1] = (byte)AccountState.UNKNOWN_ACCOUNT;
                        session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_UNKNOWN_ACCOUNT]");
                        break;
                    case AccountState.BANNED:
                        dataResponse = new byte[2];
                        dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                        dataResponse[1] = (byte)AccountState.BANNED;
                        session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_BANNED]");
                        break;
                    case AccountState.NOTIME:
                        dataResponse = new byte[2];
                        dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                        dataResponse[1] = (byte)AccountState.NOTIME;
                        session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_NOTIME]");
                        break;
                    case AccountState.ALREADYONLINE:
                        dataResponse = new byte[2];
                        dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                        dataResponse[1] = (byte)AccountState.ALREADYONLINE;
                        session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_ALREADYONLINE]");
                        break;
                    default:
                        dataResponse = new byte[2];
                        dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                        dataResponse[1] = (byte)AccountState.FAILED;
                        session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_FAILED]");
                        break;
                }
            }
            else
            {
                Log.Print(LogType.Error, $"[{session.ConnectionSocket.RemoteEndPoint}] Wrong Version [{packet.Version}.{packet.Build}]");

                dataResponse = new byte[2];
                dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                dataResponse[1] = (byte)AccountState.BADVERSION;
                session.SendData(dataResponse, "CMD_AUTH_LOGON_CHALLENGE [LOGIN_BADVERSION]");
            }
        }

        internal static void OnAuthLogonProof(AuthServerSession session, AuthLogonProof handler)
        {
            Log.Print(LogType.AuthServer,
                $"[{session.ConnectionSocket.RemoteEndPoint}] CMD_AUTH_LOGON_PROOF");

            session.Srp.ClientEphemeral = handler.A.ToPositiveBigInteger();
            session.Srp.ClientProof     = handler.M1.ToPositiveBigInteger();

            if(session.Srp.ClientProof == session.Srp.GenerateClientProof())
            {
                MainProgram.Database.SetSessionKey(session.AccountName, session.Srp.SessionKey.ToProperByteArray());
                session.SendData(new PsAuthLogonProof(session.Srp, AccountState.OK));
            } else
            {
                var dataResponse = new byte[2];
                dataResponse[0] = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
                dataResponse[1] = (byte)AccountState.UNKNOWN_ACCOUNT;
                session.SendData(dataResponse, "RS_LOGON_PROOF-WRONGPASS");
            }
        }

        internal static void OnAuthRealmList(AuthServerSession session, byte[] data)
        {
            Log.Print(LogType.AuthServer, $"[{session.ConnectionSocket.RemoteEndPoint}] CMD_AUTH_REALMLIST");

            // Get Realms
            var realms = MainProgram.Database.GetRealms();
            session.SendPacket(new PsAuthRealmList(realms));
        }
    }
}