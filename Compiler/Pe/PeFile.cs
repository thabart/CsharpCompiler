using System.Collections.Generic;

namespace Compiler.Pe
{
    public struct PeFile
    {
        public MsDosHeader MsDosHeader { get; set; }
        public IEnumerable<byte> MsDosStub { get; set; }
        public IEnumerable<byte> PeHeader { get; set; }
        public CoffHeader CoffHeader { get; set; }
    }
}
