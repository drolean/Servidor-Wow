using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database
{
    public class DBCStore<T> where T : DBCRecordBase, new()
    {
        UInt32 NumRecords = 0;
        UInt32 NumFields = 0;
        UInt32 RecordSize = 0;
        UInt32 StringDataSize = 0;
        byte[] StringData = null;

        public Dictionary<int, T> RecordDataIndexed = null;
        public List<T> RecordData = null;

        public async Task Load(string path)
        {
            await Task.Factory.StartNew(() => { LoadImpl(path); });
        }

        void LoadImpl(string path)
        {
            using (var fileStream = new FileStream("dbc\\" + path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(fileStream))
                {
                    var header = reader.ReadUInt32();

                    var header_string = Encoding.Default.GetString(BitConverter.GetBytes(header));

                    if (header_string != "WDBC")
                        throw new Exception("{0} has incorrect fourCC identifier");

                    NumRecords = reader.ReadUInt32();
                    NumFields = reader.ReadUInt32();
                    RecordSize = reader.ReadUInt32();
                    StringDataSize = reader.ReadUInt32();

                    fileStream.Position = fileStream.Length - StringDataSize;
                    StringData = reader.ReadBytes((int)StringDataSize);
                    fileStream.Position = 20;

                    for (int i = 0; i < NumRecords; ++i)
                    {
                        var rec = new T();

                        rec.SetRecordData(reader.ReadBytes((int)RecordSize));
                        rec.SetStringData(StringData);

                        int index = rec.Read();

                        if (index == -1)
                            index = i;

                        Add(index, rec);
                    }
                }
            }
        }

        public void Add(int index, T item)
        {
            if (RecordDataIndexed == null)
                RecordDataIndexed = new Dictionary<int, T>();
            RecordDataIndexed.Add(index, item);
        }

        public T Get(int index)
        {
            T ret = default(T);
            RecordDataIndexed.TryGetValue(index, out ret);
            return ret;
        }
    }

    public class DBCRecordBase
    {
        public DBCRecordBase() { }

        byte[] RecordData = null;
        byte[] StringData = null;

        public virtual int Read() { return -1; }

        public void SetRecordData(byte[] data) { RecordData = data; }
        public void SetStringData(byte[] data) { StringData = data; }

        protected UInt32 GetUInt32(int field)
        {
            return BitConverter.ToUInt32(RecordData, field * 4);
        }
        protected Int32 GetInt32(int field)
        {
            return BitConverter.ToInt32(RecordData, field * 4);
        }
        protected float GetFloat(int field)
        {
            return BitConverter.ToSingle(RecordData, field * 4);
        }
        protected UInt64 GetUInt64(int field)
        {
            return BitConverter.ToUInt64(RecordData, field * 4);
        }

        protected string GetString(int field)
        {
            int index = GetInt32(field);
            int len = 0;
            while (StringData[index + len++] != 0)
            {
            }

            return Encoding.Default.GetString(StringData, index, len - 1);
        }
    }
}
