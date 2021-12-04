using System.Collections.Generic;

namespace Assistant.Domain.ViewModels.Admin
{
    public class SlotViewModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public IEnumerable<string> ValueList { get; set; }
    }

    public class UpdateSlotViewModel : SlotViewModel
    {
        public string Id { get; set; }
    }

    public class GetSlotViewModel
    {
        IDictionary<string, string> Slots { get; set; }
    }
}
