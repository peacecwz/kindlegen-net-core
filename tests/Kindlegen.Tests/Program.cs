using System;
using System.Linq;
using Kindlegen.Models;

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
                .SetOutput("test.mobi")
                .Convert();
        
            if (!result.IsSuccess)
            {
                string message = result.Details.FirstOrDefault(x => x.ConvertLogType == ConvertLogType.Error)?.Message;
                Console.WriteLine($"Has exception: {message}");
            }
            else
            {
                Console.WriteLine("Complete successfully");
            }

            Console.ReadKey();
        }
    }
}