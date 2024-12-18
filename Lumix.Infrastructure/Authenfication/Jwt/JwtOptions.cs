﻿namespace Lumix.Infrastructure.Authenfication;

public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;
    public int RefreshTokenExpiresDays { get; set; } = 30;
    public int ExpiresHours { get; set; } = 1;
}