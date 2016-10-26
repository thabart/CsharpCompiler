using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Helpers;

namespace Compiler.Pe
{
    public enum Machines
    {
        IMAGE_FILE_MACHINE_I386 = 0x014c,
        IMAGE_FILE_MACHINE_IA64 = 0x0200,
        IMAGE_FILE_MACHINE_AMD64 = 0x8664
    }

    public enum Characteristics
    {
        IMAGE_FILE_RELOCS_STRIPPED = 0x0001,
        IMAGE_FILE_EXECUTABLE_IMAGE = 0x0002,
        IMAGE_FILE_LINE_NUMS_STRIPPED = 0x0004,
        IMAGE_FILE_LOCAL_SYMS_STRIPPED = 0x0008,
        IMAGE_FILE_AGGRESIVE_WS_TRIM = 0x0010,
        IMAGE_FILE_LARGE_ADDRESS_AWARE = 0x0020,
        IMAGE_FILE_64BIT_MACHINE = 0x0022,
        IMAGE_FILE_BYTES_REVERSED_LO = 0x0080,
        IMAGE_FILE_32BIT_MACHINE = 0x0100,
        IMAGE_FILE_DEBUG_STRIPPED = 0x0200,
        IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP = 0x0400,
        IMAGE_FILE_NET_RUN_FROM_SWAP = 0x0800,
        IMAGE_FILE_SYSTEM = 0x1000,
        IMAGE_FILE_DLL = 0x2000,
        IMAGE_FILE_UP_SYSTEM_ONLY = 0x4000,
        IMAGE_FILE_BYTES_REVERSED_HI = 0x8000
    }

    public struct CoffHeader
    {
        #region Properties

        /// <summary>
        /// Number identifying the type of the target machine.
        /// </summary>
        public Machines Machine { get; set; }
        /// <summary>
        /// Number of entries in the section table, which immediately follows the headers.
        /// </summary>
        public short NumberOfSections { get; set; }
        /// <summary>
        /// Time and date of file creation.
        /// </summary>
        public int TimeDateStamp { get; set; }
        /// <summary>
        /// File pointer of the COFF symbol table. Should be set to 0.
        /// </summary>
        public int PointToSymbolTable { get; set; }
        /// <summary>
        /// Number of entries in the COFF symbol table. Should be set to 0.
        /// </summary>
        public int NumberOfSymbols { get; set; }
        /// <summary>
        /// Size of the PE header.
        /// </summary>
        public short SizeOfOptionalHeader { get; set; }
        /// <summary>
        /// Flags indicating the attributes of the file.
        /// </summary>
        public Characteristics Characteristics { get; set; }

        #endregion

        #region Internal methods

        internal static CoffHeader Parse(IEnumerable<byte> peBytes)
        {
            var coffBytes = peBytes.Skip(132).Take(20);
            var bmachine = BitConverter.ToInt16(new[] { coffBytes.ElementAt(0), coffBytes.ElementAt(1) }, 0);
            Machines machine;
            if (!EnumHelper.TryGetValue(bmachine, out machine))
            {
                throw new ArgumentException(string.Format("the machine {0} is not supported", bmachine));
            }

            var numberOfSections = BitConverter.ToInt16(new[] { coffBytes.ElementAt(2), coffBytes.ElementAt(3) }, 0);
            var timeDateStamp = BitConverter.ToInt32(new[] { coffBytes.ElementAt(4), coffBytes.ElementAt(5), coffBytes.ElementAt(6), coffBytes.ElementAt(7) }, 0);
            var pointerToSymbolTable = BitConverter.ToInt32(new[] { coffBytes.ElementAt(8), coffBytes.ElementAt(9), coffBytes.ElementAt(10), coffBytes.ElementAt(11) }, 0);
            var numberOfSymbols = BitConverter.ToInt32(new[] { coffBytes.ElementAt(12), coffBytes.ElementAt(13), coffBytes.ElementAt(14), coffBytes.ElementAt(15) }, 0);
            var sizeOfOptionalHeader = BitConverter.ToInt16(new[] { coffBytes.ElementAt(16), coffBytes.ElementAt(17) }, 0);
            var bcharacteristic = BitConverter.ToInt16(new[] { coffBytes.ElementAt(18), coffBytes.ElementAt(19) }, 0);
            Characteristics characteristic;
            if (!EnumHelper.TryGetValue(bcharacteristic, out characteristic))
            {
                throw new ArgumentException(string.Format("the characteristic {0} is not supported", bcharacteristic));
            }

            return new CoffHeader
            {
                Machine = machine,
                NumberOfSections = numberOfSections,
                TimeDateStamp = timeDateStamp,
                PointToSymbolTable = pointerToSymbolTable,
                NumberOfSymbols = numberOfSymbols,
                SizeOfOptionalHeader = sizeOfOptionalHeader,
                Characteristics = characteristic
            };
        }

        #endregion
    }
}
