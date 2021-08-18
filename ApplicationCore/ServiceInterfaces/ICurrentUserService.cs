using System.Collections.Generic;

namespace ApplicationCore.ServiceInterfaces
{
    public interface ICurrentUserService
    {
        int  UserId { get; }
        bool IsAuthenticated { get; }
        string Email { get; }
        string FullName { get; }
        bool IsAdmin { get; }
        bool IsSuperAdmin { get; }
        IEnumerable<string> Roles { get; }
    }
}