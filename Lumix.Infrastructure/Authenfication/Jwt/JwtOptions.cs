﻿namespace Lumix.Infrastructure.Authenfication;

public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int ExpiresHours { get; set; }
    public int RefreshTokenExpiresDays { get; set; } // Added property
}