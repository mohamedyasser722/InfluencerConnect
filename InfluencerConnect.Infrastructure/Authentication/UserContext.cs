using InfluencerConnect.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Infrastructure.Authentication;
public sealed class UserContext(IHttpContextAccessor accessor) : IUserContext
{
    public Guid UserId => throw new NotImplementedException();
}
