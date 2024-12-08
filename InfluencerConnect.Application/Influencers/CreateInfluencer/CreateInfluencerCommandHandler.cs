using InfluencerConnect.Application.Abstractions.Messaging;
using InfluencerConnect.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.Influencers.CreateInfluencer;
public sealed class CreateInfluencerCommandHandler : ICommandHandler<CreateInfluencerCommand, Guid>
{
    public Task<Result<Guid>> Handle(CreateInfluencerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
