using System.Text.Json.Serialization;

namespace Assistant.Domain.ViewModels.Admin
{
    public class UserViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }

    public class UserContextViewModel
    {
        // TODO
    }
}
