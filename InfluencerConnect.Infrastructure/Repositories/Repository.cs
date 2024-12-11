using InfluencerConnect.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Infrastructure.Repositories;

public abstract class Repository<T>
    where T : Entity
{
    protected readonly ApplicationDbContext _dbcontext;
    public Repository(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    protected async Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbcontext
            .Set<T>()
            .ToListAsync(cancellationToken);
    }
    public void Add(T entity)
    {
         _dbcontext.Set<T>().Add(entity);
    }
    public void Update(T entity)
    {
        _dbcontext.Set<T>().Update(entity);
    }


}
