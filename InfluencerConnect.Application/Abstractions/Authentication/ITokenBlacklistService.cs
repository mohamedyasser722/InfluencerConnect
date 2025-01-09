using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.Abstractions.Authentication;
public interface ITokenBlacklistService
{
    Task BlacklistTokenAsync(string token, DateTime expiryDate);
    Task<bool> IsTokenBlacklistedAsync(string token);
}