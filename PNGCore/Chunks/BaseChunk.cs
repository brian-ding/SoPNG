using PNGCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGCore.Chunks
{
    internal abstract class BaseChunk
    {
        protected static ulong[] _crcTable;
        protected byte[] _data;
        protected abstract ChunkName Name { get; }
        protected int Length { get; }

        static BaseChunk()
        {
            _crcTable = new ulong[256];
            for (int i = 0; i < _crcTable.Length; i++)
            {
                ulong c = (ulong)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((c & 1) == 1)
                    {
                        c = 0xedb88320L ^ (c >> 1);
                    }
                    else
                    {
                        c = c >> 1;
                    }
                }
                _crcTable[i] = c;
            }
        }

        public BaseChunk(int length)
        {
            Length = length;
            _data = new byte[4 + 4 + Length + 4];
            BitOperation.Int32ToBytes(Length).CopyTo(_data, 0);
            Name.FillChunk(_data);
        }

        protected byte[] GetCRC(byte[] data)
        {
            ulong c = 0xffffffffL;
            for (int i = 0; i < data.Length; i++)
            {
                c = _crcTable[(c ^ data[i]) & 0xff] ^ (c >> 8);
            }
            c = c ^ 0xffffffffL;

            return BitOperation.Int32ToBytes((int)c);
        }

        public byte[] GetData()
        {
            return _data;
        }
    }
}