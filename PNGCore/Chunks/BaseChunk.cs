using PNGCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGCore.Chunks
{
    public abstract class BaseChunk
    {
        protected static ulong[] _crcTable;

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
    }
}
