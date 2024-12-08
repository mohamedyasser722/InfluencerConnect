using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.Influencers.CreateInfluencer;
public class CreateInfluencerCommandValidator : AbstractValidator<CreateInfluencerCommand>
{
    public CreateInfluencerCommandValidator()
    {
        
    }
}
