using System.Text.Json.Serialization;

namespace Assistant.Domain.ViewModels.Admin
{
    public class UpdateUserViewModel : CreateUserViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}
