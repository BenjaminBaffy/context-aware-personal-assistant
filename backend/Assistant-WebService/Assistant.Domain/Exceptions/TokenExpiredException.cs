using System;

namespace Assistant.Domain.DatabaseModel
{
    public class TokenExpiredException : Exception
    {
        public TokenExpiredException(DateTimeOffset expirationOffset) : base($"Token has expired. Expiration date: {expirationOffset.ToString("")}") { }
    }
}
