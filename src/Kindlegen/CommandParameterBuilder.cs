namespace Kindlegen
{
    public class CommandParameterBuilder
    {
        private string _command;

        private CommandParameterBuilder(string path)
        {
            _command = $"\"{path}\" ";
        }

        public static CommandParameterBuilder Create(string path)
        {
            return new CommandParameterBuilder(path);
        }

        public CommandParameterBuilder SetCompressionLevel(CompressionLevel compressionLevel)
        {
            _command += $"-c{(int) compressionLevel} ";
            return this;
        }

        public CommandParameterBuilder SetOutput(string path)
        {
            _command += $"-o \"{path}\"";
            return this;
        }

        public CommandParameterBuilder ForceWesternEncoding()
        {
            _command += $"-western ";
            return this;
        }
    }
}