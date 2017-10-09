using System.Collections.Generic;

namespace Transdirect.Exceptions
{
    public class TransdirectError
    {
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}