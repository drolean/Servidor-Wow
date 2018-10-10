using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using Common.Crypt;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using RealmServer.PacketServer;

namespace RealmServer
{
    public class RealmServerSession
    {
        public const int BufferSize = 2048 * 2;
        public static List<RealmServerSession> Sessions = new List<RealmServerSession>();

        internal RealmServerSession(int connectionId, Socket connectionSocket)
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

            // First Packet?
            SendPacket(new SMSG_AUTH_CHALLENGE());
        }

        public Socket ConnectionSocket { get; }
        public VanillaCrypt PacketCrypto { get; set; }
        public int ConnectionId { get; }
        public byte[] DataBuffer { get; }
        public string ConnectionRemoteIp => ConnectionSocket.RemoteEndPoint.ToString();

        //
        public Users Users { get; set; }

        private void Disconnect()
        {
            try
            {
                // TODO: Cannot access a disposed object.
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

        internal virtual void DataArrival(IAsyncResult asyncResult)
        {
            var bytesRecived = 0;

            try
            {
                // TODO: Foi Forçado o cancelamento de uma conexão pelo host remoto.
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
                    // TODO: Cannot access a disposed object.
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

                    Decode(headerData, out var length, out var opcode);
                    var code = (RealmEnums) opcode;

                    Log.Print(LogType.RealmServer,
                        $"[{ConnectionSocket.RemoteEndPoint}] [RCVD] [{code.ToString().PadRight(25, ' ')}] = {length}");

                    var packetDate = new byte[length];
                    // TODO: Source array was to not long enough. Check srcIndex and length, and the array's lower bound
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
                Utils.DumpPacket(data);
                Disconnect();
            }
        }

        internal void SendPacket(Common.Network.PacketServer packet)
        {
            Log.LogPacket(LogPacket.Sending, packet);
            SendPacket(packet.Opcode, packet.Packet);
        }

        private void SendPacket(int opcode, byte[] data)
        {
            if (!ConnectionSocket.Connected)
                return;

            try
            {
                Log.Print(LogType.RealmServer,
                    $"[{ConnectionSocket.RemoteEndPoint}] [SEND] [{((RealmEnums) opcode).ToString().PadRight(25, ' ')}] = {data.Length}");
                var writer = new BinaryWriter(new MemoryStream());
                var header = Encode(data.Length, opcode);
                writer.Write(header);
                writer.Write(data);
                SendData(((MemoryStream) writer.BaseStream).ToArray());
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
                Disconnect();
            }
        }

        internal void SendData(byte[] send)
        {
            var buffer = new byte[send.Length];
            Buffer.BlockCopy(send, 0, buffer, 0, send.Length);

            try
            {
                // TODO: Cannot access a disposed object.
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

        private byte[] Encode(int size, int opcode)
        {
            var index = 0;
            var newSize = size + 2;
            var header = new byte[4];
            if (newSize > 0x7FFF)
                header[index++] = (byte) (0x80 | (0xFF & (newSize >> 16)));

            header[index++] = (byte) (0xFF & (newSize >> 8));
            header[index++] = (byte) (0xFF & (newSize >> 0));
            header[index++] = (byte) (0xFF & opcode);
            header[index] = (byte) (0xFF & (opcode >> 8));

            if (PacketCrypto != null) header = PacketCrypto.Encrypt(header);

            return header;
        }

        private void Decode(byte[] header, out ushort length, out short opcode)
        {
            PacketCrypto?.Decrypt(header, 6);

            if (PacketCrypto == null)
            {
                length = BitConverter.ToUInt16(new[] {header[1], header[0]}, 0);
                opcode = BitConverter.ToInt16(header, 2);
            }
            else
            {
                length = BitConverter.ToUInt16(new[] {header[1], header[0]}, 0);
                opcode = BitConverter.ToInt16(new[] {header[2], header[3]}, 0);
            }
        }
    }
}