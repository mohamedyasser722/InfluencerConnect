using InfluencerConnect.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.Influencers.GetInfluencers;
public sealed record GetAllInfluencersQuery() : IQuery<List<InfluencersResponse>>; 
