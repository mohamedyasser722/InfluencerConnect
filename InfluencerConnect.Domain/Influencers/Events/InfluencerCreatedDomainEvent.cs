using InfluencerConnect.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.Influencers.Events;
public sealed record InfluencerCreatedDomainEvent(Guid InfluencerId) : IDomainEvent;

