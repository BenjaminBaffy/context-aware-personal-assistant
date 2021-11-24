using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Assistant.API
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        
        public string UserId => UserData.Id;
        public string FullName => UserData.Name;

        private readonly object _sync = new object();
        private volatile bool _isInitialized = false;
        private (string Id, string Name) _userData = default((string, string));
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserAccessor(
            IHttpContextAccessor httpContextAccessor
        )
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        internal (string Id, string Name) UserData
        {
            get
            {
                if (_isInitialized)
                {
                    return _userData;
                }

                lock (_sync)
                {
                    if (_isInitialized)
                    {
                        return _userData;
                    }

                    var context = _httpContextAccessor.HttpContext;
                    if (context != null && context.User != null && context.User.Identity.IsAuthenticated)
                    {
                        var sid = context.User.FindFirst(ClaimTypes.Sid); // TODO: test?!
                        var id = sid?.Value;

                        var name = context.User.Identity.Name;

                        _userData = (id, name);
                    }

                    _isInitialized = true;
                    return _userData;
                }
            }
        }
    }
}
