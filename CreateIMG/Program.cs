using PNGCore;
using System;
using System.Collections.Generic;
using System.IO;

namespace CreateIMG
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Image image = new Image(1024, 768, 237, 28, 36);
            image.Save(@"C:\Users\Brian\Desktop\test.png");
            // byte[] correct = File.ReadAllBytes(@"C:\Users\Brian\Desktop\1_1.png");
            //byte[] wrong = File.ReadAllBytes(@"C:\Users\Brian\Desktop\test.png");

            //var list = GetChunks(correct);
        }

        private static List<string> GetChunks(byte[] buffer)
        {
            List<string> names = new List<string>();
            int chunkIndex = 8;
            while (chunkIndex < buffer.Length)
            {
                char[] chars = new char[] { (char)buffer[chunkIndex + 4], (char)buffer[chunkIndex + 5], (char)buffer[chunkIndex + 6], (char)buffer[chunkIndex + 7] };
                string name = new string(chars);
                names.Add(name);
                int length = (buffer[chunkIndex] << 24) + (buffer[chunkIndex + 1] << 16) + (buffer[chunkIndex + 2] << 8) + (buffer[chunkIndex + 3] << 0);

                Console.WriteLine(name);
                for (int i = 0; i < length + 4 + 4 + 4; i++)
                {
                    Console.Write(buffer[chunkIndex + i].ToString() + " ");
                }
                Console.WriteLine();
                Console.WriteLine();

                chunkIndex += 4 + 4 + 4 + length;
            }

            return names;
        }
    }
}