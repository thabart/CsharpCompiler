using System;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Pe
{
    public struct MsDosHeader
    {
        #region Static fields

        public static IEnumerable<byte> MagicNumbers = new byte[]
        {
            77,
            90
        };

        #endregion

        #region Properties

        /// <summary>
        /// Signature = MZ
        /// </summary>
        public ushort Signature { get; set; }
        /// <summary>
        /// Number of bytes on last page of file.
        /// </summary>
        public ushort LastSize { get; set; }
        /// <summary>
        /// Total numbers of 512-bytes pages in executables (including the last page).
        /// </summary>
        public ushort Nblocks { get; set; }
        /// <summary>
        /// Number of relocation entries.
        /// </summary>
        public ushort NReloc { get; set; }
        /// <summary>
        /// Header size in paragraphs.
        /// </summary>
        public ushort HdrSize { get; set; }
        /// <summary>
        /// Minimum paragraphs of memory allocated in addition to the code size.
        /// </summary>
        public ushort MinAlloc { get; set; }
        /// <summary>
        /// Maximum number of paragraphs allocated in addition to the code size.
        /// </summary>
        public ushort MaxAlloc { get; set; }
        /// <summary>
        /// Initial SS relative to start of executable.
        /// </summary>
        public ushort Ss { get; set; }
        /// <summary>
        /// Initial SP.
        /// </summary>
        public ushort Sp { get; set; }
        /// <summary>
        /// Checksum (or 0) of executable.
        /// </summary>
        public ushort Checksum { get; set; }
        /// <summary>
        /// CS:IP relative to start of executable.
        /// </summary>
        public ushort Ip { get; set; }
        /// <summary>
        /// Initial (relative) CS value.
        /// </summary>
        public ushort Cs { get; set; }
        /// <summary>
        /// File address or relocation table.
        /// </summary>
        public ushort RelocPos { get; set; }
        /// <summary>
        /// Overlay number.
        /// </summary>
        public ushort Overlay { get; set; }
        /// <summary>
        /// Reseverd words. (4 elements).
        /// </summary>
        public ushort[] ReservedWords { get; set; }
        /// <summary>
        /// OEM identifier (for e_oeminfo).
        /// </summary>
        public ushort OemId { get; set; }
        /// <summary>
        /// OEM information; e_oemid specific.
        /// </summary>
        public ushort OemInfo { get; set; }
        /// <summary>
        /// Resever words. (10 elements)
        /// </summary>
        public ushort[] ReservedWords2 { get; set; }
        /// <summary>
        /// File address or new exe header.
        /// </summary>
        public int LfaAddress { get; set; }

        #endregion

        #region Public methods

        internal static MsDosHeader Parse(IEnumerable<byte> peBytes)
        {
            if (peBytes == null || !peBytes.Any())
            {
                throw new ArgumentNullException(nameof(peBytes));
            }

            var magicNumber = new[] { peBytes.ElementAt(0), peBytes.ElementAt(1) };
            if (magicNumber.Count() != MagicNumbers.Count() || !magicNumber.All(n => MagicNumbers.Contains(n)))
            {
                throw new FormatException("magic number is not correct");
            }

            return new MsDosHeader
            {
                Signature = BitConverter.ToUInt16(magicNumber, 0),
                LastSize = BitConverter.ToUInt16(new[] { peBytes.ElementAt(2), peBytes.ElementAt(3) }, 0),
                Nblocks = BitConverter.ToUInt16(new[] { peBytes.ElementAt(4), peBytes.ElementAt(5) }, 0),
                NReloc = BitConverter.ToUInt16(new[] { peBytes.ElementAt(6), peBytes.ElementAt(7) }, 0),
                HdrSize = BitConverter.ToUInt16(new[] { peBytes.ElementAt(8), peBytes.ElementAt(9) }, 0),
                MinAlloc = BitConverter.ToUInt16(new[] { peBytes.ElementAt(10), peBytes.ElementAt(11) }, 0),
                MaxAlloc = BitConverter.ToUInt16(new[] { peBytes.ElementAt(12), peBytes.ElementAt(13) }, 0),
                Ss = BitConverter.ToUInt16(new[] { peBytes.ElementAt(14), peBytes.ElementAt(15) }, 0),
                Sp = BitConverter.ToUInt16(new[] { peBytes.ElementAt(16), peBytes.ElementAt(17) }, 0),
                Checksum = BitConverter.ToUInt16(new[] { peBytes.ElementAt(18), peBytes.ElementAt(19) }, 0),
                Ip = BitConverter.ToUInt16(new[] { peBytes.ElementAt(20), peBytes.ElementAt(21) }, 0),
                Cs = BitConverter.ToUInt16(new[] { peBytes.ElementAt(22), peBytes.ElementAt(23) }, 0),
                RelocPos = BitConverter.ToUInt16(new[] { peBytes.ElementAt(24), peBytes.ElementAt(25) }, 0),
                Overlay = BitConverter.ToUInt16(new[] { peBytes.ElementAt(26), peBytes.ElementAt(27) }, 0),
                ReservedWords = new[]
                {
                   BitConverter.ToUInt16(new[] { peBytes.ElementAt(28), peBytes.ElementAt(29) }, 0),
                   BitConverter.ToUInt16(new[] { peBytes.ElementAt(30), peBytes.ElementAt(31) }, 0),
                   BitConverter.ToUInt16(new[] { peBytes.ElementAt(32), peBytes.ElementAt(33) }, 0),
                   BitConverter.ToUInt16(new[] { peBytes.ElementAt(34), peBytes.ElementAt(35) }, 0)
                },
                OemId = BitConverter.ToUInt16(new[] { peBytes.ElementAt(36), peBytes.ElementAt(37) }, 0),
                OemInfo = BitConverter.ToUInt16(new[] { peBytes.ElementAt(38), peBytes.ElementAt(39) }, 0),
                ReservedWords2 = new[]
                {
                    BitConverter.ToUInt16(new[] { peBytes.ElementAt(40), peBytes.ElementAt(41) }, 0),
                    BitConverter.ToUInt16(new[] { peBytes.ElementAt(42), peBytes.ElementAt(43) }, 0),
                    BitConverter.ToUInt16(new[] { peBytes.ElementAt(44), peBytes.ElementAt(45) }, 0),
                    BitConverter.ToUInt16(new[] { peBytes.ElementAt(46), peBytes.ElementAt(47) }, 0),
                    BitConverter.ToUInt16(new[] { peBytes.ElementAt(48), peBytes.ElementAt(49) }, 0),
                    BitConverter.ToUInt16(new[] { peBytes.ElementAt(50), peBytes.ElementAt(51) }, 0),
                    BitConverter.ToUInt16(new[] { peBytes.ElementAt(52), peBytes.ElementAt(53) }, 0),
                    BitConverter.ToUInt16(new[] { peBytes.ElementAt(54), peBytes.ElementAt(55) }, 0),
                    BitConverter.ToUInt16(new[] { peBytes.ElementAt(56), peBytes.ElementAt(57) }, 0),
                    BitConverter.ToUInt16(new[] { peBytes.ElementAt(58), peBytes.ElementAt(59) }, 0)
                },
                LfaAddress = BitConverter.ToInt32(new[] { peBytes.ElementAt(60), peBytes.ElementAt(61), peBytes.ElementAt(62), peBytes.ElementAt(63) }, 0)
            };
        }

        #endregion
    }
}
