using System;

namespace RealmServer.Helpers
{
    public class TutorialFlags
    {
        internal TutorialFlags(byte[] flagData)
        {
            if (flagData.Length != 32)
                throw new ArgumentOutOfRangeException(nameof(flagData), @"byte array must be 32 bytes");
            FlagData = flagData;
        }

        internal byte[] FlagData { get; }

        public void SetFlag(uint flagIndex)
        {
            FlagData[flagIndex / 8U] |= (byte) (1 << ((int) flagIndex % 8));
        }

        public void ClearFlags()
        {
            for (var index = 0; index < 32; ++index)
                FlagData[index] = byte.MaxValue;
        }

        public void ResetFlags()
        {
            for (var index = 0; index < 32; ++index)
                FlagData[index] = 0;
        }
    }
}
