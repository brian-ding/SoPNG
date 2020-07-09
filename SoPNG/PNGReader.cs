using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoPNG
{
    public class PNGReader : StreamReader
    {
        public PNGReader(Stream stream) : base(stream)
        {
        }

        public PNGReader(string path) : base(path)
        {
        }

        public override string ReadToEnd()
        {
            List<string> names = new List<string>();
            byte[] buffer = new byte[BaseStream.Length];
            BaseStream.Read(buffer, 0, buffer.Length);
            int chunkIndex = 8;
            while (chunkIndex < buffer.Length)
            {
                char[] chars = new char[] { (char)buffer[chunkIndex + 4], (char)buffer[chunkIndex + 5], (char)buffer[chunkIndex + 6], (char)buffer[chunkIndex + 7] };
                string name = new string(chars);
                int length = (buffer[chunkIndex] << 24) + (buffer[chunkIndex + 1] << 16) + (buffer[chunkIndex + 2] << 8) + (buffer[chunkIndex + 3] << 0);

                names.Add(name + Environment.NewLine);
                for (int i = 0; i < length + 4 + 4 + 4; i++)
                {
                    names.Add(buffer[chunkIndex + i].ToString() + " ");
                }
                names.Add(Environment.NewLine + Environment.NewLine);

                chunkIndex += 4 + 4 + 4 + length;
            }

            return string.Concat(names);
        }
    }
}
