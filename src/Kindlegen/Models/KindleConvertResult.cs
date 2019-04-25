using System.Collections.Generic;
using System.Linq;

namespace Kindlegen.Models
{
    public class KindleConvertResult
    {
        public bool IsSuccess
        {
            get
            {
                return Details.All(convertLog => convertLog.ConvertLogType != ConvertLogType.Error);
            }
        }

        public string Output { get; set; }
        public List<ConvertLog> Details { get; set; } = new List<ConvertLog>();
    }
}