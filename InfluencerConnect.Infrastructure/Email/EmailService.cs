using InfluencerConnect.Application.Abstractions.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Infrastructure.Email;
public class EmailService : IEmailService
{
    public Task SendAsync(string recepient, string subject, string body)
    {
        throw new NotImplementedException();
    }
}
