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

            return new PeHeader
            {
                Magic = magic
            };
        }

        #endregion
    }
}
