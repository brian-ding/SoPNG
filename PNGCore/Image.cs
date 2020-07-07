using PNGCore.Chunks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGCore
{
    public class Image
    {
        public Image(int width, int height, byte red, byte green, byte blue)
        {
            var header = new ImageHeader(width, height, 8, 2, 0, 0, 0);
        }
    }
}