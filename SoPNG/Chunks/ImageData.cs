using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Ionic.Zlib;
using SoPNG.Extensions;
using SoPNG.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoPNG.Chunks
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
            using (MemoryStream ms = new System.IO.MemoryStream())
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

        private byte[] ArrangeRGB(byte red, byte green, byte blue)
        {
            byte[] buffer = new byte[_height + _width * _height * 3];
            int dataWidth = 3 * _width + 1;

            for (int i = 0; i < buffer.Length; i++)
            {
                int x = i % dataWidth;
                int y = i / dataWidth;
                if (x == 0)
                {
                    // each line's leading byte is 0, if the filtering method is 0
                    buffer[i] = 0;
                }
                else
                {
                    switch ((x - 1) % 3)
                    {
                        case 0:
                            buffer[i] = red;
                            break;

                        case 1:
                            buffer[i] = green;
                            break;

                        case 2:
                            buffer[i] = blue;
                            break;
                    }
                }
            }

            return buffer;
        }
    }
}