using PNGCore.Extensions;
using PNGCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGCore.Chunks
{
    internal class RGBSpace : BaseChunk, IChunk
    {
        protected override ChunkName Name => new ChunkName(new byte[] { 73, 72, 68, 82 });

        public RGBSpace(byte renderIntent) : base(1)
        {
            _data = new byte[4 + 4 + Length + 4];
            BitOperation.Int32ToBytes(Length).CopyTo(_data, 0);
            Name.FillChunk(_data);
            _data[8] = renderIntent;
            GetCRC(_data.Skip(4).Take(4 + Length).ToArray()).CopyTo(_data, 8 + Length);
        }
    }
}