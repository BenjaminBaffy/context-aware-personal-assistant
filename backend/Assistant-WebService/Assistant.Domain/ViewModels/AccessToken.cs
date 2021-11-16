using System;

namespace Assistant.Domain.ViewModels
{
    public class AccessToken
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset ExpirestAt { get; set; }
    }
}
