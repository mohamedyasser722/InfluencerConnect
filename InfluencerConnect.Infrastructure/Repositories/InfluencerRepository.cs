using InfluencerConnect.Domain.Influencers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Infrastructure.Repositories;
public class InfluencerRepository : Repository<Influencer>, IInfluencerRepository
{
    private readonly ApplicationDbContext _dbcontext;
    public InfluencerRepository(ApplicationDbContext dbcontext) : base(dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<List<Influencer>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbcontext.Influencers
             .Include(i => i.ApplicationUser)
             .Include(i => i.InfluencerSocialMediaProfiles)
             .ToListAsync(cancellationToken);
    }
}
