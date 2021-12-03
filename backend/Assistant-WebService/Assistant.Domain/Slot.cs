using System.Collections.Generic;

namespace Assistant.Domain
{
    public class Slot
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public IEnumerable<string> Values { get; set; }
    }
}
