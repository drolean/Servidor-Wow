using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Common.Crypt;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;

namespace AuthServer
{
    internal class AuthServerSession
    {
        public const int BufferSize = 2048 * 2;
        public Srp6 Srp;

        public AuthServerSession(int connectionId, Socket connectionSocket)
        {
            ConnectionId = connectionId;
            ConnectionSocket = connectionSocket;
            DataBuffer = new byte[BufferSize];

            try
            {
                ConnectionSocket.BeginReceive(DataBuffer, 0, DataBuffer.Length, SocketFlags.None, DataArrival, null);
            }
            catch (SocketException e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}" +
                    $"{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                Disconnect();
            }
        }

        public int ConnectionId { get; }
        public Socket ConnectionSocket { get; }
        public byte[] DataBuffer { get; }
        public Users User { get; set; }

        private void Disconnect()
        {
            try
            {
                ConnectionSocket.Shutdown(SocketShutdown.Both);
                ConnectionSocket.Close();
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}" +
                    $"{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }
        }

        public virtual void DataArrival(IAsyncResult asyncResult)
        {
            var bytesRecived = 0;

            try
            {
                bytesRecived = ConnectionSocket.EndReceive(asyncResult);
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}" +
                    $"{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }

            if (bytesRecived != 0)
            {
                var data = new byte[bytesRecived];
                Array.Copy(DataBuffer, data, bytesRecived);

                OnPacket(data);

                try
                {
                    ConnectionSocket.BeginReceive(DataBuffer, 0, DataBuffer.Length, SocketFlags.None, DataArrival,
                        null);
                }
                catch (SocketException e)
                {
                    var trace = new StackTrace(e, true);
                    Log.Print(LogType.Error,
                        $"{e.Message}: {e.Source}" +
                        $"{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                    ConnectionSocket.Close();
                }
                catch (Exception e)
                {
                    var trace = new StackTrace(e, true);
                    Log.Print(LogType.Error,
                        $"{e.Message}: {e.Source}" +
                        $"{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                }
            }
            else
            {
                Disconnect();
            }
        }

        private void OnPacket(byte[] data)
        {
            var opcode = BitConverter.ToInt16(data, 0);

            try
            {
                var code = (AuthCMD) opcode;
                Log.Print(LogType.AuthServer,
                    $"[{ConnectionSocket.RemoteEndPoint}] [<= RCVD] [{code.ToString().PadRight(25, ' ')}] = {data.Length}");
                AuthServerRouter.CallHandler(this, code, data);
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}" +
                    $"{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                DumpPacket(data, this);
            }
        }

        internal void SendData(byte[] send, string v)
        {
            var buffer = new byte[send.Length];
            Buffer.BlockCopy(send, 0, buffer, 0, send.Length);

            try
            {
                Log.Print(LogType.AuthServer,
                    $"[{ConnectionSocket.RemoteEndPoint}] [SEND =>] [{((AuthCMD) int.Parse(v)).ToString().PadRight(25, ' ')}] = {send.Length}");

                ConnectionSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, delegate { }, null);
            }
            catch (SocketException e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}" +
                    $"{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                Disconnect();
            }
            catch (NullReferenceException e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}" +
                    $"{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                Disconnect();
            }
            catch (ObjectDisposedException e)
            {
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
                Disconnect();
            }
        }

        public void SendData(Common.Network.PacketServer packet)
        {
            SendData(packet.Packet, packet.Opcode.ToString());
        }

        public static void DumpPacket(byte[] data, AuthServerSession client)
        {
            int j;
            const string buffer = "";

            Log.Print(client == null
                ? "DEBUG: Packet Dump"
                : $"[{client.ConnectionSocket.RemoteEndPoint}] DEBUG: Packet Dump");

            if (data.Length % 16 == 0)
            {
                for (j = 0; j <= data.Length - 1; j += 16)
                    Log.Print($"| {BitConverter.ToString(data, j, 16).Replace("-", " ")} | " +
                              Encoding.ASCII.GetString(data, j, 16)
                                  .Replace("\t", "?")
                                  .Replace("\b", "?")
                                  .Replace("\r", "?")
                                  .Replace("\f", "?")
                                  .Replace("\n", "?") + " |");
            }
            else
            {
                for (j = 0; j <= data.Length - 1 - 16; j += 16)
                    Log.Print($"| {BitConverter.ToString(data, j, 16).Replace("-", " ")} | " +
                              Encoding.ASCII.GetString(data, j, 16)
                                  .Replace("\t", "?")
                                  .Replace("\b", "?")
                                  .Replace("\r", "?")
                                  .Replace("\f", "?")
                                  .Replace("\n", "?") + " |");

                Log.Print($"| {BitConverter.ToString(data, j, data.Length % 16).Replace("-", " ")} " +
                          $"{buffer.PadLeft((16 - data.Length % 16) * 3, ' ')}" +
                          "| " + Encoding.ASCII.GetString(data, j, data.Length % 16)
                              .Replace("\t", "?")
                              .Replace("\b", "?")
                              .Replace("\r", "?")
                              .Replace("\f", "?")
                              .Replace("\n", "?") +
                          $"{buffer.PadLeft(16 - data.Length % 16, ' ')}|");
            }
        }

        internal void SendPacket(Common.Network.PacketServer packet)
        {
            SendPacket((byte) packet.Opcode, packet.Packet);
        }

        internal void SendPacket(byte opcode, byte[] data)
        {
            if (!ConnectionSocket.Connected)
                return;

            try
            {
                Log.Print(LogType.AuthServer,
                    $"[{ConnectionSocket.RemoteEndPoint}] [SEND =>] [{((AuthCMD) opcode).ToString().PadRight(25, ' ')}] = {data.Length}");
                var writer = new BinaryWriter(new MemoryStream());
                writer.Write(opcode);
                writer.Write((ushort) data.Length);
                writer.Write(data);
                SendData(((MemoryStream) writer.BaseStream).ToArray(), opcode.ToString());
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}" +
                    $"{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                Disconnect();
            }
        }
    }
}