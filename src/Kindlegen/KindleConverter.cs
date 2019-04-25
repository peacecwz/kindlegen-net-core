using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Kindlegen.Models;

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
            _command += $"-c{(int)compressionLevel} ";
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

        private string GetKindlePath()
        {
            string kindlePath = "/pkg";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                kindlePath += "/win/kindlegen.exe";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                kindlePath += "/linux/kindlegen";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                kindlePath += "/mac/kindlegen";
            }
            else
            {
                throw new Exception("Unsupported OS");
            }

            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + kindlePath;
        }

        private KindleConvertResult Execute(string arguments)
        {
            using (var kindlegenProcess = new Process())
            {
                kindlegenProcess.StartInfo.UseShellExecute = false;
                kindlegenProcess.StartInfo.RedirectStandardOutput = true;
                kindlegenProcess.StartInfo.FileName = GetKindlePath();
                kindlegenProcess.StartInfo.Arguments = Encoding.Default.GetString(Encoding.UTF8.GetBytes(arguments));
                kindlegenProcess.Start();

                var output = kindlegenProcess.StandardOutput.ReadToEnd();
                kindlegenProcess.WaitForExit();
                return KindleOutputParser.ParseOutput(output);
            }
        }

        public KindleConvertResult Convert()
        {
            return Execute(_command);
        }
    }
}