using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database
{
    public class DbcReader<T> where T : DbcRecordBase, new()
    {
        private uint _numRecords;
        private uint _recordSize;
        private uint _stringDataSize;
        private byte[] _stringData;

        public Dictionary<int, T> RecordDataIndexed;
        public List<T> RecordData = null;

        public async Task Load(string path)
        {
            await Task.Factory.StartNew(() => { LoadImpl(path); });
        }

        private void LoadImpl(string path)
        {
            using (var fileStream = new FileStream("dbc\\" + path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(fileStream))
                {
                    var header = reader.ReadUInt32();

                    var headerString = Encoding.Default.GetString(BitConverter.GetBytes(header));

                    if (headerString != "WDBC")
                        throw new Exception("{0} has incorrect fourCC identifier");

                    _numRecords = reader.ReadUInt32();
                    reader.ReadUInt32();
                    _recordSize = reader.ReadUInt32();
                    _stringDataSize = reader.ReadUInt32();

                    fileStream.Position = fileStream.Length - _stringDataSize;
                    _stringData = reader.ReadBytes((int)_stringDataSize);
                    fileStream.Position = 20;

                    for (int i = 0; i < _numRecords; ++i)
                    {
                        var rec = new T();

                        rec.SetRecordData(reader.ReadBytes((int)_recordSize));
                        rec.SetStringData(_stringData);

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
            T ret;
            RecordDataIndexed.TryGetValue(index, out ret);
            return ret;
        }
    }

    public class DbcRecordBase
    {
        private byte[] _recordData;
        private byte[] _stringData;

        public virtual int Read() { return -1; }

        public void SetRecordData(byte[] data) { _recordData = data; }
        public void SetStringData(byte[] data) { _stringData = data; }

        protected uint GetUInt32(int field)
        {
            return BitConverter.ToUInt32(_recordData, field * 4);
        }

        protected int GetInt32(int field)
        {
            return BitConverter.ToInt32(_recordData, field * 4);
        }

        protected float GetFloat(int field)
        {
            return BitConverter.ToSingle(_recordData, field * 4);
        }

        protected UInt64 GetUInt64(int field)
        {
            return BitConverter.ToUInt64(_recordData, field * 4);
        }

        protected string GetString(int field)
        {
            int index = GetInt32(field);
            int len = 0;
            while (_stringData[index + len++] != 0)
            {

            }

            return Encoding.Default.GetString(_stringData, index, len - 1);
        }
    }
}
