using Compiler.Pe;
using System;
using System.IO;

namespace Compiler
{
    class Program
    {

        static void Main(string[] args)
        {
            var bytes = File.ReadAllBytes("Compiler-2.exe");
            var peParser = new PeParser();
            var peFile = peParser.Parse(bytes);
            Console.ReadLine();
        }
    }
}
