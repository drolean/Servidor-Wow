using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Common.Crypt
{
    public class Srp6
    {
        private static readonly RNGCryptoServiceProvider MRng = new RNGCryptoServiceProvider();

        public byte[] K;
        public byte[] S;

        public Srp6(string identifier, BigInteger salt, BigInteger verifier)
        {
            Identifier = identifier;
            Salt = salt;
            Verifier = verifier;

            PrivateServerEphemeral = GetRandomNumber(19) % Modulus;
        }

        public Srp6(string identifier, string password)
        {
            Identifier = identifier;
            Salt = GetRandomNumber(32) % Modulus;
            Verifier = GenerateVerifier(identifier, password);

            PrivateServerEphemeral = GetRandomNumber(19) % Modulus;
        }

        public string Identifier { get; }

        public BigInteger Modulus =>
            BigInteger.Parse("0894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7",
                NumberStyles.HexNumber);

        public BigInteger Generator { get; } = 7;

        public BigInteger Multiplier { get; } = 3;

        public BigInteger PrivateKey { get; internal set; }

        public BigInteger Verifier { get; }

        public BigInteger PrivateServerEphemeral { get; }

        public BigInteger Salt { get; }

        public BigInteger ClientEphemeral { get; set; }

        public BigInteger ServerEphemeral =>
            (Multiplier * Verifier + BigInteger.ModPow(Generator, PrivateServerEphemeral, Modulus)) % Modulus;

        public BigInteger SessionKey =>
            GenerateSessionKey(ClientEphemeral, ServerEphemeral, PrivateServerEphemeral, Modulus, Verifier);

        public BigInteger ClientProof { get; set; }

        public BigInteger ServerProof => GenerateServerProof(ClientEphemeral, ClientProof, SessionKey);

        public BigInteger GenerateClientProof()
        {
            return GenerateClientProof(Identifier, Modulus, Generator, Salt, SessionKey, ClientEphemeral,
                ServerEphemeral);
        }

        private BigInteger GenerateVerifier(string identifier, string password)
        {
            return GenerateVerifier(identifier, password, Modulus, Generator, Salt);
        }

        #region Static members

        private static BigInteger GenerateVerifier(string identifier, string password, BigInteger modulus,
            BigInteger generator, BigInteger salt)
        {
            var privateKey = Hash(salt.ToProperByteArray(),
                Hash(Encoding.ASCII.GetBytes(identifier + ":" + password)).ToProperByteArray());
            return BigInteger.ModPow(generator, privateKey, modulus);
        }

        private static BigInteger GenerateScrambler(BigInteger a, BigInteger b)
        {
            return Hash(a.ToProperByteArray(), b.ToProperByteArray());
        }

        private static BigInteger GenerateClientProof(string identifier, BigInteger modulus, BigInteger generator,
            BigInteger salt, BigInteger sessionKey, BigInteger a, BigInteger b)
        {
            // M = H(H(N) xor H(g), H(I), s, A, B, K)
            var nHash = Sha1Hash(modulus.ToProperByteArray());
            var gHash = Sha1Hash(generator.ToProperByteArray());

            // H(N) XOR H(g)
            for (int i = 0, j = nHash.Length; i < j; i++)
                nHash[i] ^= gHash[i];

            return Hash(nHash, Sha1Hash(Encoding.ASCII.GetBytes(identifier)), salt.ToProperByteArray(),
                a.ToProperByteArray(), b.ToProperByteArray(), sessionKey.ToProperByteArray());
        }

        private static BigInteger GenerateSessionKey(BigInteger clientEphemeral, BigInteger serverEphemeral,
            BigInteger privateServerEphemeral, BigInteger modulus, BigInteger verifier)
        {
            return Interleave(BigInteger.ModPow(
                clientEphemeral *
                BigInteger.ModPow(verifier, GenerateScrambler(clientEphemeral, serverEphemeral), modulus),
                privateServerEphemeral, modulus));
        }

        private static BigInteger GenerateServerProof(BigInteger a, BigInteger clientProof, BigInteger sessionKey)
        {
            return Hash(a.ToProperByteArray(), clientProof.ToProperByteArray(), sessionKey.ToProperByteArray());
        }

        // http://www.ietf.org/rfc/rfc2945.txt
        // Chapter 3.1
        private static BigInteger Interleave(BigInteger sessionKey)
        {
            var T = sessionKey.ToProperByteArray().SkipWhile(b => b == 0).ToArray(); // Remove all leading 0-bytes
            if ((T.Length & 0x1) == 0x1) T = T.Skip(1).ToArray(); // Needs to be an even length, skip 1 byte if not
            var g = Sha1Hash(Enumerable.Range(0, T.Length).Where(i => (i & 0x1) == 0x0).Select(i => T[i]).ToArray());
            var h = Sha1Hash(Enumerable.Range(0, T.Length).Where(i => (i & 0x1) == 0x1).Select(i => T[i]).ToArray());

            var result = new byte[40];
            for (int i = 0, rC = 0; i < result.Length / 2; i++)
            {
                result[rC++] = g[i];
                result[rC++] = h[i];
            }

            return result.ToPositiveBigInteger();
        }

        private static BigInteger Hash(params byte[][] args)
        {
            return Sha1Hash(args.SelectMany(b => b).ToArray()).ToPositiveBigInteger();
        }

        #endregion

        #region Helper functions

        private static BigInteger GetRandomNumber(uint bytes)
        {
            var data = new byte[bytes];
            MRng.GetNonZeroBytes(data);
            return data.ToPositiveBigInteger();
        }

        private static byte[] Sha1Hash(byte[] bytes)
        {
            var sha1 = SHA1.Create();
            return sha1.ComputeHash(bytes);
        }

        #endregion
    }
}