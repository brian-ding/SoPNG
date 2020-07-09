using SoPNG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoPNG.Chunks
{
    internal struct ChunkName
    {
        private const int BYTE_LENGTH = 4;
        private byte[] _nameBytes;
        private char[] _nameChars;
        public string Name { get; }

        public ChunkName(byte[] name)
        {
            if (name.Length != BYTE_LENGTH)
            {
                throw new ArgumentOutOfRangeException("byte array's length must be 4 for chunk name");
            }

            _nameBytes = name;
            _nameChars = new char[BYTE_LENGTH] { (char)name[0], (char)name[1], (char)name[2], (char)name[3] };
            Name = new string(_nameChars);
        }

        public void FillChunk(byte[] data)
        {
            _nameBytes.CopyTo(data, 4);
        }
    }
}
