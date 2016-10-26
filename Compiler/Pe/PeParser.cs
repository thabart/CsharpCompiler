using System;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Pe
{
    /// <summary>
    /// Portable executable parser.
    /// </summary>
    public class PeParser
    {
        private static byte[] PeHeaders = new byte[]
        {
            80,
            69,
            0,
            0
        };

        public PeFile Parse(IEnumerable<byte> bytes)
        {
            if (bytes.Count() < 336)
            {
                throw new ArgumentException("the size is not correct. At least > to 336");
            }

            return new PeFile
            {
                MsDosHeader = MsDosHeader.Parse(bytes),
                MsDosStub = ParseMsdosStub(bytes),
                PeHeader = ParsePeSignature(bytes)
            };
        }

        private static IEnumerable<byte> ParseMsdosStub(IEnumerable<byte> bytes)
        {
            return bytes.Skip(64).Take(64);
        }

        private static IEnumerable<byte> ParsePeSignature(IEnumerable<byte> bytes)
        {
            var values = bytes.Skip(128).Take(4);
            if (values.Count() != PeHeaders.Count() || !values.All(v => PeHeaders.Contains(v)))
            {
                throw new FormatException("the PE header is not valid");
            }

            return values;
        }
    }
}
