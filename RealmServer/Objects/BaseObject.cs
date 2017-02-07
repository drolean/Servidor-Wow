using System;
using System.Collections;

namespace RealmServer.Objects
{
    public class BaseObject
    {
        /* AQUI VAI OS GLOBAL MUDAR DEPOIS PARA GLOBAL EM OUTRO ARQUIVO DE CONF */
        public const float DefaultDistanceVisible = 155.8f;
        public static int DefaultMaxLevel = 60;                 // Max Player Level
        public static int[] XPTable = new int[DefaultMaxLevel + 1];    // Max XPTable Level from Database
        /* AQUI VAI OS GLOBAL MUDAR DEPOIS PARA GLOBAL EM OUTRO ARQUIVO DE CONF */

        public int UpdateCount { get; private set; }
        public int DataLength { get; set; }

        public BitArray Mask { get; }
        public Hashtable UpdateData { get; }

        public BaseObject()
        {
            UpdateData = new Hashtable();
            Mask = new BitArray(DataLength, false); 
        }

        // [START] Definições 
        public ulong GUID = 0;
        // [END] Definições

        public void SetUpdateField<T>(int index, T value, byte offset = 0)
        {
            UpdateCount++;
            switch (value.GetType().Name)
            {
                case "SByte":
                case "Int16":
                    {
                        Mask.Set(index, true);

                        if (UpdateData.ContainsKey(index))
                            UpdateData[index] = (int)UpdateData[index] |
                                                (int)Convert.ChangeType(value, typeof(int)) <<
                                                (offset * (value.GetType().Name == "Byte" ? 8 : 16));
                        else
                            UpdateData[index] = (int)Convert.ChangeType(value, typeof(int)) <<
                                                (offset * (value.GetType().Name == "Byte" ? 8 : 16));

                        break;
                    }
                case "Byte":
                case "UInt16":
                    {
                        Mask.Set(index, true);

                        if (UpdateData.ContainsKey(index))
                            UpdateData[index] = (uint)UpdateData[index] |
                                                (uint)Convert.ChangeType(value, typeof(uint)) <<
                                                (offset * (value.GetType().Name == "Byte" ? 8 : 16));
                        else
                            UpdateData[index] = (uint)Convert.ChangeType(value, typeof(uint)) <<
                                                (offset * (value.GetType().Name == "Byte" ? 8 : 16));

                        break;
                    }
                case "Int64":
                    {
                        Mask.Set(index, true);
                        Mask.Set(index + 1, true);

                        long tmpValue = (long)Convert.ChangeType(value, typeof(long));

                        UpdateData[index] = (uint)(tmpValue & int.MaxValue);
                        UpdateData[index + 1] = (uint)((tmpValue >> 32) & int.MaxValue);

                        break;
                    }
                case "UInt64":
                    {
                        Mask.Set(index, true);
                        Mask.Set(index + 1, true);

                        ulong tmpValue = (ulong)Convert.ChangeType(value, typeof(ulong));

                        UpdateData[index] = (uint)(tmpValue & uint.MaxValue);
                        UpdateData[index + 1] = (uint)((tmpValue >> 32) & uint.MaxValue);

                        break;
                    }
                default:
                    {
                        Mask.Set(index, true);
                        UpdateData[index] = value;

                        break;
                    }
            }
        }
    }
}
