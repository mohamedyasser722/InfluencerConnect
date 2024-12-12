using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Infrastructure.Hangfire;
public class RefreshTokenCleanupService
{
    private readonly ApplicationDbContext _context;

    public RefreshTokenCleanupService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CleanupExpiredTokensAsync()
    {
        await _context.RefreshTokens
            .Where(token => token.ExpiresOn < DateTime.UtcNow).ExecuteDeleteAsync();
    }
}