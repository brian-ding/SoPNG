using SoPNG.Chunks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoPNG
{
    public class Image
    {
        private List<byte> _buffer = new List<byte>();

        public Image(int width, int height, byte red, byte green, byte blue)
        {
            BaseChunk chunk1 = new ImageHeader(width, height, 8, 2, 0, 0, 0);
            _buffer.AddRange(chunk1.GetData());

            BaseChunk chunk2 = new RGBSpace(0);
            _buffer.AddRange(chunk2.GetData());

            BaseChunk chunk3 = new ImageGamma(1 / 2.2);
            _buffer.AddRange(chunk3.GetData());

            BaseChunk chunk4 = new PhysDimension(3779, 3779, 1);
            _buffer.AddRange(chunk4.GetData());

            BaseChunk chunk5 = new ImageData(width, height, red, green, blue);
            _buffer.AddRange(chunk5.GetData());

            BaseChunk chunk6 = new ImageTrailer();
            _buffer.AddRange(chunk6.GetData());
        }

        public void Save(string path)
        {
            File.WriteAllBytes(path, _buffer.ToArray());
        }
    }
}