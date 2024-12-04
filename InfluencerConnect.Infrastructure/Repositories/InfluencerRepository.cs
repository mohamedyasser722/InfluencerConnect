using InfluencerConnect.Domain.Influencers;
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

}
