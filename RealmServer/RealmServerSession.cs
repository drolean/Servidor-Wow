using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Common.Globals;
using Common.Helpers;
using Common.Network;

namespace RealmServer
{
    public class RealmServerSession
    {
        public const int BufferSize = 2048 * 2;

        public RealmServerSession(int connectionId, Socket connectionSocket)
        {
            ConnectionId = connectionId;
            ConnectionSocket = connectionSocket;
            DataBuffer = new byte[BufferSize];

            try
            {
                Log.Print(LogType.RealmServer, $"Incoming connection from [{ConnectionRemoteIp}]");
                ConnectionSocket.BeginReceive(DataBuffer, 0, DataBuffer.Length, SocketFlags.None, DataArrival, null);
            }
            catch (SocketException e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                Disconnect();
            }

            Thread.Sleep(500);

            // Connection Packet
            using (var packet = new PacketServer(RealmEnums.SMSG_AUTH_CHALLENGE))
            {
                //packet.WriteBytes(new byte[] {0x33, 0x18, 0x34, 0xC8});
                packet.WriteBytes(new byte[] {1, 2, 3, 4});
                SendPacket(packet);
            }
        }

        public string ConnectionRemoteIp => ConnectionSocket.RemoteEndPoint.ToString();

        public int ConnectionId { get; }
        public Socket ConnectionSocket { get; }
        public byte[] DataBuffer { get; }

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
                    $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
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
                    $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
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
                        $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                    ConnectionSocket.Close();
                }
                catch (Exception e)
                {
                    var trace = new StackTrace(e, true);
                    Log.Print(LogType.Error,
                        $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                }
            }
            else
            {
                Disconnect();
            }
        }

        private void OnPacket(byte[] data)
        {
            try
            {
                for (var index = 0; index < data.Length; index++)
                {
                    var headerData = new byte[6];
                    Array.Copy(data, index, headerData, 0, 6);

                    Utils.Decode(headerData, out var length, out var opcode);
                    Log.Print(LogType.RealmServer,
                        $"[{ConnectionSocket.RemoteEndPoint}] [RCVD] [{((RealmEnums) opcode).ToString().PadRight(25, ' ')}] = {length}");
                    var code = (RealmEnums) opcode;

                    var packetDate = new byte[length];
                    Array.Copy(data, index + 6, packetDate, 0, length - 4);
                    RealmServerRouter.CallHandler(this, code, packetDate);

                    index += 2 + (length - 1);
                }
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                DumpPacket(data, this);
            }
        }

        public static void DumpPacket(byte[] data, RealmServerSession client)
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

        internal void SendPacket(PacketServer packet)
        {
            Log.LogPacket(LogPacket.Sending, packet);
            SendPacket(packet.Opcode, packet.Packet);
        }

        internal void SendPacket(int opcode, byte[] data)
        {
            Log.Print(LogType.RealmServer,
                $"[{ConnectionSocket.RemoteEndPoint}] [SEND] [{((RealmEnums) opcode).ToString().PadRight(25, ' ')}] = {data.Length}");
            var writer = new BinaryWriter(new MemoryStream());
            var header = Utils.Encode(data.Length, opcode);
            writer.Write(header);
            writer.Write(data);
            SendData(((MemoryStream) writer.BaseStream).ToArray());
        }

        internal void SendData(byte[] send)
        {
            var buffer = new byte[send.Length];
            Buffer.BlockCopy(send, 0, buffer, 0, send.Length);

            try
            {
                ConnectionSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, delegate { }, null);
            }
            catch (SocketException e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                Disconnect();
            }
            catch (NullReferenceException e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                Disconnect();
            }
            catch (ObjectDisposedException e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                Disconnect();
            }
        }
    }
}