using System;

namespace Assistant.Domain.ViewModels
{
    public class AccessToken
    {
        public string UserId { get; set; }
        public string LoginName { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }
}
