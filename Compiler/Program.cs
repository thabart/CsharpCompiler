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
            var msDosHeader = ParseMdosHeader(bytes.ToList());
            Console.ReadLine();
        }

        private static MsDosHeader ParseMdosHeader(IEnumerable<byte> bytes)
        {
            // 1. Parse the magic number.
            var magicNumber = BitConverter.ToUInt16(new[] { bytes.ElementAt(0), bytes.ElementAt(1) }, 0);
            var expected = UInt16.Parse("5A4D", System.Globalization.NumberStyles.HexNumber);
            if (magicNumber != expected)
            {
                return default(MsDosHeader);
            }

            // 64 bytes.
            return new MsDosHeader
            {
                Signature = magicNumber,
                LastSize = BitConverter.ToUInt16(new[] { bytes.ElementAt(2), bytes.ElementAt(3) }, 0),
                Nblocks = BitConverter.ToUInt16(new[] { bytes.ElementAt(4), bytes.ElementAt(5) }, 0),
                NReloc = BitConverter.ToUInt16(new[] { bytes.ElementAt(6), bytes.ElementAt(7) }, 0),
                HdrSize = BitConverter.ToUInt16(new[] { bytes.ElementAt(8), bytes.ElementAt(9) }, 0),
                MinAlloc = BitConverter.ToUInt16(new[] { bytes.ElementAt(10), bytes.ElementAt(11) }, 0),
                MaxAlloc = BitConverter.ToUInt16(new[] { bytes.ElementAt(12), bytes.ElementAt(13) }, 0),
                Ss = BitConverter.ToUInt16(new[] { bytes.ElementAt(14), bytes.ElementAt(15) }, 0),
                Sp = BitConverter.ToUInt16(new[] { bytes.ElementAt(16), bytes.ElementAt(17) }, 0),
                Checksum = BitConverter.ToUInt16(new[] { bytes.ElementAt(18), bytes.ElementAt(19) }, 0),
                Ip = BitConverter.ToUInt16(new[] { bytes.ElementAt(20), bytes.ElementAt(21) }, 0),
                Cs = BitConverter.ToUInt16(new[] { bytes.ElementAt(22), bytes.ElementAt(23) }, 0),
                RelocPos = BitConverter.ToUInt16(new[] { bytes.ElementAt(24), bytes.ElementAt(25) }, 0),
                Overlay = BitConverter.ToUInt16(new[] { bytes.ElementAt(26), bytes.ElementAt(27) }, 0),
                ReservedWords = new[]
                {
                   BitConverter.ToUInt16(new[] { bytes.ElementAt(28), bytes.ElementAt(29) }, 0),
                   BitConverter.ToUInt16(new[] { bytes.ElementAt(30), bytes.ElementAt(31) }, 0),
                   BitConverter.ToUInt16(new[] { bytes.ElementAt(32), bytes.ElementAt(33) }, 0),
                   BitConverter.ToUInt16(new[] { bytes.ElementAt(34), bytes.ElementAt(35) }, 0)
                },
                OemId = BitConverter.ToUInt16(new[] { bytes.ElementAt(36), bytes.ElementAt(37) }, 0),
                OemInfo = BitConverter.ToUInt16(new[] { bytes.ElementAt(38), bytes.ElementAt(39) }, 0),
                ReservedWords2 = new []
                {
                    BitConverter.ToUInt16(new[] { bytes.ElementAt(40), bytes.ElementAt(41) }, 0),
                    BitConverter.ToUInt16(new[] { bytes.ElementAt(42), bytes.ElementAt(43) }, 0),
                    BitConverter.ToUInt16(new[] { bytes.ElementAt(44), bytes.ElementAt(45) }, 0),
                    BitConverter.ToUInt16(new[] { bytes.ElementAt(46), bytes.ElementAt(47) }, 0),
                    BitConverter.ToUInt16(new[] { bytes.ElementAt(48), bytes.ElementAt(49) }, 0),
                    BitConverter.ToUInt16(new[] { bytes.ElementAt(50), bytes.ElementAt(51) }, 0),
                    BitConverter.ToUInt16(new[] { bytes.ElementAt(52), bytes.ElementAt(53) }, 0),
                    BitConverter.ToUInt16(new[] { bytes.ElementAt(54), bytes.ElementAt(55) }, 0),
                    BitConverter.ToUInt16(new[] { bytes.ElementAt(56), bytes.ElementAt(57) }, 0),
                    BitConverter.ToUInt16(new[] { bytes.ElementAt(58), bytes.ElementAt(59) }, 0)
                },
                LfaAddress = BitConverter.ToInt32(new []{ bytes.ElementAt(60), bytes.ElementAt(61), bytes.ElementAt(61), bytes.ElementAt(61) }, 0)
            };
        }
    }
}
