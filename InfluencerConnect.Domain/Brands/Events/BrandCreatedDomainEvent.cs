using InfluencerConnect.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.Brands.Events;
public sealed record BrandCreatedDomainEvent(Guid BrandId) : IDomainEvent;

