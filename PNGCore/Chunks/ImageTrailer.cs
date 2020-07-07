using PNGCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGCore.Chunks
{
    internal class ImageTrailer : BaseChunk, IChunk
    {
        protected override ChunkName Name => new ChunkName(new byte[] { 73, 69, 78, 68 });

        public ImageTrailer() : base(0)
        {
            GetCRC(_data.Skip(4).Take(4 + Length).ToArray()).CopyTo(_data, 8 + Length);
        }
    }
}