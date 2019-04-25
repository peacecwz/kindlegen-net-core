using System.Diagnostics;
using System.IO;

namespace Kindlegen
{
    public class KindleConverter
    {
        private string _command;

        private KindleConverter(string path)
        {
            _command = $"\"{path}\" ";
        }

        public static KindleConverter Create(string path)
        {
            return new KindleConverter(path);
        }

        public KindleConverter SetCompressionLevel(CompressionLevel compressionLevel)
        {
            _command += $"-c{(int) compressionLevel} ";
            return this;
        }

        public KindleConverter SetOutput(string path)
        {
            _command += $"-o \"{path}\"";
            return this;
        }

        public KindleConverter ForceWesternEncoding()
        {
            _command += $"-western ";
            return this;
        }
        
        private string Execute(string arguments)
        {
            using (var kindlegenProcess = new Process())
            {
                kindlegenProcess.StartInfo.UseShellExecute = false;
                kindlegenProcess.StartInfo.RedirectStandardOutput = true;
                kindlegenProcess.StartInfo.FileName = Path.Combine("", "kindlegen.exe");
                kindlegenProcess.StartInfo.Arguments = arguments;
                kindlegenProcess.Start();
            
                var output = kindlegenProcess.StandardOutput.ReadToEnd();
                kindlegenProcess.WaitForExit();
                return output;
            }
        }

        public KindleConvertResult Convert()
        {
            var result = new KindleConvertResult();
            var commandOutput = Execute(_command);
            result.IsSuccess = true;
            result.Output = commandOutput;
            return result;
        }
    }
}