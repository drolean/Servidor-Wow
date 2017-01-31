namespace Common.Crypt
{
    public class VanillaCrypt
    {
        protected bool Initialized;
        public byte SendI, SendJ, RecvI, RecvJ;
        protected byte[] Key;

        public byte[] Decrypt(byte[] data, int length)
        {
            if (!Initialized) return data;

            for (int t = 0; t < length; t++)
            {
                RecvI %= (byte)Key.Length;
                byte x = (byte)((data[t] - RecvJ) ^ Key[RecvI]);
                ++RecvI;
                RecvJ = data[t];
                data[t] = x;
            }
            return data;
        }

        public byte[] Encrypt(byte[] data)
        {
            if (!Initialized) return data;

            for (int t = 0; t < data.Length; t++)
            {
                SendI %= (byte)Key.Length;
                byte x = (byte)((data[t] ^ Key[SendI]) + SendJ);
                ++SendI;
                data[t] = SendJ = x;
            }
            return data;
        }

        public void Init(byte[] key)
        {
            Key = key;
            SendI = SendJ = RecvI = RecvJ = 0;
            Initialized = true;
        }
    }
}
