using Ionic.Zlib;
using PNGCore.Extensions;
using PNGCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PNGCore.Chunks
{
    internal class ImageData : BaseChunk, IChunk
    {
        protected override ChunkName Name => new ChunkName(new byte[] { 73, 68, 65, 84 });

        private int _width;
        private int _height;

        public ImageData(int width, int height, byte red, byte green, byte blue) : base(1 + width * height * 3)
        {
            _width = width;
            _height = height;

            byte[] buffer = ArrangeRGB(red, green, blue);
            byte[] compressedBuffer = Compress(buffer);

            _data = new byte[4 + 4 + compressedBuffer.Length + 4];
            BitOperation.Int32ToBytes(compressedBuffer.Length).CopyTo(_data, 0);
            Name.FillChunk(_data);
            compressedBuffer.CopyTo(_data, 8);
            GetCRC(_data.Skip(4).Take(4 + compressedBuffer.Length).ToArray()).CopyTo(_data, 8 + compressedBuffer.Length);
        }

        private byte[] Compress(byte[] buffer)
        {
            using (var ms = new MemoryStream())
            {
                using (var compressor = new ZlibStream(ms, CompressionMode.Compress, CompressionLevel.BestSpeed))
                {
                    compressor.Write(buffer, 0, buffer.Length);
                }

                byte[] data = ms.ToArray();

                return data;
            }
        }

        private byte[] Depress(byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (var compressor =
                       new ZlibStream(ms, CompressionMode.Decompress))
                {
                    compressor.Write(buffer, 0, buffer.Length);
                }

                byte[] data = ms.ToArray();

                return data;
            }
        }

        private Random random = new Random();

        private byte[] ArrangeRGB(byte red, byte green, byte blue)
        {
            List<byte> buffer = new List<byte>();
            for (int y = 0; y < _height; y++)
            {
                buffer.Add(0);
                for (int x = 0; x < _width; x++)
                {
                    if ((x > 200 && _width - x > 200)
                        && (y > 100 && _height - y > 100))
                    {
                        buffer.Add(0);
                        buffer.Add(0);
                        buffer.Add(0);
                    }
                    else
                    {
                        buffer.Add(red);
                        buffer.Add(green);
                        buffer.Add(blue);
                    }
                }
            }

            return buffer.ToArray();
        }
    }
}