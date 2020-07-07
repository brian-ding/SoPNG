using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using PNGCore.Extensions;
using PNGCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGCore.Chunks
{
    internal class ImageData : BaseChunk, IChunk
    {
        protected override ChunkName Name => new ChunkName(new byte[] { 73, 68, 65, 84 });

        public ImageData(int width, int height, byte red, byte green, byte blue) : base(1 + width * height * 3)
        {
            byte[] buffer = new byte[1 + width * height * 3];
            buffer[0] = 0;
            for (int i = 1; i < buffer.Length; i += 3)
            {
                buffer[i] = red;
                buffer[i + 1] = green;
                buffer[i + 2] = blue;
            }
            byte[] compressedBuffer = Compress(buffer);

            _data = new byte[4 + 4 + compressedBuffer.Length + 4];
            BitOperation.Int32ToBytes(compressedBuffer.Length).CopyTo(_data, 0);
            Name.FillChunk(_data);
            compressedBuffer.CopyTo(_data, 8);
            GetCRC(_data.Skip(4).Take(4 + compressedBuffer.Length).ToArray()).CopyTo(_data, 8 + compressedBuffer.Length);
        }

        private byte[] Compress(byte[] data)
        {
            byte[] resBuffer = null;

            MemoryStream mOutStream = new MemoryStream(data.Length);
            DeflaterOutputStream defStream = new DeflaterOutputStream(mOutStream);

            try
            {
                defStream.Write(data, 0, data.Length);
                defStream.Flush();
                defStream.Finish();

                resBuffer = mOutStream.ToArray();
            }
            finally
            {
                defStream.Close();
                mOutStream.Close();
            }

            return resBuffer;
        }

        private byte[] Depress(byte[] data)
        {
            //var outputStream = new MemoryStream();
            //using (var compressedStream = new MemoryStream(data))
            //using (var inputStream = new InflaterInputStream(compressedStream))
            //{
            //    inputStream.CopyTo(outputStream);
            //    outputStream.Position = 0;
            //    return outputStream;
            //}

            var outputStream = new MemoryStream();
            using (var compressedStream = new MemoryStream(data))
            using (var inputStream = new InflaterInputStream(compressedStream))
            {
                inputStream.CopyTo(outputStream);
                outputStream.Position = 0;
                return outputStream.ToArray();
            }
        }
    }
}