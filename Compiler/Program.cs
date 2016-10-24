using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var bytes = File.ReadAllBytes("Compiler-2.exe");
            ParseMdosHeader(bytes.ToList());
            Console.ReadLine();
        }

        private static void ParseMdosHeader(IEnumerable<byte> bytes)
        {
            // 1. Parse the magic number.
            var magicNumber = BitConverter.ToUInt16(new[] { bytes.ElementAt(0), bytes.ElementAt(1) }, 0);
            var expected = UInt16.Parse("5A4D", System.Globalization.NumberStyles.HexNumber);
            if (magicNumber != expected)
            {
                return;
            }

            var lastPageSize = BitConverter.ToUInt16(new[] { bytes.ElementAt(2), bytes.ElementAt(3) }, 0);
            var filePages = BitConverter.ToUInt16(new[] { bytes.ElementAt(4), bytes.ElementAt(5) }, 0);
            Console.WriteLine("ok");
        }
    }
}
