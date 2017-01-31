using System;
using System.Net.Sockets;
using System.Text;
using Common.Globals;
using Framework.Helpers;

namespace AuthServer
{
    internal class AuthServerSession
    {
        public const int BufferSize = 2048 * 2;

        public int ConnectionId { get; private set; }
        public Socket ConnectionSocket { get; }
        public byte[] DataBuffer { get; }

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
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
                Disconnect();
            }
        }

        private void Disconnect()
        {
            try
            {
                Log.Print(LogType.AuthServer, "User Disconnected");
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

        private void OnPacket(byte[] data)
        {
            short opcode = BitConverter.ToInt16(data, 0);

            #if DEBUG
            Log.Print(LogType.AuthServer, $"Data Received: {opcode:X2} ({opcode})");
            #endif

            try
            {
                AuthCMD code = (AuthCMD)opcode;
                AuthServerRouter.CallHandler(this, code, data);
            }
            catch (Exception e)
            {
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
                DumpPacket(data, this);
            }
        }

        internal void SendData(byte[] send, string v)
        {
            byte[] buffer = new byte[send.Length];
            Buffer.BlockCopy(send, 0, buffer, 0, send.Length);

            try
            {
                ConnectionSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, delegate { }, null);
                Log.Print(LogType.AuthServer, $"[{ConnectionSocket.RemoteEndPoint}] ({v}) Server -> Client");
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

        public static void DumpPacket(byte[] data, AuthServerSession client)
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
                    Log.Print($"| {BitConverter.ToString(data, j, 16).Replace("-", " ")} |" +
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
                    Log.Print($"| {BitConverter.ToString(data, j, 16).Replace("-", " ")} |" +
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
    }

}
