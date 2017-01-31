using Common.Crypt;
using Common.Globals;
using Common.Network;
using Framework.Helpers;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace RealmServer
{
    internal class RealmServerSession
    {
        public VanillaCrypt Crypt;

        public const int BufferSize = 2048 * 2;

        public int ConnectionId { get; private set; }
        public Socket ConnectionSocket { get; }
        public byte[] DataBuffer { get; }

        internal RealmServerSession(int connectionId, Socket connectionSocket)
        {
            ConnectionId = connectionId;
            ConnectionSocket = connectionSocket;
            DataBuffer = new byte[BufferSize];

            try
            {
                Log.Print(LogType.RealmServer, $"Incoming connection from [{ConnectionSocket.RemoteEndPoint}]");
                ConnectionSocket.BeginReceive(DataBuffer, 0, DataBuffer.Length, SocketFlags.None, DataArrival, null);
                SendPacket(new RealmServerHandler.SmsgAuthChallenge());
            }
            catch (SocketException e)
            {
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
                Disconnect();
            }
        }

        private void Disconnect()
        {
            try
            {
                Log.Print(LogType.RealmServer, "User Disconnected");
                ConnectionSocket.Shutdown(SocketShutdown.Both);
                ConnectionSocket.Close();
            }
            catch (Exception e)
            {
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
            }
        }

        public virtual void DataArrival(IAsyncResult asyncResult)
        {
            int bytesRecived = 0;

            try
            {
                bytesRecived = ConnectionSocket.EndReceive(asyncResult);
            }
            catch (Exception e)
            {
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
            }

            if (bytesRecived != 0)
            {
                byte[] data = new byte[bytesRecived];
                Array.Copy(DataBuffer, data, bytesRecived);

                OnPacket(data);

                try
                {
                    ConnectionSocket.BeginReceive(DataBuffer, 0, DataBuffer.Length, SocketFlags.None, DataArrival, null);
                }
                catch (SocketException e)
                {
                    Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
                    ConnectionSocket.Close();
                }
                catch (Exception e)
                {
                    Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
                }
            }
            else
            {
                Disconnect();
            }
        }

        internal void SendData(byte[] send, string v)
        {
            byte[] buffer = new byte[send.Length];
            Buffer.BlockCopy(send, 0, buffer, 0, send.Length);

            try
            {
                ConnectionSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, delegate { }, null);
            }
            catch (SocketException e)
            {
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
                Disconnect();
            }
            catch (NullReferenceException e)
            {
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
                Disconnect();
            }
            catch (ObjectDisposedException e)
            {
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
                Disconnect();
            }
        }

        internal void SendData(PacketServer packet)
        {
            SendData(packet.Packet, packet.Opcode.ToString());
        }

        internal static void DumpPacket(byte[] data, RealmServerSession client)
        {
            int j;
            string buffer = "";

            if (client == null)
                Log.Print($"DEBUG: Packet Dump");
            else
                Log.Print($"[{client.ConnectionSocket.RemoteEndPoint}] DEBUG: Packet Dump");

            if (data.Length % 16 == 0)
            {
                for (j = 0; j <= data.Length - 1; j += 16)
                {
                    Log.Print($"| {BitConverter.ToString(data, j, 16).Replace("-", " ")} | " +
                              Encoding.ASCII.GetString(data, j, 16)
                                  .Replace("\t", "?")
                                  .Replace("\b", "?")
                                  .Replace("\r", "?")
                                  .Replace("\f", "?")
                                  .Replace("\n", "?") + " |");
                }
            }
            else
            {
                for (j = 0; j <= data.Length - 1 - 16; j += 16)
                {
                    Log.Print($"| {BitConverter.ToString(data, j, 16).Replace("-", " ")} | " +
                              Encoding.ASCII.GetString(data, j, 16)
                                  .Replace("\t", "?")
                                  .Replace("\b", "?")
                                  .Replace("\r", "?")
                                  .Replace("\f", "?")
                                  .Replace("\n", "?") + " |");
                }

                Log.Print($"| {BitConverter.ToString(data, j, data.Length % 16).Replace("-", " ")} " +
                          $"{buffer.PadLeft((16 - data.Length % 16) * 3, ' ')}" +
                          "| " + Encoding.ASCII.GetString(data, j, data.Length % 16)
                              .Replace("\t", "?")
                              .Replace("\b", "?")
                              .Replace("\r", "?")
                              .Replace("\f", "?")
                              .Replace("\n", "?") +
                          $"{buffer.PadLeft((16 - data.Length % 16), ' ')}|");
            }
        }

        internal void SendPacket(PacketServer packet)
        {
            SendPacket((byte)packet.Opcode, packet.Packet);
        }

        internal void SendPacket(byte opcode, byte[] data)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write(opcode);
            writer.Write((ushort)data.Length);
            writer.Write(data);
            SendData(((MemoryStream)writer.BaseStream).ToArray(), opcode.ToString());
        }

        private void OnPacket(byte[] data)
        {
            for (int index = 0; index < data.Length; index++)
            {
                byte[] headerData = new byte[6];
                Array.Copy(data, index, headerData, 0, 6);

                ushort length;
                short opcode;

                Decode(headerData, out length, out opcode);

                RealmCMD code = (RealmCMD)opcode;
                byte[] packetDate = new byte[length];
                Array.Copy(data, index + 6, packetDate, 0, length - 4);
                try
                {
                    RealmServerRouter.CallHandler(this, code, packetDate);
                } catch (Exception e)
                {
                    Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
                    DumpPacket(data, this);
                }

                index += 2 + (length - 1);
            }
        }

        private void Decode(byte[] header, out ushort length, out short opcode)
        {
            Crypt?.Decrypt(header, 6);

            if (Crypt == null)
            {
                length = BitConverter.ToUInt16(new[] { header[1], header[0] }, 0);
                opcode = BitConverter.ToInt16(header, 2);
            }
            else
            {
                length = BitConverter.ToUInt16(new[] { header[1], header[0] }, 0);
                opcode = BitConverter.ToInt16(new[] { header[2], header[3] }, 0);
            }
        }
    }
}
