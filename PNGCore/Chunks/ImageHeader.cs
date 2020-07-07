using PNGCore.Extensions;
using PNGCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGCore.Chunks
{
    internal class ImageHeader : BaseChunk, IChunk
    {
        protected override ChunkName Name => new ChunkName(new byte[] { 73, 72, 68, 82 });

        public ImageHeader(int width, int height, byte depth, byte colorType, byte compression, byte filter, byte interlace) : base(13)
        {
            BitOperation.Int32ToBytes(width).CopyTo(_data, 8);
            BitOperation.Int32ToBytes(height).CopyTo(_data, 12);
            _data[16] = depth;
            _data[17] = colorType;
            _data[18] = compression;
            _data[19] = filter;
            _data[20] = interlace;
            GetCRC(_data.Skip(4).Take(4 + Length).ToArray()).CopyTo(_data, 8 + Length);
        }
    }
}