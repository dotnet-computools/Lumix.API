using Lumix.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Lumix.Infrastructure.Authenfication;

public class PermissionRequirement(Permission[] permissions)
    : IAuthorizationRequirement
{
    public Permission[] Permissions { get; set; } = permissions;
}