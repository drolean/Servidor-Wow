namespace Common.Crypt
{
    public class VanillaCrypt
    {
        protected bool Initialized;
        protected byte[] Key;
        public byte SendI, SendJ, RecvI, RecvJ;

        public byte[] Decrypt(byte[] data, int length)
        {
            if (!Initialized) return data;

            for (var t = 0; t < length; t++)
            {
                RecvI %= (byte) Key.Length;
                var x = (byte) ((data[t] - RecvJ) ^ Key[RecvI]);
                ++RecvI;
                RecvJ = data[t];
                data[t] = x;
            }

            return data;
        }

        public byte[] Encrypt(byte[] data)
        {
            if (!Initialized) return data;

            for (var t = 0; t < data.Length; t++)
            {
                SendI %= (byte) Key.Length;
                var x = (byte) ((data[t] ^ Key[SendI]) + SendJ);
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