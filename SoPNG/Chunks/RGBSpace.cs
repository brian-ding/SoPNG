using SoPNG.Extensions;
using SoPNG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoPNG.Chunks
{
    internal class RGBSpace : BaseChunk, IChunk
    {
        protected override ChunkName Name => new ChunkName(new byte[] { 115, 82, 71, 66 });

        public RGBSpace(byte renderIntent) : base(1)
        {
            _data[8] = renderIntent;
            GetCRC(_data.Skip(4).Take(4 + Length).ToArray()).CopyTo(_data, 8 + Length);
        }
    }
}