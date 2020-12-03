using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGCore.Chunks
{
    internal class ImageBuffer
    {
        private readonly int _width;
        private readonly int _height;
        private int ColumnCount { get => 1 + 3 * _width; }

        private byte[] _buffers;

        public ImageBuffer(int width, int height)
        {
            _width = width;
            _height = height;

            _buffers = new byte[_height * ColumnCount];
        }

        public ImageBuffer SetBackgroundColor(byte red, byte green, byte blue)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 1; x < ColumnCount; x++)
                {
                    int index = y * ColumnCount + x;
                    switch (x % 3)
                    {
                        case 1:
                            _buffers[index] = red;
                            break;

                        case 2:
                            _buffers[index] = green;
                            break;

                        case 0:
                            _buffers[index] = blue;
                            break;
                    }
                }
            }

            return this;
        }

        public ImageBuffer DrawRectangle(int left, int top, int width, int height, byte red, byte green, byte blue)
        {
            for (int y = top; y < top + height; y++)
            {
                for (int x = left; x < left + width; x++)
                {
                    _buffers[y * ColumnCount + (1 + 3 * x)] = red;
                    _buffers[y * ColumnCount + (2 + 3 * x)] = green;
                    _buffers[y * ColumnCount + (3 + 3 * x)] = blue;
                }
            }

            return this;
        }

        public ImageBuffer DrawLine(int left, int top, int width, int height, Func<int, int> func, byte red, byte green, byte blue, int thickness)
        {
            for (int x = left; x < left + width; x++)
            {
                int value = top + height - func(x - left);
                for (int y = value - thickness; y < value + thickness + 1; y++)
                {
                    if (y >= top && y < top + height)
                    {
                        _buffers[y * ColumnCount + (1 + 3 * x)] = red;
                        _buffers[y * ColumnCount + (2 + 3 * x)] = green;
                        _buffers[y * ColumnCount + (3 + 3 * x)] = blue;
                    }
                }
            }

            return this;
        }

        public byte[] ToArray()
        {
            return _buffers;
        }
    }
}