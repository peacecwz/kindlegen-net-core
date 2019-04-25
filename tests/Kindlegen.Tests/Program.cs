using System;

namespace Kindlegen.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Write your .opf .htm .html .epub .zip or directory path: ");
            string path = Console.ReadLine();
            var result = KindleConverter.Create(path)
                .SetCompressionLevel(CompressionLevel.NoCompression)
                .SetOutput("test")
                .Convert();

            Console.WriteLine(result.Output);
        }
    }
}