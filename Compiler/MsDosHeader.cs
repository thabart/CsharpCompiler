namespace Compiler
{
    public struct MsDosHeader
    {
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
    }
}
