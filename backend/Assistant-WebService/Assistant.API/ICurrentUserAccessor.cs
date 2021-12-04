namespace Assistant.API
{
    public interface ICurrentUserAccessor
    {
        string UserId { get; }
        string FullName { get; }
    }
}
