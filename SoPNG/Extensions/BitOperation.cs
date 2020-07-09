using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoPNG.Extensions
{
    public static class BitOperation
    {
        public static byte[] Int32ToBytes(int number)
        {
            byte[] result = new byte[4];
            result[0] = (byte)(number << 0 >> 24);
            result[1] = (byte)(number << 8 >> 24);
            result[2] = (byte)(number << 16 >> 24);
            result[3] = (byte)(number << 24 >> 24);

            return result;
        }
    }
}
