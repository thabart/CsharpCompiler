using System.Linq;
using System.Collections.Generic;
using Compiler.Helpers;
using System;

namespace Compiler.Pe
{
    public enum MagicNumbers
    {
        PE32 = 0x10b,
        ROM_IMAGE = 0x107,
        PE32_PLUS = 0x20b
    }
    /// <summary>
    /// Optional PE header.
    /// </summary>
    public class PeHeader
    {
        #region Properties

        /// <summary>
        /// Identifies the state of the image file.
        /// </summary>
        public MagicNumbers Magic { get; set; }
        /// <summary>
        /// Major linker version.
        /// </summary>
        public byte MajorLinkerVersion { get; set; }
        /// <summary>
        /// Minor linker version.
        /// </summary>
        public byte MinorLinkerVersion { get; set; }
        /// <summary>
        /// Size of the code (text) section.
        /// </summary>
        public int SizeOfCode { get; set; }
        /// <summary>
        /// Size of the initialized data section.
        /// </summary>
        public int SizeOfInitializedData { get; set; }
        /// <summary>
        /// Size of the unitialized data section.
        /// </summary>
        public int SizeOfUnitializedData { get; set; }
        /// <summary>
        /// The address of the entry point relative to the image base when the executable file is loaded into memory.
        /// </summary>
        public int AddressOfEntryPoint { get; set; }
        /// <summary>
        /// The address that is relative to the image base of the beginning of code section when it is loaded into memory.
        /// </summary>
        public int BaseOfCode { get; set; }
        /// <summary>
        /// The address that is relative to the image base of the beginning of data section when it is loaded into memory.
        /// </summary>
        public int BaseOfData { get; set; }

        #endregion

        #region Internal methods

        internal static PeHeader Parse(IEnumerable<byte> peBytes, short size)
        {
            var peHeaderBytes = peBytes.Skip(152).Take(size);
            var bMagic = BitConverter.ToInt16(new[] { peHeaderBytes.ElementAt(0), peHeaderBytes.ElementAt(1) }, 0);
            MagicNumbers magic;
            if (!EnumHelper.TryGetValue(bMagic, out magic))
            {
                throw new ArgumentException(string.Format("the magic {0} is not supproted", bMagic));
            }

            var majorLinkerVersion = peHeaderBytes.ElementAt(2);
            var minorLinkerVersion = peHeaderBytes.ElementAt(3);
            var sizeOfCode = BitConverter.ToInt32(new[] { peHeaderBytes.ElementAt(4), peHeaderBytes.ElementAt(5), peHeaderBytes.ElementAt(6), peHeaderBytes.ElementAt(7) }, 0);
            var sizeOfInitializedData = BitConverter.ToInt32(new[] { peHeaderBytes.ElementAt(8), peHeaderBytes.ElementAt(9), peHeaderBytes.ElementAt(10), peHeaderBytes.ElementAt(11) }, 0);
            var sizeOfUnitalizedData = BitConverter.ToInt32(new[] { peHeaderBytes.ElementAt(12), peHeaderBytes.ElementAt(13), peHeaderBytes.ElementAt(14), peHeaderBytes.ElementAt(15) }, 0);
            var addressOfEntryPoint = BitConverter.ToInt32(new[] { peHeaderBytes.ElementAt(16), peHeaderBytes.ElementAt(17), peHeaderBytes.ElementAt(18), peHeaderBytes.ElementAt(19) }, 0);
            var baseOfCode = BitConverter.ToInt32(new[] { peHeaderBytes.ElementAt(20), peHeaderBytes.ElementAt(21), peHeaderBytes.ElementAt(22), peHeaderBytes.ElementAt(23) }, 0);
            var result = new PeHeader
            {
                Magic = magic,
                MajorLinkerVersion = majorLinkerVersion,
                MinorLinkerVersion = minorLinkerVersion,
                SizeOfCode = sizeOfCode,
                SizeOfInitializedData = sizeOfInitializedData,
                SizeOfUnitializedData = sizeOfUnitalizedData,
                AddressOfEntryPoint = addressOfEntryPoint,
                BaseOfCode = baseOfCode
            };
            if (magic == MagicNumbers.PE32)
            {
                var baseOfData = BitConverter.ToInt32(new[] { peHeaderBytes.ElementAt(24), peHeaderBytes.ElementAt(25), peHeaderBytes.ElementAt(26), peHeaderBytes.ElementAt(27) }, 0);
                result.BaseOfData = baseOfData;
            }

            return result;
        }

        #endregion
    }
}
