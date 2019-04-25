using Kindlegen.Models;

namespace Kindlegen
{
    internal class KindleOutputParser
    {
        internal static string GetLogType(string line)
        {
            if (string.IsNullOrEmpty(line.Trim()) || line.IndexOf(':') == -1)
            {
                return "";
            }

            try
            {
                if (line.IndexOf('(') == -1)
                {
                    return line.Substring(0, line.IndexOf(':')).Replace(":", "");
                }

                return line.Substring(0, line.IndexOf('(')).Replace("(", "");
            }
            catch
            {
                return "";
            }
        }

        internal static KindleConvertResult ParseOutput(string output)
        {
            var result = new KindleConvertResult();
            result.Output = output;
            foreach (var line in output.Split('\n'))
            {
                try
                {
                    string logTypeString = GetLogType(line);
                    if (string.IsNullOrEmpty(logTypeString))
                    {
                        continue;
                    }

                    ConvertLogType logType = ConvertLogType.Info;
                    switch (logTypeString)
                    {
                        case "Info":
                            logType = ConvertLogType.Info;
                            break;
                        case "Warning":
                            logType = ConvertLogType.Warning;
                            break;
                        case "Error":
                            logType = ConvertLogType.Error;
                            break;
                    }

                    string codeStart = line
                        .Substring(line.IndexOf(':') + 1);
                    string code = codeStart
                        .Substring(0, codeStart.IndexOf(':') + 1)
                        .Replace(":", "")
                        .Trim();
                    string message = line.Substring(line.IndexOf(' ')).Trim();
                    result.Details.Add(new ConvertLog()
                    {
                        ConvertLogType = logType,
                        Code = code,
                        Message = message
                    });
                }
                catch
                {
                    // Ignored
                }
            }

            return result;
        }
    }
}