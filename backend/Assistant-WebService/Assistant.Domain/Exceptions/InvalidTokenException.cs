using System;

namespace Assistant.Domain.DatabaseModel
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string json) : base($"Token could not be serialized: '{json}'") { }
    }
}
