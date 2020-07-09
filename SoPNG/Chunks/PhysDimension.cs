using SoPNG.Extensions;
using SoPNG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoPNG.Chunks
{
    internal class PhysDimension : BaseChunk, IChunk
    {
        protected override ChunkName Name => new ChunkName(new byte[] { 112, 72, 89, 115 });

        public PhysDimension(int xAxis, int yAxis, byte unit) : base(9)
        {
            BitOperation.Int32ToBytes(xAxis).CopyTo(_data, 8);
            BitOperation.Int32ToBytes(yAxis).CopyTo(_data, 12);
            _data[16] = unit;
            GetCRC(_data.Skip(4).Take(4 + Length).ToArray()).CopyTo(_data, 8 + Length);
        }
    }
}