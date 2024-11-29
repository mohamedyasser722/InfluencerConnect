using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.Brands;
public interface IBrandRepository
{
    Task<Brand?> GetByIdAsync(Guid id);
}
