using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.Influencers;
public interface IInfluencerRepository
{
    Task<Influencer?> GetByIdAsync(Guid id);
}
