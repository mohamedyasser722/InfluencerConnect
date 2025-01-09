using InfluencerConnect.Application.Abstractions.Authentication;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Infrastructure.Authentication;

public class TokenBlacklistService(IMemoryCache cache) : ITokenBlacklistService
{
    private readonly IMemoryCache _cache = cache;

    public Task BlacklistTokenAsync(string token, DateTime expiryDate)
    {
        // Use the token as the key and store its expiry date
        _cache.Set(token, true, expiryDate - DateTime.UtcNow);
        return Task.CompletedTask;
    }

    public Task<bool> IsTokenBlacklistedAsync(string token)
    {
        return Task.FromResult(_cache.TryGetValue(token, out _));
    }
}