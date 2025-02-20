﻿using System.ComponentModel.DataAnnotations;

namespace InfluencerConnect.Infrastructure.Authentication;
public sealed record JwtOptions
{
    
    public static readonly string SectionName  = "Jwt";
    [Required]
    public string Key { get; init; } = string.Empty;
    [Required]
    public string Issuer { get; init; } = string.Empty;
    [Required]
    public string Audience { get; init; } = string.Empty;
    [Required]
    [Range(1, int.MaxValue)]
    public int ExpiryInMinutes { get; init; }
}

public sealed record RefreshTokenOptions
{
    public static readonly string SectionName = "RefreshToken";
    [Required]
    [Range(1, int.MaxValue)]
    public int RefreshTokenExpiryInDays { get; init; }
}