using SoPNG.Extensions;
using SoPNG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoPNG.Chunks
{
    internal class ImageGamma : BaseChunk, IChunk
    {
        protected override ChunkName Name => new ChunkName(new byte[] { 103, 65, 77, 65 });

        public ImageGamma(double gamma) : base(4)
        {
            BitOperation.Int32ToBytes((int)Math.Round(gamma * 100000)).CopyTo(_data, 8);
            GetCRC(_data.Skip(4).Take(4 + Length).ToArray()).CopyTo(_data, 8 + Length);
        }
    }
}